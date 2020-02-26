using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LCU.StateAPI;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Security.Claims;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace LCU.State.API.LowCodeUnit.Reporting.Management
{
    public static class ConnectToState
    {
        [FunctionName("ConnectToState")]
        public static async Task<ConnectToStateResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequest req, ILogger logger, 
            ClaimsPrincipal claimsPrincipal, [DurableClient] IDurableEntityClient entity,
            [SignalR(HubName = ReportingManagementState.HUB_NAME)]IAsyncCollector<SignalRGroupAction> signalRGroupActions)
        {
            return await entity.ConnectToState<ReportingManagementState>(req, logger, claimsPrincipal, signalRGroupActions);
        }
    }
}
