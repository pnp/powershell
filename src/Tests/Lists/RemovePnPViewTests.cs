using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class RemoveViewTests
    {
        #region Test Setup/CleanUp
        private static string listTitle;
        private static string viewTitle;
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

            viewTitle = "TempView";
            scope.ExecuteCommand("Add-PnPView",
                new CommandParameter("List", listTitle),
                new CommandParameter("Title", viewTitle),
                new CommandParameter("Fields", new string[] { "Title" }));
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
        public void RemovePnPViewTest()
        {
            var results = scope.ExecuteCommand("Remove-PnPView",
                new CommandParameter("Identity", viewTitle),
                new CommandParameter("List", listTitle),
                new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
        #endregion
    }
}
