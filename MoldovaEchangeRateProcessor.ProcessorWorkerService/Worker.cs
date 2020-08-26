using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoldovaEchangeRateProcessor.ProcessorWorkerService.Data;
using MoldovaExchangeRateProcessor.WebParser.Models;
using MoldovaExchangeRateProcessor.WebParser.Models.BankProcessors;

namespace MoldovaEchangeRateProcessor.ProcessorWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IRepository _repository;
        private readonly IServiceProvider _services;
        public Worker(ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _services.CreateScope())
            {
                _repository = scope.ServiceProvider.GetRequiredService<IRepository>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                                       
                    var banks = await GetBanksExchangeRatesAsync();
                                       
                    foreach(var bank in banks)
                    {
                        foreach(var rate in bank.ExchangeRates)
                        {
                            try
                            {
                                _repository.AddExchangeRate(rate);
                                _logger.LogInformation("Rate successfully added to db!");
                            }
                            catch(Exception ex)
                            {
                                _logger.LogError("An error occured adding rate to db {0}", ex.Message);
                            }
                        }
                    }

                    await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
                }
            }
        }

        private async Task<Bank[]> GetBanksExchangeRatesAsync()
        {
            var bankProcessors = new List<BankProcessor> { 
                new MoldovaAgroindbank(), 
                new Victoriabank(), 
                new Moldindconbank(),
                new Eximbank()
            };

            var tasks = new List<Task<Bank>>();

            foreach (var bankProcessor in bankProcessors)
            {
                tasks.Add(bankProcessor.ProcessAsync());
            }

            return await Task.WhenAll(tasks);
        } 
    }
}
