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
            //TODO: fallback for DB an proper DI for services
            //services.AddDbContext<CurrencyContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            //services.AddScoped<ICurrencyProvider, CurrencyFallback>();

            //services.AddScoped<IMetalsPricesProvider, GuandlMetalsPricesProvider>();
            //services.AddScoped<IMetalsPricesProvider, FallbackMetalsPricesProvider>();


            services.AddControllers();            
            services.AddScoped<CombineCurrencyAndMetalDataService>();

            
            //TODO: servie names and url to configs, add dev prod etc...
            services.AddHttpClient("MetalApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:57365/api/metal/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient("CurrencyApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:54782/api/currency/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
