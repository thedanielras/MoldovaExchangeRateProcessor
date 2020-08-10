using MoldovaExchangeRateProcessor.WebParser.Models;
using MoldovaExchangeRateProcessor.WebParser.Models.Banks;
using System;
using System.Collections.Generic;
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
            int i = 0;
            foreach (var bank in banks)
            {               
                var exchangeRates = bank.GetExchangeRates();
                if (i > 0) result.Append("\n");
                result.Append($"Bank Name: { bank.Name }\n");

                foreach (var exchangeRate in exchangeRates)
                {
                    result.Append(exchangeRate.ToString());
                    result.Append('\n');
                }

                i++;
            }


            return result.ToString();
        }
    }
}
