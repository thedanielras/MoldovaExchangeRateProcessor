using System;
using MoldovaExchangeRateProcessor.WebParser;
using MoldovaExchangeRateProcessor.WebParser.Models;
using MoldovaExchangeRateProcessor.WebParser.Models.Banks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    class Program
    {
        static void Main()
        {
            var botClient = ExchangeRateProcessorBotClient.GetBotClient();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            botClient.StopRecieving();
        }
    }
}
