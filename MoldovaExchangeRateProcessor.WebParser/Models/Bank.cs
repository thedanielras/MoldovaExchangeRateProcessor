using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.WebParser.Models
{
    public abstract class Bank
    {
        public string Name { get; private set; }
        public string WebUrl { get; private set; }

        protected string webHtmlContent { get; set; }
        protected List<ExchangeRate> exchangeRates;
        private IWebWorker webWorker { get; set; }

        public Bank(string name, string webUrl, IWebWorker webWorker)
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
        public abstract List<ExchangeRate> GetExchangeRates();
    }
}
