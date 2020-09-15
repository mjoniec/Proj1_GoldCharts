using CurrencyDataProvider.DbModel;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace CurrencyDataProvider
{
    public static class DbInitializer
    {
        public static void Initialize(CurrencyContext context)
        {
            context.Database.EnsureCreated();

            if (!context.USD_AUD.Any())
            {
                context.USD_AUD.Add(new USD_AUD { Date = new DateTime(2020, 1, 1), Rate = 1.1 });
                context.USD_AUD.Add(new USD_AUD { Date = new DateTime(2020, 1, 2), Rate = 1.2 });
            }

            if (!context.USD_EUR.Any())
            {
                context.USD_EUR.Add(new USD_EUR { Date = new DateTime(2020, 1, 1), Rate = 2.1 });
                context.USD_EUR.Add(new USD_EUR { Date = new DateTime(2020, 1, 2), Rate = 2.2 });
            }

            context.SaveChanges();
        }
    }
}
