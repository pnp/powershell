using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class RequestReIndexListTests
    {
        #region Test Setup/CleanUp
        private static string listTitle;

        private static PSTestScope scope;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            scope = new PSTestScope();
            listTitle = $"TempList {Guid.NewGuid()}";
            scope.ExecuteCommand("New-PnPList",
                new CommandParameter("Title", listTitle),
                new CommandParameter("Template", Microsoft.SharePoint.Client.ListTemplateType.GenericList));
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            scope.ExecuteCommand("Remove-PnPList",
                new CommandParameter("Identity", listTitle),
                new CommandParameter("Force"));
            scope?.Dispose();
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void RequestPnPReIndexListTest()
        {
            var results = scope.ExecuteCommand("Request-PnPReIndexList",
                new CommandParameter("Identity", listTitle));

            Assert.AreEqual(results.Count, 0);
        }
        #endregion
    }
}
