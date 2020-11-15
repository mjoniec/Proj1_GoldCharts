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
            //adds db contexts to services
            //dev or prod connection string is determined by json
            services.AddDbContext<CurrencyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddScoped<CurrencyProvider>()
                .AddScoped<ICurrencyProvider, CurrencyProvider>(
                    s => s.GetService<CurrencyProvider>());

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
