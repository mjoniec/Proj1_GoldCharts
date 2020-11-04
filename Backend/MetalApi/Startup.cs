using System;
using MetalApi.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MetalApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<GuandlMetalsPricesProvider>()
                .AddScoped<IMetalsPricesProvider, GuandlMetalsPricesProvider>(
                    s => s.GetService<GuandlMetalsPricesProvider>());

            services
                .AddScoped<FallbackMetalsPricesProvider>()
                .AddScoped<IMetalsPricesProvider, FallbackMetalsPricesProvider>(
                    s => s.GetService<FallbackMetalsPricesProvider>());

            //TODO: figure out better way to set up HttpFallback
            var httpFallback = services
                .BuildServiceProvider()
                .GetService<HttpFallback>();

            services.AddHttpClient("QuandlService", client =>
            {
                //client.BaseAddress = new Uri("https://www.quandl.com/api/v3/datasets/");
                client.BaseAddress = new Uri("https://www.quandl.com/api/v3/xxx/");//wrong url for tests
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(httpFallback.FallbackPolicy);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
