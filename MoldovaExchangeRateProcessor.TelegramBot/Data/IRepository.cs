using MoldovaExchangeRateProcessor.WebParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot.Data
{
    interface IRepository
    {
        List<Bank> GetBanksAndRelativeExchangeRatesByDate(DateTime date);
        List<ExchangeRate> GetExchangeRatesByBankName(string name);
    }
}
