using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class AddFileTests
    {
        #region Test Setup/CleanUp

        private static string fileName;
        private static PSTestScope scope;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            fileName = $"{Guid.NewGuid()}.txt";
            scope = new PSTestScope();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            scope.ExecuteCommand("Remove-PnPFile",
                new CommandParameter("SiteRelativeUrl", $"shared documents/{fileName}"),
                new CommandParameter("Force"));

            scope?.Dispose();
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        //TODO: This is a scaffold of the cmdlet - complete the unit test
        [TestMethod]
        [DeploymentItem(@"Resources\template.xml", "Resources")]
        public void AddPnPFileTest()
        {
            var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Resources{System.IO.Path.DirectorySeparatorChar}template.xml");
            var results = scope.ExecuteCommand("Add-PnPFile",
                new CommandParameter("Path", filePath),
                new CommandParameter("NewFileName", fileName),
                new CommandParameter("Folder", "documents"));

            Assert.IsTrue(results.Count > 0);
        }
    }
    #endregion
}
