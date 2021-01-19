using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using System.Collections;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class RemoveListTests : PnPTest
    {
        #region Test Setup/CleanUp
        private static string listTitle;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            listTitle = $"TempList {Guid.NewGuid()}";
            TestScope.ExecuteCommand("New-PnPList",
                new CommandParameter("Title", listTitle),
                new CommandParameter("Template", Microsoft.SharePoint.Client.ListTemplateType.GenericList));
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
        public void RemovePnPListTest()
        {
            var results = TestScope.ExecuteCommand("Remove-PnPList",
                new CommandParameter("Identity", listTitle),
                new CommandParameter("Force"));

            Assert.IsNotNull(results);
        }
        #endregion
    }
}
