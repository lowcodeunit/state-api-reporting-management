using Fathym.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace state_api_reporting_management_tests
{
    [TestClass]
    public class SavePowerBIConnectionTests : AzFunctionTestBase
    {
        
        public SavePowerBIConnectionTests() : base()
        {
            APIRoute = "api/SavePowerBIConnection";                
        }

        [TestMethod]
        public async Task TestSavePowerBIConnection()
        {
            LcuEntApiKey = "";            
            PrincipalId = "";

            addRequestHeaders();

            var url = $"{HostURL}/{APIRoute}";            

            var response = await httpGet(url); 

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var model = getContent<dynamic>(response);

            dynamic result = model.Result;            

            throw new NotImplementedException("Implement me!");                  
        }
    }
}
