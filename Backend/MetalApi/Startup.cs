using System;
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
            services.AddHttpClient("QuandlService", client =>
            {
                //client.BaseAddress = new Uri("https://www.quandl.com/api/v3/datasets/");
                client.BaseAddress = new Uri("https://www.quandl.com/api/v3/xxx/");//wrong url for tests
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(HttpFallback.FallbackPolicy);

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
