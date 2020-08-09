using MoldovaExchangeRateProcessor.TelegramBot.Commands;
using MoldovaExchangeRateProcessor.WebParser.Models;
using MoldovaExchangeRateProcessor.WebParser.Models.Banks;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    class ExchangeRateBotBusinessLogic : IBotBusinessLogic
    {
        public string RunCommand(string commandLiteral) => GetCommand(commandLiteral).Execute();

        private ICommand GetCommand(string commandLiteral)
        {
            switch (commandLiteral)
            {
                case "start":
                    return new HelpCommand();
                case "help":
                    return new HelpCommand();
                case "getExchange":
                    return new GetExchangeRateCommand(new List<Bank>() { new MoldovaAgroindbank() });
                default:
                    return new UnknownCommand();
            }
        }
    }
}
