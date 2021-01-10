using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class GetFolderTests : PnPTest
    {
        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void GetPnPFolderTest()
        {
            var results = TestScope.ExecuteCommand("Get-PnPFolder",
                new CommandParameter("Url", "Shared Documents"));

            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
