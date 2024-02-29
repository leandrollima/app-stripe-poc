using App.Service.Extensions;
using App.Web.MVC.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.MVC
{
	public class Startup : Interfaces.IStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;
        public void ConfigureServices(IServiceCollection services, IConfigurationBuilder configurationBuilder, IWebHostEnvironment environment)
        {
            services.RegisterServices(Configuration, environment);
          
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
                .AddRazorRuntimeCompilation();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
                        
            app.UseStripe();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Payment}/{action=Index}/{id?}");
        }
    }
}
