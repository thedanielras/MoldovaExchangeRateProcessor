using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.WebParser.Models
{
    public interface IWebWorker
    {
        public string GetHtml(string url);
    }
}
