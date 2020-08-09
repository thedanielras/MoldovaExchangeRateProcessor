using System;
using System.Collections.Generic;
using System.Text;

namespace MoldovaExchangeRateProcessor.WebParser.Utils
{
    public static class WebParserUtils
    {
        public static string Substr (string source, string fromOccurrence, string toOccurrence)
        {
            int startIndex, endIndex, contentLength;

            startIndex = source.IndexOf(fromOccurrence);
            endIndex = source.IndexOf(toOccurrence);

            if (startIndex == -1 || endIndex == -1)
                throw new Exception("Invalid source Exception");

            contentLength = endIndex - startIndex;

            return source.Substring(startIndex, contentLength);
        }
    }
}
