using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    interface IBotBusinessLogic
    {
        public string RunCommand(string commandLiteral);
    }
}
