FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /source
#copy project files
COPY MoldovaExchangeRateProcessor.sln .
COPY MoldovaExchangeRateProcessor.TelegramBot ./MoldovaExchangeRateProcessor.TelegramBot
COPY MoldovaExchangeRateProcessor.WebParser ./MoldovaExchangeRateProcessor.WebParser
COPY sh/. ./MoldovaExchangeRateProcessor.TelegramBot/

#build up
WORKDIR /source/MoldovaExchangeRateProcessor.TelegramBot
RUN chmod +x wait-for-it.sh
RUN dotnet publish -o /release/MoldovaExchangeRateProcessor.TelegramBot