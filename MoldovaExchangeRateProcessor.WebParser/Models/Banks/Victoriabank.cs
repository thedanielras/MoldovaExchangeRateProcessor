using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.WebParser.Models.Banks
{
    public class Victoriabank : Bank
    {

        public Victoriabank() : base("Victoriabank", "https://www.victoriabank.md/en/", new WebWorker())
        {

        }

        public override List<ExchangeRate> GetExchangeRates()
        {
            if (this.exchangeRates != null)
                return this.exchangeRates;

            List<ExchangeRate> rates = new List<ExchangeRate>();

            var context = BrowsingContext.New(Configuration.Default);

            //Create a document from a virtual request / response pattern
            var document = context.OpenAsync(req => req.Content(webHtmlContent)).GetAwaiter().GetResult();

            try
            {
                double euroBuyRate = 0d;
                double euroSellRate = 0d;

                euroBuyRate = Convert.ToDouble(document.QuerySelector("div.currency-tab-content table tr:nth-child(2) td:nth-child(2)").Text());
                euroSellRate = Convert.ToDouble(document.QuerySelector("div.currency-tab-content table tr:nth-child(2) td:nth-child(3)").Text());

                rates.Add(new ExchangeRate(euroBuyRate, euroSellRate, ExchangeRateCurrency.EUR));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rates;
        }
    }
}
