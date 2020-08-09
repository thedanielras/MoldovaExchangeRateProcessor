using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    class HelpCommand : ICommand
    {
        public string Execute()
        {
            return "List of Commands: \n/getExchange - gets the exchange rate";
        }
    }
}
