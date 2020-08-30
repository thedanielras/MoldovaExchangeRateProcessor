#!/bin/bash
dotnet-ef database update
dotnet "/release/MoldovaExchangeRateProcessor.ProcessorWorkerService/MoldovaExchangeRateProcessor.ProcessorWorkerService.dll"