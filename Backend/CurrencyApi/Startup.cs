using CurrencyApi.Repositories;
using CurrencyApi.Services;
using CurrencyDatabaseCommon;
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

            services.AddScoped<ICurrencyService, CurrencyService>();

            services
                .AddScoped<CurrencyRepository>()
                .AddScoped<ICurrencyRepository, CurrencyRepository>(
                    s => s.GetService<CurrencyRepository>());

            services
                .AddScoped<CurrencyFallback>()
                .AddScoped<ICurrencyRepository, CurrencyFallback>(
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
