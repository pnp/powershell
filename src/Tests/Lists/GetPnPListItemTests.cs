using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using System.Collections;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class GetListItemTests : PnPTest
    {
        private static string listTitle;
        private static int itemId;

        #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            listTitle = $"TempList {Guid.NewGuid()}";

            var values = new Hashtable();
            values.Add("Title", "Test Item");
            TestScope.ExecuteCommand("New-PnPList",
            new CommandParameter("Title", listTitle),
            new CommandParameter("Template", ListTemplateType.GenericList)
            );

            var results = TestScope.ExecuteCommand("Add-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Values", values));

            if (results.Count > 0)
            {
                itemId = ((ListItem)results[0].BaseObject).Id;
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
        public void GetPnPListItemTest()
        {

            var results = TestScope.ExecuteCommand("Get-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Id", itemId));

            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
