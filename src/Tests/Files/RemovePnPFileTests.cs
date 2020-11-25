using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class RemoveFileTests
    {
        #region Setup

        private static string fileName;
        private static PSTestScope scope;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            fileName = $"{Guid.NewGuid()}.txt";
            scope = new PSTestScope();
            var filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Resources{System.IO.Path.DirectorySeparatorChar}template.xml");

            scope.ExecuteCommand("Add-PnPFile",
                new CommandParameter("Path", filePath),
                new CommandParameter("NewFileName", fileName),
                new CommandParameter("Folder", "documents"));

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
        //[TestMethod]
        public void RemovePnPFileTest()
        {
            var results = scope.ExecuteCommand("Remove-PnPFile",
                 new CommandParameter("SiteRelativeUrl", $"shared documents/{fileName}"),
                 new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
    }
    #endregion
}
