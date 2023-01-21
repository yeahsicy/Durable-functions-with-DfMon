using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp_durable_fan_out_and_in
{
    public static class Function1
    {
        [FunctionName("HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            string instanceId = await starter.StartNewAsync("Orchestrator", null);
            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
            return starter.CreateCheckStatusResponse(req, instanceId);
        }
        [FunctionName("Orchestrator")]
        public static async Task<string[]> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var parallelTasks = new List<Task<string>>();

            for (int i = 0; i < 5; i++)
                parallelTasks.Add(context.CallActivityAsync<string>(nameof(Process), $"Job {i}"));

            return await Task.WhenAll(parallelTasks);
        }

        [FunctionName(nameof(Process))]
        public static string Process([ActivityTrigger] string name, ILogger log)
        {
            var r = new Random();
            var t = r.Next(10);
            Thread.Sleep(t * 1000);

            log.LogInformation($"{name} spent {t}s to complete. ");


            return $"{name}: {t}s";
        }
    }
}