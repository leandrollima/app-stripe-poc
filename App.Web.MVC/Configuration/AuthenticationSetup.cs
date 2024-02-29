using App.Repository.Context;
using App.Repository.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Web.MVC.Configuration
{
	public static class AuthenticationSetup
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
   //         services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
   //         {
   //             options.Password.RequireDigit = false;
   //             options.Password.RequireLowercase = false;
   //             options.Password.RequireNonAlphanumeric = false;
   //             options.Password.RequireUppercase = false;
   //             options.Password.RequiredLength = 8;
   //             options.SignIn.RequireConfirmedEmail = true;
                
   //             options.User.RequireUniqueEmail = true;

   //         })
   //             .AddEntityFrameworkStores<AppDbContext>();            			
            
   //         services
			//.AddAuthentication(options =>
			//{
			//	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//	options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			//	options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

			//})
			//.AddCookie(options =>
			//{
			//	options.LoginPath = "/sign-in";
			//	options.AccessDeniedPath = "/sign-out";
   //             options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
   //             options.Cookie.SameSite = SameSiteMode.Lax;
   //         });
        }
    }
}
