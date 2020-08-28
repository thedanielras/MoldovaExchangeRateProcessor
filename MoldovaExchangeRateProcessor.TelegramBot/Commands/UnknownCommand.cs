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
            return Task.Run(() => "Unknown Command. Type /help for the list of commands.");
        }
    }
}
