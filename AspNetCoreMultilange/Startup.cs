using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace SixtyThreeBits.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment Env)
        {

        }

        public void ConfigureServices(IServiceCollection Services) 
        {                     
            Services.AddControllersWithViews(Options=> { 
                Options.RespectBrowserAcceptHeader = true;                
            });
            
            Services.Configure<RouteOptions>(routeOptions => {
                routeOptions.AppendTrailingSlash = true;
            });
        }

        public void Configure(IApplicationBuilder App, IWebHostEnvironment Env)
        {
            if (Env.IsDevelopment())
            {
                App.UseDeveloperExceptionPage();
            }
            

            App.UseFileServer();            
            App.UseRouting();


            var CultureEn = new CultureInfo("en");
            var CultureEs = new CultureInfo("es");
            var CultureJa = new CultureInfo("ja");
            var CultureKa = new CultureInfo("ka") { NumberFormat = new NumberFormatInfo { CurrencyDecimalSeparator = "." } };

            var RequestLocalizationOptions = new RequestLocalizationOptions();
            RequestLocalizationOptions.RequestCultureProviders.Clear();
            RequestLocalizationOptions.RequestCultureProviders.Add(new CustomCultureProvider());
            RequestLocalizationOptions.SupportedCultures = new List<CultureInfo> { CultureEn, CultureEs, CultureJa, CultureKa };
            RequestLocalizationOptions.SupportedUICultures = new List<CultureInfo> { CultureEn, CultureEs, CultureJa, CultureKa };
            App.UseRequestLocalization(RequestLocalizationOptions);

            App.UseEndpoints(Endpoints =>
            {
                Endpoints.MapControllers();
            });
        }

        public class CustomCultureProvider : RequestCultureProvider
        {
            public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext Context)
            {
                var DefautlCulture = "en";
                var Culture = Context.Request.RouteValues["Culture"]?.ToString() ?? DefautlCulture;
                await Task.Yield();
                return new ProviderCultureResult(Culture);
            }
        }
    }
}
