using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class GetFileTests : PnPTest
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
        public void GetPnPFileAsStringTest()
        {
            var results = TestScope.ExecuteCommand("Get-PnPFile",
                new CommandParameter("Url", $"Shared Documents/{fileName}"),
                new CommandParameter("AsString"));
            Assert.AreEqual(results.Count, 1);
            Assert.IsInstanceOfType(results[0].BaseObject, typeof(string));
        }

        [TestMethod]
        public void GetPnPFileAsListItemTest()
        {
            var results = TestScope.ExecuteCommand("Get-PnPFile",
                new CommandParameter("Url", $"Shared Documents/{fileName}"),
                new CommandParameter("AsListItem"));
            Assert.AreEqual(results.Count, 1);
            Assert.IsInstanceOfType(results[0].BaseObject, typeof(Microsoft.SharePoint.Client.ListItem));
        }
    }
    #endregion
}
