using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoldovaExchangeRateProcessor.WebParser.Models
{
    public class Bank
    {
        public Bank()
        {
            ExchangeRates = new List<ExchangeRate>();
        }
        public Bank(string name, List<ExchangeRate> rates)
        {
            Name = name;
            ExchangeRates = rates;
        }
        
        public int Id { get; private set; }
        public string Name { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; }
    }
}
