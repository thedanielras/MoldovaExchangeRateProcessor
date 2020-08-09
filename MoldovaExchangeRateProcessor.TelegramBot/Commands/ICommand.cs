using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    interface ICommand
    {
        public string Execute();
    }
}
