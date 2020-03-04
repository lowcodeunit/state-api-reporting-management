using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using LCU.Presentation.State.ReqRes;
using Fathym;
using LCU.StateAPI;
using Microsoft.AspNetCore.Http;
using System;
using Fathym.API;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System.Runtime.Serialization;
using LCU.State.API.LowCodeUnit.Reporting.Management.Models;

namespace LCU.State.API.LowCodeUnit.Reporting.Management
{
    [Serializable]
    [DataContract]
    public class SavePowerBIConnectionRequest : PowerBIModel
    {
        // [DataMember]
        // public virtual string Country { get; set; }

        // [DataMember]
        // public virtual string FullName { get; set; }

        // [DataMember]
        // public virtual string Handle { get; set; }
    }
    public static class SavePowerBIConnection
    {
        [FunctionName("SavePowerBIConnection")]
        public static async Task<Status> Run([HttpTrigger] HttpRequest req, ILogger log,
            [SignalR(HubName = ReportingManagementState.HUB_NAME)]IAsyncCollector<SignalRMessage> signalRMessages,
            [Blob("state-api/{headers.lcu-ent-api-key}/{headers.lcu-hub-name}/{headers.x-ms-client-principal-id}/{headers.lcu-state-key}", FileAccess.ReadWrite)] CloudBlockBlob stateBlob)
        {
            return await stateBlob.WithStateAction<ReportingManagementState, SavePowerBIConnectionRequest>(req, signalRMessages, log, async (state, reportingConnReq) =>
            {
                log.LogInformation($"Executing SetUserDetails Action.");

                state.SavePowerBIConnection(reportingConnReq);//(reportingConnReq.Username, reportingConnReq.Password, reportingConnReq.ApiUrl, reportingConnReq.AuthorityUrl, reportingConnReq.ClientID, reportingConnReq.GroupID, reportingConnReq.ResourceUrl);

                return state;
            });
        }
    }
    
    
    
    
    
    // public static class SavePowerBIConnection
    // {
    //     [FunctionName("SavePowerBIConnection")]
    //     public static async Task<Status> RunOrchestrator(
    //         [OrchestrationTrigger] IDurableOrchestrationContext ctx)
    //     {
    //         var actArgs = ctx.GetInput<ExecuteActionArguments>();

    //         var entityId = new EntityId(typeof(ReportingManagementState).Name, actArgs.StateDetails.Username);

    //         await ctx.CallEntityAsync<object>(entityId, "SavePowerBIConnection", actArgs.ActionRequest);

    //         return Status.Success;
    //     }


    // }
}