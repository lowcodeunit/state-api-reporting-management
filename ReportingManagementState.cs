using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LCU.StateAPI.Utilities;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using LCU.Presentation.State.ReqRes;
using LCU.State.API.LowCodeUnit.Reporting.Management.Models;

namespace LCU.State.API.LowCodeUnit.Reporting.Management
{
    [Serializable]
    public class ReportingManagementState
    {
        #region Constants
        public const string HUB_NAME = "reportingmanagement";
        #endregion

        #region Properties 
        public virtual PowerBIModel PowerBI { get; set; }

        public virtual StateDetails StateDetails { get; set; }
        #endregion

        #region Constructors
        public ReportingManagementState(StateDetails stateDetails)
        {
            this.StateDetails = stateDetails;
        }
        #endregion

        #region API Methods
        public virtual void SavePowerBIConnection(PowerBIModel powerBi)
        {
            PowerBI = powerBi;
        }
        #endregion
    }

    public static class ReportingManagementStateEntity
    {
        [FunctionName("ReportingManagementState")]
        public static void Run([EntityTrigger] IDurableEntityContext ctx, ILogger log)
        {
            var action = ctx.OperationName.ToLowerInvariant();

            var state = ctx.GetState<ReportingManagementState>();

            if (action == "$init" && state == null)
                state = new ReportingManagementState(ctx.GetInput<StateDetails>());

            switch (action)
            {
                case "savepowerbiconnection":
                    var actionReq = ctx.GetInput<ExecuteActionRequest>();

                    state.SavePowerBIConnection(actionReq.Arguments.Metadata["PowerBI"].ToObject<PowerBIModel>());
                    break;
            }

            ctx.SetState(state);

            ctx.StartNewOrchestration("SendState", state);

            // ctx.Return(state);
        }
    }
}
