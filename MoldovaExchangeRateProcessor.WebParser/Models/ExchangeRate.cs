using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace MoldovaExchangeRateProcessor.WebParser.Models
{
    public enum ExchangeRateCurrency
    {
        EUR,
        USD,
        RON,
        RUB
    }

    public class ExchangeRate
    {
        public int Id { get; private set; }
        public double BuyRate { get; set; }
        public double SellRate { get; set; }
        public DateTime Date { get; private set; }
        public ExchangeRateCurrency Currency { get; private set; }
        public Bank Bank { get; set; }
        public int BankId { get; set; }
        public bool IsCorrect
        {
            get
            {
                if (BuyRate == 0d || SellRate == 0d) return false;
                return true;
            }
        }

        public ExchangeRate(double buyRate, double sellRate, ExchangeRateCurrency currency)
        {
            BuyRate = buyRate;
            SellRate = sellRate;
            Currency = currency;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ",";

            return $"{Currency.ToString()}: Buy: {BuyRate.ToString("00.00", nfi)} | Sell: {SellRate.ToString("00.00", nfi)}";
        }        
    }
}
