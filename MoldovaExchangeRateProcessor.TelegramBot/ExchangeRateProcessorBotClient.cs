using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    public class ExchangeRateProcessorBotClient
    {
        private const string botApiKey = "1330904374:AAGp0Pb4D5KbWc47n8OaSZoIJSCw3fib2og";
        private static ExchangeRateProcessorBotClient instance = null;
        private ITelegramBotClient botClient;
        private IBotBusinessLogic botLogic;
        private ExchangeRateProcessorBotClient()
        {
            botClient = new TelegramBotClient(botApiKey);

            Console.WriteLine(
              $"Bot Client Intialized!"
            );

            botClient.OnMessage += BotOnMessage;
            botClient.StartReceiving();

            botLogic = new ExchangeRateBotBusinessLogic();

            instance = this;
        }

        public static ExchangeRateProcessorBotClient GetBotClient()
        {
            return instance ?? new ExchangeRateProcessorBotClient();
        }

        public void StopRecieving() => botClient.StopReceiving();

        private void BotOnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null && e.Message.Text.StartsWith("/"))
            {
                Console.WriteLine($"Received the command {e.Message.Text}");
                string response = botLogic.RunCommand(e.Message.Text.Substring(1));
                SendMessage(response, e.Message.Chat.Id);
            }
            else
            {
                SendMessage("Type /help for the list of available commands.", e.Message.Chat.Id);
            }

        }

        private async void SendMessage(string message, long chatId)
        {
            await botClient.SendTextMessageAsync(
                 chatId: chatId,
                 text: message
            );
        }       
    }
}
