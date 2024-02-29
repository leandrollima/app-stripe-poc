using App.Mapper;
using App.Service.Extensions;
using Microsoft.EntityFrameworkCore;

namespace App.Web.MVC.Configuration
{
	public static class StartupServicesConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment/*, SecretClient azureSecretClient*/)
        {
            services.AddAuthentication(configuration);
            services.AddContextAndRepository(configuration.GetConnectionString("DefaultConnection")!, configuration.GetSection("MariaDbVersion").Value!);

            services.AddStripeConfigurations(configuration);
            services.AddServices();

            services.AddAutoMapperDto();
            services.AddSettingsModels(configuration);
        }
    }
}
