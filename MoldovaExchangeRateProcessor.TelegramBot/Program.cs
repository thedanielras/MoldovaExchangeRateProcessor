using System;
using MoldovaExchangeRateProcessor.WebParser;
using MoldovaExchangeRateProcessor.WebParser.Models;
using MoldovaExchangeRateProcessor.WebParser.Models.BankProcessors;
using Telegram.Bot;
using Telegram.Bot.Args;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoldovaExchangeRateProcessor.TelegramBot.Data;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    class Program
    {
        static void Main()
        {
            var configBuilder = new ConfigurationBuilder();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configBuilder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string connectionString = context.Configuration.GetConnectionString("SqlServerDb");
                    services.AddTransient<IService, BotClientService>();
                    services.AddTransient<IRepository, SqlServerRepo>();
                    services.AddDbContext<SqlServerDbContext>(opt => opt.UseSqlServer(connectionString));
                  
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<BotClientService>(host.Services);
            svc.Run();
        }
    }
}
