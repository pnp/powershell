using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.Collections;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class AddListItemTests : PnPTest
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
        public void AddPnPListItemTest()
        {
            Hashtable values = new Hashtable();
            values.Add("Title", "Test Item");

            var results = TestScope.ExecuteCommand("Add-PnPListItem",
                new CommandParameter("List", listTitle),
                new CommandParameter("Values", values));

            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
