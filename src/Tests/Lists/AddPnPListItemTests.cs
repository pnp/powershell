using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Collections;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class AddListItemTests
    {
        #region Test Setup/CleanUp
        private static string listTitle;
        private static PSTestScope scope;

        // #region Setup
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
        [TestMethod]
        public void AddPnPListItemTest()
        {
            Hashtable values = new Hashtable();
            values.Add("Title", "Test Item");

            var results = scope.ExecuteCommand("Add-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Values", values));

            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
