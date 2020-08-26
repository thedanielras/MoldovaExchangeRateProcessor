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
            if (CheckIfRateAlreadyIsInDb(rate))
            {
                _logger.LogInformation("This Rate already exists in the db");
                return;
            }

            var bank = GetBankByName(rate.Bank.Name);
            if (bank != null) bank.ExchangeRates.Add(rate);
            else
            {
                _context.Banks.Add(rate.Bank);
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
        
        private bool CheckIfRateAlreadyIsInDb(ExchangeRate rate)
        {
            var bank = GetBankByName(rate.Bank.Name);
            if (bank == null) return false;

            return bank.ExchangeRates.Any(e => e.Date.Date == rate.Date.Date && e.Currency == rate.Currency);
        } 
    }
}
