using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.WebParser.Models
{
    public enum ExchangeRateCurrency
    {
        EUR,
        USD,
        RON
    }

    public class ExchangeRate
    {
        public double BuyRate { get; set; }
        public double SellRate { get; set; }
        public ExchangeRateCurrency Currency { get; private set; }

        public ExchangeRate(double buyRate, double sellRate, ExchangeRateCurrency currency)
        {
            BuyRate = buyRate;
            SellRate = sellRate;
            Currency = currency;
        }
    }
}
