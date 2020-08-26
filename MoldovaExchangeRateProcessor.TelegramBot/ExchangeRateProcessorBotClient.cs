using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoldovaExchangeRateProcessor.TelegramBot.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MoldovaExchangeRateProcessor.TelegramBot
{
    class ExchangeRateProcessorBotClient
    {
        private static ExchangeRateProcessorBotClient _instance = null;
        private ITelegramBotClient _botClient;
        private IBotBusinessLogic _botLogic;
        private ILogger _logger;
        private IConfiguration _config;
        private IRepository _repository;

        private ExchangeRateProcessorBotClient(IBotClientArgs botArgs)
        {
            _logger = botArgs.Logger;
            _config = botArgs.Config;
            _repository = botArgs.Repository;

            _botClient = new TelegramBotClient(botArgs.ApiKey);
            _logger.LogInformation("Bot Client Intialized!");
            _botClient.OnMessage += BotOnMessage;
            _botLogic = new ExchangeRateBotBusinessLogic(botArgs);
            _instance = this;
        }

        public static ExchangeRateProcessorBotClient GetBotClient(BotClientArgs botArgs)
        {
            return _instance ?? new ExchangeRateProcessorBotClient(botArgs);
        }

        public void StopRecieving() => _botClient.StopReceiving();
        public void StartRecieving()
        {
            _botClient.StartReceiving();
            _logger.LogInformation("Bot Client Started Recieving Messages!");
        }

        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null && e.Message.Text.StartsWith("/"))
            {
                _logger.LogInformation("Received the command {0}", e.Message.Text);
                string response = "";
                try
                {
                    response = await _botLogic.RunCommandAsync(e.Message.Text.Substring(1));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return;
                }

                SendMessage(response, e.Message.Chat.Id);
            }
            else
            {
                SendMessage("Type /help for the list of available commands.", e.Message.Chat.Id);
            }
        }

        private async void SendMessage(string message, long chatId)
        {
             await _botClient.SendTextMessageAsync(
                 chatId: chatId,
                 text: message
            );
        }
    }
}
