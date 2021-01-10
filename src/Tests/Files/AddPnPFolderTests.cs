using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class AddFolderTests : PnPTest
    {
        #region Test Setup/CleanUp

        private static string folderName;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            folderName = $"Folder_{Guid.NewGuid()}";
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            TestScope.ExecuteCommand("Remove-PnPFolder",
                new CommandParameter("Name", folderName),
                new CommandParameter("Folder", "Shared Documents"),
                new CommandParameter("Force"));
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void AddPnPFolderTest()
        {
            var results = TestScope.ExecuteCommand("Add-PnPFolder",
                new CommandParameter("Name", folderName),
                new CommandParameter("Folder", "Shared Documents"));

            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
