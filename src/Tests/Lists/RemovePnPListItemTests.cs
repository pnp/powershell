using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using System.Collections;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class RemoveListItemTests
    {
        #region Test Setup/CleanUp
        private static string listTitle;

        private static int itemId;
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

            var values = new Hashtable();
            values.Add("Title", "Test Item");
            var results = scope.ExecuteCommand("Add-PnPListItem",
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
            scope.ExecuteCommand("Remove-PnPList",
                new CommandParameter("Identity", listTitle),
                new CommandParameter("Force"));
            scope?.Dispose();
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void RemovePnPListItemTest()
        {
            var results = scope.ExecuteCommand("Remove-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Identity", itemId),
                new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
    }
    #endregion
}
