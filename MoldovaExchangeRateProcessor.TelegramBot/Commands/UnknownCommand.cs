using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    class UnknownCommand : ICommand
    {
        public string Execute()
        {
            return "Unknown Command. Type /help for the list of commands.";
        }
    }
}
