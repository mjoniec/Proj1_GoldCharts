using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurrencyDataProvider;
using CurrencyDataProvider.Providers;
using GoldChartsApi.Services;
using MetalsDataProvider.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;


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
            var fallbackPolicy =
                Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .FallbackAsync(FallbackAction, OnFallbackAsync);

            services.AddHttpClient("QuandlService", client =>
            {
                //client.BaseAddress = new Uri("https://www.quandl.com/api/v3/datasets/");
                client.BaseAddress = new Uri("https://www.quandl.com/api/v3/xxx/");//wrong url for tests
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(fallbackPolicy);

            services.AddDbContext<CurrencyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddControllers();
            
            services.AddScoped<CombineCurrencyAndMetalDataService>();
            
            //services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            services.AddScoped<ICurrenciesProvider, CurrenciesFallback>();

            services.AddScoped<IMetalsPricesProvider, GuandlMetalsPricesProvider>();
            //services.AddScoped<IMetalsPricesProvider, FallbackMetalsPricesProvider>();
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

        private Task OnFallbackAsync(DelegateResult<HttpResponseMessage> response, Context context)
        {
            //Console.WriteLine("About to call the fallback action. This is a good place to do some logging");
            return Task.CompletedTask;
        }

        private Task<HttpResponseMessage> FallbackAction(DelegateResult<HttpResponseMessage> responseToFailedRequest, Context context, CancellationToken cancellationToken)
        {
            //Console.WriteLine("Fallback action is executing");

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(responseToFailedRequest.Result.StatusCode)
            {
                Content = new StringContent($"The fallback executed, the original error was {responseToFailedRequest.Result.Content.ReadAsStringAsync()}")
            };
            return Task.FromResult(httpResponseMessage);
        }
    }
}
