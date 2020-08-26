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
            var rates = _repo.GetExchangeRatesByDate(DateTime.Now);

            StringBuilder result = new StringBuilder();

            if (rates.Count == 0) result.Append("Whoops, somenthing went wrong, try again later.");

            var ratesGroupedByBankName = rates.GroupBy(r => r.Bank.Name);

            bool firstIterationFlag = true;
            foreach (var bank in ratesGroupedByBankName)
            {
                if (firstIterationFlag) result.Append("\n");

                string bankName = bank.Key;
                result.Append($"{ bankName }\n");

                foreach(var rate in bank)
                {
                    result.Append(rate.ToString());
                    result.Append('\n');
                }

                result.Append('\n');
                firstIterationFlag = false;
            }

            return Task.Run(() => result.ToString());
        }
    }
}
