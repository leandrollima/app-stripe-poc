using App.DTO.Response;
using App.DTO.ViewModels;
using App.Repository.Entity;
using App.Repository.Interfaces;
using App.Service.Constants;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StripeClient;
using StripeClient.Dto;

namespace App.Service
{
	public class PaymentService
    {
        const string OPEN = "open";
        const string PRODUCT_NOT_FOUND = "Product not found";

        private readonly IMapper _mapper;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly CheckoutStripeClient _checkoutStripeService;
        private readonly ProductStripeClient _productStripeClientService;
        private readonly ProductService _productService;

        public PaymentService(IMapper mapper,
            IRepository<Payment> paymentRepository,
            CheckoutStripeClient checkoutService,
            ProductStripeClient productClientService,
            ProductService productService)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _checkoutStripeService = checkoutService;
            _productStripeClientService = productClientService;
            _productService = productService;
        }

        public SessionDto? GetStripeSession(string sessionId) => _checkoutStripeService.GetSession(sessionId);

        public async Task<string> GetUrlForPackageAsync(string currency, string priceId, string successUrl, string cancelUrl, uint playerId, string? coupon)
        {
            var session = _checkoutStripeService.ProductPayment(priceId, successUrl, cancelUrl);

            var product = (await _productStripeClientService.GetProductsAsync(currency, true, true, p => p.Id == priceId))?.FirstOrDefault();

            Payment str = new Payment
            {
                Id = session.Id,
                PlayerId = playerId,
                PriceId = product!.Prices?.FirstOrDefault()?.Id!,
                ProductId = product?.Id!,
                ProductName = product?.Name!,
                Amount = session.AmountTotal!.Value,
                Currency = currency,
                CheckoutStatus = OPEN,
                PaymentStatus = OPEN,
                Coupon = coupon
            };

            _paymentRepository.Add(str);
            await _paymentRepository.SaveChangesAsync();

            return session.Url;
        }

        public async Task<PaymentDto> GetSessionAsync(string sessionId)
        {
            var session = await _paymentRepository.DbSet().AsNoTracking().SingleOrDefaultAsync(x => x.Id == sessionId);
            var dto = _mapper.Map<PaymentDto>(session);
            return dto;
        }

        public async Task UpdateStripePaymentOnWebhookAsync(PaymentDto stripePaymentDto)
        {
            var stripePayment = await _paymentRepository.DbSet().SingleOrDefaultAsync(x => x.Id.Equals(stripePaymentDto.SessionId));
            if (stripePayment is not null)
            {
                stripePayment.Paid = true;
                stripePayment.PaymentStatus = stripePaymentDto.PaymentStatus;
                stripePayment.CheckoutStatus = stripePaymentDto.CheckoutStatus;

                _paymentRepository.Update(stripePayment);
                await _paymentRepository.SaveChangesAsync();
            }
        }

        public async Task<CheckoutResultViewModel> CompletePayment(string sessionId)
        {
            CheckoutResultViewModel response = new CheckoutResultViewModel() { SessionId = sessionId };

            var stripeSession = GetStripeSession(sessionId);

            await UpdateStripePaymentAsync(stripeSession!);

            var paymentDto = await GetSessionAsync(sessionId);

            if (paymentDto is null)
            {
                response.AddError(PAYMENT.NOT_FOUND);
                return response;
            }

            var product = (await _productService.GetProductsAsync(paymentDto.Currency, x => x.Id == paymentDto.ProductId)).FirstOrDefault();

            if (product is null)
            {
                response.AddError(PRODUCT_NOT_FOUND);
                return response;
            }

            var player = new CharacterViewModel();// await _characterService.GetByIdWithOutfitAsync((int)paymentDto.PlayerId);

            if (player is null)
            {
                response.AddError(PLAYER.NOT_FOUND);
                return response;
            }

            response.ProductName = paymentDto.ProductName;
            response.Amount = paymentDto.Amount;
            response.ReceiptUrl = paymentDto.ReceiptUrl;
            response.PlayerId = player.Id;
            response.PlayerName = player.Name;
            response.LinkAnimOutFit = player.LinkAnimOutFit;
            response.Currency = paymentDto.Currency;
            response.MonthlyPackage = product.MonthlyPackage;
            response.PaymentUrl = stripeSession!.PaymentUrl;

            return response;
        }

        private async Task<int> UpdateStripePaymentAsync(SessionDto stripeSession)
        {
            if (stripeSession is null)
                return 0;

            var stripePayment = await _paymentRepository.DbSet().SingleOrDefaultAsync(x => x.Id.Equals(stripeSession.Id));
            if (stripePayment is not null)
            {
                stripePayment.Paid = stripeSession.PaymentStatus == "paid";
                stripePayment.PaymentStatus = stripeSession.PaymentStatus;
                stripePayment.CheckoutStatus = stripeSession.CheckoutStatus;
                stripePayment.Updated = DateTime.UtcNow;
                stripePayment.ReceiptUrl = stripeSession.ReceiptUrl;
                stripePayment.Currency = stripeSession.Currency;

                _paymentRepository.Update(stripePayment);
                return await _paymentRepository.SaveChangesAsync();
            }

            return 0;
        }
    }
}
