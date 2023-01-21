/////////////////
Created by: Jerry Ye
Alias: ziweiye
Date: 2023-01-21T22:07:33Z

/////////////////
This is a durable function app sample with Pattern "Fan out/fan in". 
https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview?tabs=csharp#fan-in-out

It leverages Durable Functions Monitor (aka DfMon) "Injected" mode. 
https://github.com/microsoft/DurableFunctionsMonitor/blob/main/durablefunctionsmonitor.dotnetbackend/NUGET_README.md

/////////////////
Some key settings in this repo: 
- <DurableFunctionsMonitorRoutePrefix>monitor</DurableFunctionsMonitorRoutePrefix> in .csproj file. 
- "AzureFunctionsJobHost__extensions__durableTask__hubName": "LocalDurableHub" in local.settings.json. 
- DfmEndpoint.Setup(new DfmSettings { DisableAuthentication = true }); in Startup.cs. 