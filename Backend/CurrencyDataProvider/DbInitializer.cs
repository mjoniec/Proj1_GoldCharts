using CurrencyDataProvider.Initialize;
using Microsoft.EntityFrameworkCore.Internal;

namespace CurrencyDataProvider
{
    public static class DbInitializer
    {
        public static void Initialize(CurrencyContext context)
        {
            context.Database.EnsureCreated();

            if (!context.USD_AUD.Any())
            {
                USD_AUD_Initialize.Initialize(context);
            }

            if (!context.USD_EUR.Any())
            {
                USD_EUR_Initialize.Initialize(context);
            }

            if (!context.EUR_AUD.Any())
            {
                EUR_AUD_Initialize.Initialize(context);
            }

            context.SaveChanges();
        }
    }
}
