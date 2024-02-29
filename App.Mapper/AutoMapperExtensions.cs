using Microsoft.Extensions.DependencyInjection;

namespace App.Mapper
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperDto(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapperExtensions).Assembly, 
                typeof(App.DTO.SuperClasses.DtoModel).Assembly, 
                typeof(App.Repository.Entity.Payment).Assembly,
                typeof(StripeClient.Dto.PriceDto).Assembly);
            return service;
        }
    }
}
