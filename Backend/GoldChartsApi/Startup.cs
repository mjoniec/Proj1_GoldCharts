using System;
using GoldChartsApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoldChartsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();            
            services.AddScoped<CombineCurrencyAndMetalDataService>();

            //TODO: add filters and pipe

            var metalApiOptions = new MetalApiOptions();

            Configuration
                .GetSection(MetalApiOptions.MetalApi)
                .Bind(metalApiOptions);

            services.AddHttpClient(MetalApiOptions.MetalApi, client =>
            {
                client.BaseAddress = new Uri(metalApiOptions.Url);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            var currencyApiOptions = new CurrencyApiOptions();

            Configuration
                .GetSection(CurrencyApiOptions.CurrencyApi)
                .Bind(currencyApiOptions);

            services.AddHttpClient(CurrencyApiOptions.CurrencyApi, client =>
            {
                client.BaseAddress = new Uri(currencyApiOptions.Url);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
