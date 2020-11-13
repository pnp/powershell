using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class AddViewTests
    {
        #region Test Setup/CleanUp
        private static string listTitle;
        private static string viewTitle;

        private static PSTestScope scope;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            using (var context = TestCommon.CreateClientContext())
            {
                listTitle = $"TempList {Guid.NewGuid()}";
                context.Web.CreateList(ListTemplateType.GenericList, listTitle, false);
                viewTitle = "TempView";
            }
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
        public void AddPnPViewTest()
        {
            var fields = new List<string>();
            fields.Add("Title");
            var results = scope.ExecuteCommand("Add-PnPView",
                new CommandParameter("List", listTitle),
                new CommandParameter("Title", viewTitle),
                new CommandParameter("Fields", fields));

            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
