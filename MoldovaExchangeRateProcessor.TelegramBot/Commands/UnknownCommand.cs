using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    class UnknownCommand : ICommand
    {
        public Task<string> ExecuteAsync()
        {
            return new Task<string>(() => "Unknown Command. Type /help for the list of commands.");
        }
    }
}
