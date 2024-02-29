using App.DTO.ViewModels;
using App.Repository.Entity;
using App.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Service
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductViewModel>> GetMonthlyPackages(string currency = "brl")
        {
            return await GetProductsAsync(currency, product => product.MonthlyPackage == true);
        }

        public async Task<List<ProductViewModel>> GetProductsAsync(string currency = "brl", Func<Product, bool>? expression = null)
        {
            List<ProductViewModel> productsVM = new List<ProductViewModel>();

            List<Product> products = new List<Product>();

            IQueryable<Product> iqProducts = _productRepository.DbSet()
              .Include(x => x.Prices)
               .Where(x => x.Active && x.Prices.Any(p => p.Currency == currency && p.Active== true));

            if (expression is not null)
                products = iqProducts.Where(expression).ToList();
            else
                products = await iqProducts.ToListAsync();

            if (products is null)
                return productsVM;

            foreach (var product in products)
            {
                var price = product.Prices?.OrderByDescending(x => x.Created).FirstOrDefault();

                if (price is not null)
                {
                    productsVM.Add(new ProductViewModel
                    {
                        Currency = currency,
                        Id = product.Id,
                        ProductName = string.IsNullOrWhiteSpace(product.ProductName) ? string.Empty : product.ProductName,
                        Description = string.IsNullOrWhiteSpace(product.Description) ? string.Empty : product.Description,
                        PriceAmount = price.UnitAmount,
                        PriceId = price.Id!,
                        Features = product.Features?.Split(';'),
                        MonthlyPackage = product.MonthlyPackage
                    });;
                }
            }
            return productsVM;
        }     
    }
}
