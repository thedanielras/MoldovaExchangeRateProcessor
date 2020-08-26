using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    interface IBotBusinessLogic
    {
        public Task<string> RunCommandAsync(string commandLiteral);
    }
}
