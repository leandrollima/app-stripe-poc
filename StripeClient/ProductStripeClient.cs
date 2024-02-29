using StripeClient.Dto;
using Stripe;

namespace StripeClient
{
    public class ProductStripeClient
    {
        private readonly ProductService _productService;
        private readonly PriceService _priceService;

        public ProductStripeClient(ProductService productService, PriceService priceService)
        {
            this._productService = productService;
            this._priceService = priceService;
        }

        public async Task<ICollection<ProductDto>?> GetProductsAsync(string currency, bool? productsActive, bool? pricesActive)
        {
			var productListOptions = GetProductListOptions(productsActive);
			var priceListOptions = GetPriceListOptions(currency, pricesActive);

			var products = await _productService.ListAsync(productListOptions);
            var prices = await _priceService.ListAsync(priceListOptions);

            ICollection<ProductDto> productsDto = new List<ProductDto>();

            foreach (var product in products)
            {
                var pricesOfProduct = prices.Where(x => x.ProductId == product.Id);

                bool monthlyPackageValue = product.Metadata.TryGetValue("monthly_package", out var rawValue) && bool.TryParse(rawValue?.ToString(), out var parsedValue)
                       ? parsedValue
                       : false;

                uint purchaseLimitValue = product.Metadata.TryGetValue("purchase_limit", out var rawPurchaseLimit) && uint.TryParse(rawPurchaseLimit?.ToString(), out var parsedPurchaseLimit)
                   ? parsedPurchaseLimit
                   : 0;

                ProductDto productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Prices = new List<PriceDto>(),
                    MonthlyPackage = monthlyPackageValue,
                    PurchaseLimit = purchaseLimitValue,
                    Features = product.Features is null ? new List<string>() : product.Features.Select(x => x.Name).ToList(),
                    Active = product.Active,
                };

                foreach (var priceOfProduct in pricesOfProduct)
                {
                    productDto.Prices.Add(new PriceDto
                    {
                        Id = priceOfProduct.Id,
                        Currency = priceOfProduct.Currency,
                        Interval = priceOfProduct.Recurring?.Interval,
                        Amount = priceOfProduct.UnitAmount,
                        Active = priceOfProduct.Active,
                    });
                }

                if (productDto.Prices.Any())
                    productsDto.Add(productDto);
            }

            return productsDto;
        }

        public async Task<ICollection<ProductDto>?> GetProductsAsync(string currency, bool? productsActive, bool? pricesActive, Func<Price, bool> expression)
        {
            var productListOptions = GetProductListOptions(productsActive);
			var priceListOptions = GetPriceListOptions(currency, pricesActive);

			var products = await _productService.ListAsync(productListOptions);
			var prices = await _priceService.ListAsync(priceListOptions);

            var pricesOfProduct = prices.Where(expression).ToList();

            ICollection<ProductDto> productsDto = new List<ProductDto>();

            foreach (var price in pricesOfProduct)
            {
                var _products = products.Where(x => x.Id == price.ProductId);
                if(_products is not null)
                {
                    foreach (var product in _products)
                    {
                        bool monthlyPackageValue = product.Metadata.TryGetValue("monthly_package", out var rawValue) && bool.TryParse(rawValue?.ToString(), out var parsedValue)
                      ? parsedValue
                      : false;

                        uint purchaseLimitValue = product.Metadata.TryGetValue("purchase_limit", out var rawPurchaseLimit) && uint.TryParse(rawPurchaseLimit?.ToString(), out var parsedPurchaseLimit)
                           ? parsedPurchaseLimit
                           : 0;

                        ProductDto productDto = new ProductDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Prices = new List<PriceDto>(),
                            MonthlyPackage = monthlyPackageValue,
                            PurchaseLimit = purchaseLimitValue,
                            Features = product.Features is null ? new List<string>() : product.Features.Select(x => x.Name).ToList()
                        };

                        foreach (var priceOfProduct in pricesOfProduct)
                        {
                            productDto.Prices.Add(new PriceDto
                            {
                                Id = priceOfProduct.Id,
                                Currency = priceOfProduct.Currency,
                                Interval = priceOfProduct.Recurring?.Interval,
                                Amount = priceOfProduct.UnitAmount,
                            });
                        }

                        if (productDto.Prices.Any())
                            productsDto.Add(productDto);
                    }
                }
            }
            return productsDto;
        }

        public async Task<ICollection<PriceDto>?> GetPricesAsync(string currency, bool? pricesActive, Func<Price, bool> expression)
        {
            var priceListOptions = GetPriceListOptions(currency, pricesActive);

			var prices = await _priceService.ListAsync(priceListOptions);

            var pricesOfProduct = prices.Where(expression);

            ICollection<PriceDto> pricesDto = new List<PriceDto>();

            foreach (var priceOfProduct in pricesOfProduct)
            {
                pricesDto.Add(new PriceDto
                {
                    Id = priceOfProduct.Id,
                    Currency = priceOfProduct.Currency,
                    Interval = priceOfProduct.Recurring?.Interval,
                    Amount = priceOfProduct.UnitAmount,
                });
            }

            return pricesDto;
        }


        #region Private methods

        private ProductListOptions GetProductListOptions(bool? productsActive)
        {
			var productListOptions = new ProductListOptions();
			if (productsActive is not null)
				productListOptions.Active = productsActive.Value;
            return productListOptions;
		}

		private PriceListOptions GetPriceListOptions(string currency, bool? pricesActive)
        {
			var priceListOptions = new PriceListOptions();

			if (pricesActive is not null)
				priceListOptions.Active = pricesActive.Value;

			if (!string.IsNullOrWhiteSpace(currency))
				priceListOptions.Currency = currency;

            return priceListOptions;

		}

		#endregion Private methods
	}
}
