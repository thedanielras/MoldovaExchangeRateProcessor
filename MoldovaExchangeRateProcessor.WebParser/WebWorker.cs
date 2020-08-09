using MoldovaExchangeRateProcessor.WebParser.Models;
using System;
using System.Net;
using System.Net.Http;

namespace MoldovaExchangeRateProcessor.WebParser
{
    public class WebWorker : IWebWorker
    {
        private HttpClient httpClient;

        public WebWorker()
        {
            httpClient = new HttpClient();
        }

        public string GetHtml(string url)
        {
            string responseBody = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
            string htmlContent = Utils.WebParserUtils.Substr(responseBody, "<html", "</html");

            return htmlContent;
        }
    }
}
