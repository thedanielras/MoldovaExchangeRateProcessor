FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /source
#copy project files
COPY MoldovaExchangeRateProcessor.sln .
COPY MoldovaExchangeRateProcessor.ProcessorWorkerService ./MoldovaExchangeRateProcessor.ProcessorWorkerService
COPY MoldovaExchangeRateProcessor.WebParser ./MoldovaExchangeRateProcessor.WebParser
COPY sh/. ./MoldovaExchangeRateProcessor.ProcessorWorkerService/

#install 'dotnet-ef' tool
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

#build up
WORKDIR /source/MoldovaExchangeRateProcessor.ProcessorWorkerService
RUN chmod +x wait-for-it.sh
RUN chmod +x startup.sh
RUN dotnet publish -o /release/MoldovaExchangeRateProcessor.ProcessorWorkerService