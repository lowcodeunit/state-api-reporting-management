using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Fathym;
using LCU.StateAPI;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LCU.State.API.LowCodeUnit.Reporting.Management
{
    public static class SavePowerBIConnection
    {
        [FunctionName("SavePowerBIConnection")]
        public static async Task<Status> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext ctx)
        {
            var actArgs = ctx.GetInput<ExecuteActionArguments>();

            var entityId = new EntityId(typeof(ReportingManagementState).Name, actArgs.StateDetails.Username);

            await ctx.CallEntityAsync<object>(entityId, "SavePowerBIConnection", new
            {
                PowerBIConnection = "xcv"
            });

            return Status.Success;
        }

        // [FunctionName("SetPowerBIConnection_Hello")]
        // public static string SayHello([ActivityTrigger] string name, ILogger log)
        // {
        //     log.LogInformation($"Saying hello to {name}.");
        //     return $"Hello {name}!";
        // }
    }
}