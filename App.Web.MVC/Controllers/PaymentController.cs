using App.DTO.Response;
using App.DTO.ViewModels;
using App.Service;
using App.Web.MVC.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.MVC.Controllers
{
    [AllowAnonymous]
    public class PaymentController : Controller
    {
        const string CURRENCY = "brl";

        private readonly PaymentService _paymentService;
        private readonly ProductService _productService;
        private readonly ImportProductsStripeService _importProductsStripeService;

        public PaymentController(PaymentService paymentService,
            ProductService productService,
            ImportProductsStripeService importProductsStripeService)
        {
            _paymentService = paymentService;
            _productService = productService;
            _importProductsStripeService = importProductsStripeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetMonthlyPackages(CURRENCY);
            var players = new List<CharacterViewModel>(); // await _accountService.GetPlayersFromAccountAsync(email);

			return View(new CheckoutPackagesViewModel() { Products = products, Players = players });
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutPackagesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Products = await _productService.GetMonthlyPackages(CURRENCY);
                model.Players = new List<CharacterViewModel>(); // await _accountService.GetPlayersFromAccountAsync(email);

				return View(model);
            }

            var player = new CharacterViewModel(); // await _accountService.GetPlayersFromAccountAsync(email);

			var sessionUrl = await _paymentService.GetUrlForPackageAsync(currency: model.Currency, priceId: model.PriceId,
                successUrl: HttpContext.GetUrl($"payment/success")!,
                cancelUrl: HttpContext.GetUrl($"payment/canceled")!,
                playerId: (uint)player.Id,
                coupon: model.Coupon);

            return Redirect(sessionUrl);
        }

        public IActionResult Success(string sessionId)
        {
            return View();
        }

        public IActionResult Cancel(string sessionId)
        {
            return View();
        }

        [Route("import")]
        public async Task<IActionResult> ImportProducts()
        {
            var completed = await _importProductsStripeService.ImportProductsFromStripeToDatabaseAsync();
            return completed == 0 ? NoContent() : Ok("Importação realizada");
        }
    }
}
