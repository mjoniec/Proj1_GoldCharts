using CurrencyDatabaseCommon;
using CurrencyDatabaseCommon.Data;
using CurrencyDatabaseCommon.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CurrencyDatabaseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Spinning CurrencyDatabaseGenerator");

            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<CurrencyContext>();

                //TODO: dev config
                optionsBuilder.UseSqlServer("Server=DESKTOP-CNCHB79\\SQLEXPRESS;Database=Currency;Trusted_Connection=True;MultipleActiveResultSets=true");
                
                //TODO: prod config
                //options.UseSqlServer("Server=DESKTOP-CNCHB79\\SQLEXPRESS;Database=Currency;Trusted_Connection=True;MultipleActiveResultSets=true");

                CurrencyContext context = new CurrencyContext(optionsBuilder.Options);

                Initialize(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Generated");
        }

        public static void Initialize(CurrencyContext context)
        {
            context.Database.EnsureCreated(); //creates tables

            if (!context.USD_AUD.Any())
            {
                context.USD_AUD.AddRange(
                    USD_AUD_Data.Get()
                        .Select(l => new USD_AUD { Date = l.Date, Value = l.Value })
                        .ToList());
            }

            if (!context.USD_EUR.Any())
            {
                context.USD_EUR.AddRange(
                    USD_EUR_Data.Get()
                        .Select(l => new USD_EUR { Date = l.Date, Value = l.Value })
                        .ToList());
            }

            if (!context.EUR_AUD.Any())
            {
                context.EUR_AUD.AddRange(
                    EUR_AUD_Data.Get()
                        .Select(l => new EUR_AUD { Date = l.Date, Value = l.Value })
                        .ToList());
            }

            context.SaveChanges();
        }
    }
}
