using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class AddViewTests : PnPTest
    {
        #region Test Setup/CleanUp
        private static string listTitle;
        private static string viewTitle;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            listTitle = $"TempList {Guid.NewGuid()}";
            viewTitle = "TestView";
            TestScope.ExecuteCommand("New-PnPList",
                new CommandParameter("Title", listTitle),
                new CommandParameter("Template", ListTemplateType.GenericList));
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
        public void AddPnPViewTest()
        {
            var fields = new List<string>();
            fields.Add("Title");
            var results = TestScope.ExecuteCommand("Add-PnPView",
                new CommandParameter("List", listTitle),
                new CommandParameter("Title", viewTitle),
                new CommandParameter("Fields", fields));

            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
