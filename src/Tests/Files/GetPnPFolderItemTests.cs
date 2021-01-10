using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class GetFolderItemTests : PnPTest
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
                new CommandParameter("SiteRelativeUrl", $"Shared Documents/{fileName}"),
                new CommandParameter("Force"));

        }
        #endregion


        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void GetPnPFolderItemTest()
        {
            var results = TestScope.ExecuteCommand("Get-PnPFolderItem",
                new CommandParameter("FolderSiteRelativeUrl", "Shared Documents")
            );
            Assert.IsTrue(results.Count > 0);
            
        }
        #endregion
    }
}
