using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoldovaExchangeRateProcessor.WebParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot.Data
{
    class SqlServerRepo : IRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly ILogger _logger;

        public SqlServerRepo(SqlServerDbContext context, ILogger<SqlServerRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<ExchangeRate> GetExchangeRatesByBankName(string bankName)
        {
            var rates = _context.ExchangeRates.ToList<ExchangeRate>()
                .Where(e => e.Bank.Name == bankName).ToList<ExchangeRate>();
            return rates;
        }

        public List<Bank> GetBanksAndRelativeExchangeRatesByDate(DateTime date)
        {
            List<Bank> banks = null;
            try
            {
                banks = _context.Banks.ToList();

                foreach (var bank in banks)
                {
                    _context.Entry(bank)
                          .Collection(b => b.ExchangeRates)
                          .Query()
                          .Where(e => e.Date.Date == date.Date)
                          .Load();
                }               
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocuured while pulling from db: {0}", ex.Message);
            }

            return banks;
        }
    }
}
