using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.WebParser.Models.BankProcessors
{
    public class Victoriabank : BankProcessor
    {
        public Victoriabank() : base("Victoriabank", "https://www.victoriabank.md/en/", WebWorker.GetInstance())
        {

        }

        public override async Task<List<ExchangeRate>> GetExchangeRatesAsync()
        {         
            List<ExchangeRate> rates = new List<ExchangeRate>();

            var context = BrowsingContext.New(Configuration.Default);

            //Create a document from a virtual request / response pattern
            var document = await context.OpenAsync(req => req.Content(webHtmlContent));

            var usdRate = GetExchangeRateByWebSiteTableColumn(document, 1, ExchangeRateCurrency.USD);
            if (usdRate != null && usdRate.IsCorrect)
                rates.Add(usdRate);

            var eurRate = GetExchangeRateByWebSiteTableColumn(document, 2, ExchangeRateCurrency.EUR);
            if (eurRate != null && eurRate.IsCorrect)
                rates.Add(eurRate);

            var rubRate = GetExchangeRateByWebSiteTableColumn(document, 3, ExchangeRateCurrency.RUB);
            if (rubRate != null && rubRate.IsCorrect)
                rates.Add(rubRate);

            var ronRate = GetExchangeRateByWebSiteTableColumn(document, 4, ExchangeRateCurrency.RON);
            if (ronRate != null && ronRate.IsCorrect)
                rates.Add(ronRate);

            return rates;
        }

        private ExchangeRate GetExchangeRateByWebSiteTableColumn(IDocument document, int numComlumn, ExchangeRateCurrency currency)
        {
            ExchangeRate rate = null;

            string buyRateQuery = $"div.currency-tab-content table tr:nth-child({numComlumn}) td:nth-child(2)";
            string sellRateQuery = $"div.currency-tab-content table tr:nth-child({numComlumn}) td:nth-child(3)";

            rate = GetExchangeRateFromDocumentByQuery(document, buyRateQuery, sellRateQuery, currency);

            return rate;
        }
    }
}
