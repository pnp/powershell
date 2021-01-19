using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class AddFileTests : PnPTest
    {
        #region Test Setup/CleanUp

        private static string fileName;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            fileName = $"{Guid.NewGuid()}.txt";
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            TestScope.ExecuteCommand("Remove-PnPFile",
                new CommandParameter("SiteRelativeUrl", $"shared documents/{fileName}"),
                new CommandParameter("Force"));
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        [DeploymentItem(@"Resources\template.xml", "Resources")]
        public void AddPnPFileTest()
        {
            var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Resources{System.IO.Path.DirectorySeparatorChar}template.xml");
            var results = TestScope.ExecuteCommand("Add-PnPFile",
                new CommandParameter("Path", filePath),
                new CommandParameter("NewFileName", fileName),
                new CommandParameter("Folder", "Shared Documents"));

            Assert.IsTrue(results.Count > 0);
        }
    }
    #endregion
}
