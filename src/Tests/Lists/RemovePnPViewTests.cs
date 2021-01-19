using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class RemoveViewTests : PnPTest
    {
        #region Test Setup/CleanUp
        private static string listTitle;
        private static string viewTitle;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            listTitle = $"TempList {Guid.NewGuid()}";
            TestScope.ExecuteCommand("New-PnPList",
                new CommandParameter("Title", listTitle),
                new CommandParameter("Template", Microsoft.SharePoint.Client.ListTemplateType.GenericList));

            viewTitle = "TempView";
            TestScope.ExecuteCommand("Add-PnPView",
                new CommandParameter("List", listTitle),
                new CommandParameter("Title", viewTitle),
                new CommandParameter("Fields", new string[] { "Title" }));
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            TestScope.ExecuteCommand("Remove-PnPList",
                new CommandParameter("Identity", listTitle),
                new CommandParameter("Force"));
        }
        #endregion
        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void RemovePnPViewTest()
        {
            var results = TestScope.ExecuteCommand("Remove-PnPView",
                new CommandParameter("Identity", viewTitle),
                new CommandParameter("List", listTitle),
                new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
        #endregion
    }
}
