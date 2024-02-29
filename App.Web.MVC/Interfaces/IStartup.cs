namespace App.Web.MVC.Interfaces
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void ConfigureServices(IServiceCollection services, IConfigurationBuilder configurationBuilder, IWebHostEnvironment environment);
        void Configure(WebApplication app, IWebHostEnvironment env);
    }
}
