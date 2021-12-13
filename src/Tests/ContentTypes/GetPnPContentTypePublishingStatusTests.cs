using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.ContentTypes
{
    [TestClass]
    public class GetPnPContentTypePublishingStatusTests : PnPTest
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
        public void GetPnPContentTypePublishingStatusTest()
        {
            using (var scope = new PSTestScope(true))
            {
                Microsoft.SharePoint.Client.Web web = ClientContext.Web;
                var contentType = web.ContentTypes.GetById("0x0101");
                var results = scope.ExecuteCommand("Get-PnPContentTypePublishingStatus",
                    new CommandParameter("ContentType", contentType));

                Assert.IsNotNull(results);
            }
        }
        #endregion
    }
}
            