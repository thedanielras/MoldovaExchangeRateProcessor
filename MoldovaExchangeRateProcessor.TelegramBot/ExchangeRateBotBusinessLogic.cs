using Microsoft.Extensions.Logging;
using MoldovaExchangeRateProcessor.TelegramBot.Commands;
using MoldovaExchangeRateProcessor.WebParser.Models;
using MoldovaExchangeRateProcessor.WebParser.Models.BankProcessors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    class ExchangeRateBotBusinessLogic : IBotBusinessLogic
    {
        private readonly IBotClientArgs _botArgs;
        private readonly ILogger _logger;

        public Task<string> RunCommandAsync(string commandLiteral)
        {
            var command = GetCommand(commandLiteral);
            var result = command.ExecuteAsync();
            
            return result;
        }

        public ExchangeRateBotBusinessLogic(IBotClientArgs botArgs)
        {
            _botArgs = botArgs;
            _logger = botArgs.Logger;
        }

        private ICommand GetCommand(string commandLiteral)
        {          
            switch (commandLiteral)
            {
                case "start":                    
                    return new HelpCommand();
                case "help":                 
                    return new HelpCommand();              
                case "getCurrent":                 
                    return new GetCurrentCommand(_botArgs);       
                default:                 
                    return new UnknownCommand();
            }
        }
    }
}
