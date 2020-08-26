using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    class HelpCommand : ICommand
    {
        public Task<string> ExecuteAsync()
        {
            return Task.Run(() => "List of Commands: \n/getCurrent - gets the current exchange rate");
        }
    }
}
