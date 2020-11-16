using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;
using System.Collections;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class SetListItemTests
    {
        private static string listTitle;
        private static PSTestScope scope;

        private static int itemId;

        #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            using (var context = TestCommon.CreateClientContext())
            {
                listTitle = $"TempList {Guid.NewGuid()}";
                context.Web.CreateList(ListTemplateType.GenericList, listTitle, false);
            }
            // scope = new PSTestScope(true);
            scope = new PSTestScope();

            Hashtable values = new Hashtable();
            values.Add("Title", "Test Item");
            var results = scope.ExecuteCommand("Add-PnPListItem",
             new CommandParameter("List", listTitle),
             new CommandParameter("Values", values));

            if (results.Count > 0)
                itemId = ((ListItem)results[0].BaseObject).Id;
        }


        [ClassCleanup]
        public static void Cleanup()
        {
            using (var context = TestCommon.CreateClientContext())
            {
                var list = context.Web.Lists.GetByTitle(listTitle);
                list.DeleteObject();
                context.ExecuteQueryRetry();
            }
            scope?.Dispose();
        }
        #endregion


        #region Scaffolded Cmdlet Tests
        //TODO: This is a scaffold of the cmdlet - complete the unit test
        //[TestMethod]
        public void SetPnPListItemTest()
        {
            Hashtable values = new Hashtable();
            values.Add("Title", "Updated Item");
            var results = scope.ExecuteCommand("Set-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Identity", itemId),
                new CommandParameter("Values", values));

            Assert.AreEqual(results.Count, 1);

            var item = (ListItem)results[0].BaseObject;
            Assert.AreEqual(item["Title"], "Updated Item");
        }
        #endregion
    }
}

