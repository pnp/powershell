using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class GetListTests : PnPTest
    {
        private static string listTitle;

        #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var results = TestScope.ExecuteCommand("Get-PnPList");
            var list = results[0].BaseObject as Microsoft.SharePoint.Client.List;
            listTitle = list.Title;
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void GetPnPListTest()
        {
            var results = TestScope.ExecuteCommand("Get-PnPList");
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetPnPSpecifiedListTest()
        {
            var results = TestScope.ExecuteCommand("Get-PnPList", new CommandParameter("Identity", listTitle));
            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
