using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Security.Claims;

namespace LCU.State.API.LowCodeUnit.Reporting.Management
{
    public static class ExecuteAction
    {
        [FunctionName("ExecuteAction")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Admin, "post", Route = null)] HttpRequest req,
            ClaimsPrincipal claimsPrincipal, ILogger log, [DurableClient] IDurableOrchestrationClient actions)
        {
            return await actions.ExecuteAction(req, claimsPrincipal, log);
        }
    }
}
