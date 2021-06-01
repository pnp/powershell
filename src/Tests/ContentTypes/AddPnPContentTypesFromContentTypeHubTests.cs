using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.ContentTypes
{
    [TestClass]
    public class AddContentTypesFromContentTypeHubTests : PnPTest
    {
        #region Test Setup/CleanUp
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
             // This runs on class level once before all tests run
            //using (var ctx = TestCommon.CreateClientContext())
            //{
            //}
        }
        #endregion
        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void AddPnPContentTypesFromContentTypeHubTest()
        {
            using (var scope = new PSTestScope(true))
            {
                var contentTypes = new List<string>();
                contentTypes.Add("0x0101");
                var results = scope.ExecuteCommand("Add-PnPContentTypesFromContentTypeHub",
                    new CommandParameter("ContentTypes", contentTypes));

                Assert.IsNotNull(results);
            }
        }
        #endregion
    }
}
            