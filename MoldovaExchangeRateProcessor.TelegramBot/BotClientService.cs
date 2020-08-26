﻿using Microsoft.Extensions.Configuration;
using MoldovaExchangeRateProcessor.TelegramBot.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    class BotClientService :  IService
    {
        private readonly IRepository _repo;
        private readonly ILogger<BotClientService> _logger;
        private readonly IConfiguration _config;   

        public BotClientService(ILogger<BotClientService> logger, IConfiguration config, IRepository repo)
        {
            _repo = repo;
            _logger = logger;
            _config = config;
        }

        public void Run()
        {
            var telegramApiKey = _config.GetValue<string>("TelegramApiKey");
            var botArgs = new BotClientArgs(_logger, _config, _repo, telegramApiKey);
            var botClient = ExchangeRateProcessorBotClient.GetBotClient(botArgs);
            botClient.StartRecieving();
            Console.WriteLine("Press any key to stop Bot Client and exit...");
            Console.ReadKey();
            botClient.StopRecieving();
        }
    }
}
