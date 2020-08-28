using MoldovaExchangeRateProcessor.WebParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.ProcessorWorkerService.Data
{
    public interface IRepository
    {
        void AddExchangeRate(ExchangeRate rate);
        Bank GetBankByName(string bankName);
    }
}
