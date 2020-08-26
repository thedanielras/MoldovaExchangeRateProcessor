using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldovaExchangeRateProcessor.WebParser.Models
{
    public abstract class BankProcessor
    {
        public string Name { get; private set; }
        public string WebUrl { get; private set; }
        protected string webHtmlContent { get; set; }
        private IWebWorker webWorker { get; set; }
        public BankProcessor(string name, string webUrl, IWebWorker webWorker)
        {
            Name = name;
            WebUrl = webUrl;
            this.webWorker = webWorker;

            setWebContent();
        }
        private void setWebContent()
        {
            string html = webWorker.GetHtml(WebUrl);

            webHtmlContent = html;
        }
        public abstract Task<List<ExchangeRate>> GetExchangeRatesAsync();
        public async Task<Bank> ProcessAsync()
        {
            var rates = await GetExchangeRatesAsync();
            var bankModel = new Bank(this.Name, rates);

            rates.ForEach(r => r.Bank = bankModel);

            return bankModel;
        }
        protected ExchangeRate GetExchangeRateFromDocumentByQuery(
            IDocument document, 
            string buyRateQuerySelector, 
            string sellRateQuerySelector,
            ExchangeRateCurrency currecy)
        {
            ExchangeRate rate = null;

            try
            {
                double buyRate = 0d;
                double sellRate = 0d;

                buyRate = Convert.ToDouble(document.QuerySelector(buyRateQuerySelector).Text());
                sellRate = Convert.ToDouble(document.QuerySelector(sellRateQuerySelector).Text());

                rate = new ExchangeRate(buyRate, sellRate, currecy);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rate;
        }
    }
}
