using Microsoft.EntityFrameworkCore;
using MoldovaExchangeRateProcessor.WebParser.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.TelegramBot.Data
{
    class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {

        }
        
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Bank> Banks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<ExchangeRate>()
                 .Property(e => e.Currency)
                 .HasConversion(
                     v => v.ToString(),
                     v => (ExchangeRateCurrency)Enum.Parse(typeof(ExchangeRateCurrency), v));

            modelBuilder.Entity<ExchangeRate>()
                 .HasIndex(e => new { e.BankId, e.Date, e.Currency })
                 .IsUnique();

            modelBuilder.Entity<Bank>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
