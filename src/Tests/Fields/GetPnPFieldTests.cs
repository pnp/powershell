using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Fields
{
    [TestClass]
    public class GetFieldTests
    {
        private static PSTestScope scope;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            scope = new PSTestScope();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            scope?.Dispose();
        }

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void GetPnPFieldTest()
        {
            var results = scope.ExecuteCommand("Get-PnPField");
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetPnPFieldForListTest()
        {
            var listResults = scope.ExecuteCommand("Get-PnPList");
            if (listResults.Count > 0)
            {
                var list = listResults[0].BaseObject as Microsoft.SharePoint.Client.List;
                var results = scope.ExecuteCommand("Get-PnPField", new CommandParameter("List", list));
                Assert.IsTrue(results.Count > 0);
            }
        }
        #endregion
    }
}
