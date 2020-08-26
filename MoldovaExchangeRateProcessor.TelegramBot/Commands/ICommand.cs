using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    interface ICommand
    {
        public Task<string> ExecuteAsync();
    }
}
