using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class RemoveFileTests : PnPTest
    {
        #region Setup

        private static string fileName;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            fileName = $"{Guid.NewGuid()}.txt";
            var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Resources{System.IO.Path.DirectorySeparatorChar}template.xml");

            TestScope.ExecuteCommand("Add-PnPFile",
                new CommandParameter("Path", filePath),
                new CommandParameter("NewFileName", fileName),
                new CommandParameter("Folder", "Shared Documents"));

        }

        [ClassCleanup]
        public static void Cleanup()
        {
            TestScope.ExecuteCommand("Remove-PnPFile",
                new CommandParameter("SiteRelativeUrl", $"Shared documents/{fileName}"),
                new CommandParameter("Force"));

        }
        #endregion

        #region Scaffolded Cmdlet Tests
        //TODO: This is a scaffold of the cmdlet - complete the unit test
        //[TestMethod]
        public void RemovePnPFileTest()
        {
            var results = TestScope.ExecuteCommand("Remove-PnPFile",
                 new CommandParameter("SiteRelativeUrl", $"Shared Documents/{fileName}"),
                 new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
    }
    #endregion
}
