using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoldovaExchangeRateProcessor.TelegramBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    class BotClientArgs : IBotClientArgs
    {
        public string ApiKey { get; }
        public ILogger Logger { get; }
        public IConfiguration Config { get; }
        public IRepository Repository { get; }

        public BotClientArgs(ILogger logger, IConfiguration config, IRepository repository, string apiKey)
        {
            ApiKey = apiKey;
            Logger = logger;
            Config = config;
            Repository = repository;
        }
    }
}
