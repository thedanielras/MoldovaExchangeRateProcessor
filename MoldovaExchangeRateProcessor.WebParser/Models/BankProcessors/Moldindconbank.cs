using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.WebParser.Models.BankProcessors
{
    public class Moldindconbank : BankProcessor
    {
        public Moldindconbank() : base("Moldindconbank", "https://www.micb.md/", WebWorker.GetInstance())
        {

        }

        public async override Task<List<ExchangeRate>> GetExchangeRatesAsync()
        {
            List<ExchangeRate> rates = new List<ExchangeRate>();

            var context = BrowsingContext.New(Configuration.Default);

            //Create a document from a virtual request / response pattern
            var document = await context.OpenAsync(req => req.Content(webHtmlContent));
                      
            var usdRate = GetExchangeRateByWebSiteTableColumn(document, 1, ExchangeRateCurrency.USD);
            rates.Add(usdRate);
            var eurRate = GetExchangeRateByWebSiteTableColumn(document, 2, ExchangeRateCurrency.EUR);
            rates.Add(eurRate);
            var rubRate = GetExchangeRateByWebSiteTableColumn(document, 3, ExchangeRateCurrency.RUB);
            rates.Add(rubRate);
            var ronRate = GetExchangeRateByWebSiteTableColumn(document, 4, ExchangeRateCurrency.RON);
            rates.Add(ronRate);

            return rates;
        }

        private ExchangeRate GetExchangeRateByWebSiteTableColumn(IDocument document, int numComlumn, ExchangeRateCurrency currency)
        {
            ExchangeRate rate = null;

            string buyRateQuery = $"#currancy-rates table tbody tr:nth-child({numComlumn}) td:nth-child(2)";
            string sellRateQuery = $"#currancy-rates table tbody tr:nth-child({numComlumn}) td:nth-child(3)";

            rate = GetExchangeRateFromDocumentByQuery(document, buyRateQuery, sellRateQuery, currency);

            return rate;
        }
    }
}
