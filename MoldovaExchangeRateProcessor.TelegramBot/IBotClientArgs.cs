using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoldovaExchangeRateProcessor.TelegramBot.Data;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    interface IBotClientArgs
    {
        string ApiKey { get; }
        IConfiguration Config { get; }
        ILogger Logger { get; }
        IRepository Repository { get; }
    }
}