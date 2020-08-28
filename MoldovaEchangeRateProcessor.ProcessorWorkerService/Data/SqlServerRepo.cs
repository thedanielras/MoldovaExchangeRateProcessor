using AngleSharp.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoldovaExchangeRateProcessor.WebParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoldovaEchangeRateProcessor.ProcessorWorkerService.Data
{
    public class SqlServerRepo : IRepository
    {
        private readonly SqlServerDbContext _context;
        private readonly ILogger<SqlServerRepo> _logger;

        public SqlServerRepo(SqlServerDbContext context, ILogger<SqlServerRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<ExchangeRate> GetExchangeRateByDate(DateTime date)
        {
            var rates = _context.ExchangeRates.Where(e => e.Date.Date == date.Date);

            return rates;
        }

        public void AddExchangeRate(ExchangeRate rate)
        {
            ExchangeRate dbEntity = TryGetRateFromDb(rate);
            if (dbEntity != null)
            {
                _logger.LogInformation("This Rate already exists in the db");

                //Update db entity
                dbEntity.BuyRate = rate.BuyRate;
                dbEntity.SellRate = rate.SellRate;

                _context.ExchangeRates.Update(dbEntity);              
            }
            else
            {
                var bank = GetBankByName(rate.Bank.Name);
                if (bank != null) bank.ExchangeRates.Add(rate);
                else
                {
                    _context.Banks.Add(rate.Bank);
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error saving changes to DB \n{0}", ex.Message);
            }
        }

        public Bank GetBankByName(string bankName)
        {
            return _context.Banks.Where(b => b.Name == bankName).Include(b => b.ExchangeRates).FirstOrDefault();
        }    

        private ExchangeRate TryGetRateFromDb(ExchangeRate rate)
        {
            var bank = GetBankByName(rate.Bank.Name);
            if (bank == null) return null;

            return bank.ExchangeRates.Where(e => e.Date.Date == rate.Date.Date && e.Currency == rate.Currency).FirstOrDefault();
        }
    }
}
