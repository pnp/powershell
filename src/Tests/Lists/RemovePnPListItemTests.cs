using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using System.Collections;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class RemoveListItemTests : PnPTest
    {
        #region Test Setup/CleanUp
        private static string listTitle;

        private static int itemId;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            listTitle = $"TempList {Guid.NewGuid()}";
            TestScope.ExecuteCommand("New-PnPList",
                new CommandParameter("Title", listTitle),
                new CommandParameter("Template", Microsoft.SharePoint.Client.ListTemplateType.GenericList));

            var values = new Hashtable();
            values.Add("Title", "Test Item");
            var results = TestScope.ExecuteCommand("Add-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Values", values));
            if (results.Count > 0)
            {
                itemId = ((Microsoft.SharePoint.Client.ListItem)results[0].BaseObject).Id;
            }

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
        public void RemovePnPListItemTest()
        {
            var results = TestScope.ExecuteCommand("Remove-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Identity", itemId),
                new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
    }
    #endregion
}
