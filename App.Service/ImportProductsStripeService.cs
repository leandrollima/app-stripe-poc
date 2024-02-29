using App.Repository.Entity;
using App.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using StripeClient;

namespace App.Service
{
    public class ImportProductsStripeService
    {
        private readonly ProductStripeClient _productClientService;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Price> _priceRepository;

        public ImportProductsStripeService(ProductStripeClient productClientService,
            IRepository<Product> productRepository,
            IRepository<Price> priceRepository)
        {
            _productClientService = productClientService;
            _productRepository = productRepository;
            _priceRepository = priceRepository;
        }

        public async Task<int> ImportProductsFromStripeToDatabaseAsync(string currency = "brl")
        {
            var stripeProducts = await _productClientService.GetProductsAsync(currency, null, null); //  null, null -> import all

			if (stripeProducts is null)
                return 0;

            List<Product> products = _productRepository.DbSet()
                .Include(x => x.Prices)
                 .Where(x => x.Prices.Any(p => p.Currency == currency))
                .ToList();

            foreach (var stripeProduct in stripeProducts)
            {
				var product = products.SingleOrDefault(x => x.Id == stripeProduct.Id);

                if (product is null)
                {
                    Product newProduct = new Product
                    {
                        Id = stripeProduct.Id,
                        ProductName = string.IsNullOrWhiteSpace(stripeProduct.Name) ? string.Empty : stripeProduct.Name,
                        Description = string.IsNullOrWhiteSpace(stripeProduct.Description) ? string.Empty : stripeProduct.Description,
                        PurchaseLimit = stripeProduct.PurchaseLimit,
                        MonthlyPackage = stripeProduct.MonthlyPackage,
                        Features = String.Join(';', stripeProduct.Features),
                        Active = stripeProduct.Active
                    };

                    if (stripeProduct.Prices?.Count > 0)
                    {
                        foreach (var stripePrice in stripeProduct.Prices)
                        {
                            var newPrice = new Price
                            {
                                Id = stripePrice!.Id!,
                                Currency = stripePrice.Currency!,
                                Type = "one_time",
                                UnitAmount = stripePrice.Amount is null ? 0 : stripePrice.Amount.Value!,
                                Active = stripePrice.Active
                            };

                            _priceRepository.Add(newPrice);
                            newProduct.Prices.Add(newPrice);
                        }
                    }

                    _productRepository.DbSet().Entry(newProduct).State = EntityState.Added;
                    _productRepository.Add(newProduct);
                }
                else
                {
                    product.ProductName = string.IsNullOrWhiteSpace(stripeProduct.Name) ? string.Empty : stripeProduct.Name;
                    product.Description = string.IsNullOrWhiteSpace(stripeProduct.Description) ? string.Empty : stripeProduct.Description;
                    product.PurchaseLimit = stripeProduct.PurchaseLimit;
                    product.MonthlyPackage = stripeProduct.MonthlyPackage;
                    product.Features = String.Join(';', stripeProduct.Features);
                    product.Active = stripeProduct.Active;
                    product.Updated = DateTime.UtcNow;

                    if (stripeProduct.Prices?.Count > 0)
                    {
                        foreach (var stripePrice in stripeProduct.Prices)
                        {
                            var price = product!.Prices.SingleOrDefault(x => x.Id == stripePrice.Id);
                            if (price is null)
                            {
                                var newPrice = new Price
                                {
                                    Id = stripePrice!.Id!,
                                    Currency = stripePrice.Currency!,
                                    Type = "one_time",
                                    UnitAmount = stripePrice.Amount is null ? 0 : stripePrice.Amount.Value!,
                                    Active = stripePrice.Active
                                };

                                _priceRepository.Add(newPrice);
                                product.Prices.Add(newPrice);
                            }
                            else
                            {
                                price.UnitAmount = stripePrice.Amount is null ? 0 : stripePrice.Amount.Value!;
                                price.Active = stripePrice.Active;
                                price.Updated = DateTime.UtcNow;

                                _priceRepository.DbSet().Entry(price).State = EntityState.Modified;
                            }
                        }
                    }
                    _productRepository.DbSet().Entry(product).State = EntityState.Modified;
                }
            }
            return await _productRepository.SaveChangesAsync();
        }
    }
}
