using CurrencyDataProvider.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyApi
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
                .AddScoped<CurrencyRepository>()
                .AddScoped<ICurrencyProvider, CurrencyRepository>(
                    s => s.GetService<CurrencyRepository>());

            services
                .AddScoped<CurrencyFallback>()
                .AddScoped<ICurrencyProvider, CurrencyFallback>(
                    s => s.GetService<CurrencyFallback>());

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
