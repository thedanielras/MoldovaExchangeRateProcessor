using MoldovaExchangeRateProcessor.WebParser.Models;
using MoldovaExchangeRateProcessor.WebParser.Models.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    class GetExchangeRateCommand : ICommand
    {
        private List<Bank> banks;

        public GetExchangeRateCommand(List<Bank> banks)
        {
            this.banks = banks;
        }

        public string Execute()
        {
            StringBuilder result = new StringBuilder();

            var exchangeRates = banks.SelectMany((bank, index) =>
            {
                if (index > 0) result.Append("\n");
                result.Append($"Bank Name: { bank.Name }\n");
                return bank.GetExchangeRates();
            });

            foreach (var rate in exchangeRates)
            {
                result.Append(rate.ToString());
                result.Append('\n');
            }

            return result.ToString();
        }
    }
}
