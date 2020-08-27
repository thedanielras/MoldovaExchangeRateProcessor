using System;
using System.Collections.Generic;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using AngleSharp;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.WebParser.Models.BankProcessors
{
    public class MoldovaAgroindbank : BankProcessor
    {
        public MoldovaAgroindbank() : base("Moldova AgroindBank", "https://www.maib.md/", WebWorker.GetInstance())
        {

        }

        public override async Task<List<ExchangeRate>> GetExchangeRatesAsync()
        {
            List<ExchangeRate> rates = new List<ExchangeRate>();

            var context = BrowsingContext.New(Configuration.Default);

            //Create a document from a virtual request / response pattern
            var document = await context.OpenAsync(req => req.Content(webHtmlContent));

            var usdRate = GetExchangeRateByWebSiteTableColumn(document, 1, ExchangeRateCurrency.USD);
            if (usdRate != null)
                rates.Add(usdRate);

            var eurRate = GetExchangeRateByWebSiteTableColumn(document, 2, ExchangeRateCurrency.EUR);
            if (eurRate != null)
                rates.Add(eurRate);

            var rubRate = GetExchangeRateByWebSiteTableColumn(document, 3, ExchangeRateCurrency.RUB);
            if (rubRate != null)
                rates.Add(rubRate);

            var ronRate = GetExchangeRateByWebSiteTableColumn(document, 4, ExchangeRateCurrency.RON);
            if (ronRate != null)
                rates.Add(ronRate);

            return rates;       
        }

        private ExchangeRate GetExchangeRateByWebSiteTableColumn(IDocument document, int numComlumn, ExchangeRateCurrency currency)
        {
            ExchangeRate rate = null;

            string buyRateQuery = $"div.currencies table tr:nth-child({numComlumn}) td:nth-child(2)";
            string sellRateQuery = $"div.currencies table tr:nth-child({numComlumn}) td:nth-child(3)";

            rate = GetExchangeRateFromDocumentByQuery(document, buyRateQuery, sellRateQuery, currency);

            return rate;
        }
    }
}
