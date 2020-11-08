using CurrencyDataProvider;
using CurrencyDataProvider.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            //TODO: add fallback service for DB and sort out DI for actual DB

            //services.AddDbContext<CurrencyContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            //services.AddScoped<ICurrencyProvider, CurrencyFallback>();

            //services
            //    .AddScoped<CurrencyRepository>()
            //    .AddScoped<ICurrencyProvider, CurrencyRepository>(
            //        s => s.GetService<CurrencyRepository>());

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