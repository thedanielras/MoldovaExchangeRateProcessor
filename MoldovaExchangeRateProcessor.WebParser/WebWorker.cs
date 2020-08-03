using System;
using System.Net;
using System.Net.Http;

namespace MoldovaExchangeRateProcessor.WebParser
{
    public class WebWorker
    {
        private HttpClient httpClient;

        public WebWorker()
        {
            httpClient = new HttpClient();
        }

        public string GetHtml(string url) {
            string responseBody = httpClient.GetStringAsync(url).GetAwaiter().GetResult();

            return responseBody;
        }
    }
}
