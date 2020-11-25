using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Fields
{
    [TestClass]
    public class GetFieldTests : PnPTest
    {

        // #region Setup

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void GetPnPFieldTest()
        {
            var results = TestScope.ExecuteCommand("Get-PnPField");
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetPnPFieldForListTest()
        {
            var listResults = TestScope.ExecuteCommand("Get-PnPList");
            if (listResults.Count > 0)
            {
                var list = listResults[0].BaseObject as Microsoft.SharePoint.Client.List;
                var results = TestScope.ExecuteCommand("Get-PnPField", new CommandParameter("List", list));
                Assert.IsTrue(results.Count > 0);
            }
            else
            {
                Assert.Fail();
            }
        }
        #endregion
    }
}
