using MoldovaExchangeRateProcessor.TelegramBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.TelegramBot.Commands
{
    class GetCurrentCommand : ICommand
    {
        private readonly IRepository _repo;

        public GetCurrentCommand(IBotClientArgs botArgs)
        {
            _repo = botArgs.Repository;
        }

        public Task<string> ExecuteAsync()
        {
            var banks = _repo.GetBanksAndRelativeExchangeRatesByDate(DateTime.Now);

            StringBuilder result = new StringBuilder();

            if (banks.Count == 0) result.Append("Whoops, somenthing went wrong, try again later.");                     

            bool firstIterationFlag = true;
            foreach (var bank in banks)
            {
                if (firstIterationFlag) result.Append("\n");

                string bankName = bank.Name;
                result.Append($"{ bankName }\n");

                if(bank.ExchangeRates != null && bank.ExchangeRates.Count > 0)
                {
                    foreach (var rate in bank.ExchangeRates)
                    {
                        result.Append(rate.ToString());
                        result.Append('\n');
                    }
                } else
                {
                    result.Append("Not available for today!\n");
                }               

                result.Append('\n');
                firstIterationFlag = false;
            }

            return Task.Run(() => result.ToString());
        }
    }
}
