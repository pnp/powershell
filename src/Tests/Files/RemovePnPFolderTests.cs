using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class RemoveFolderTests : PnPTest
    {
        #region Test Setup/CleanUp

        private static string folderName;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            folderName = $"Folder_{Guid.NewGuid()}";

            TestScope.ExecuteCommand("Add-PnPFolder",
                new CommandParameter("Name", folderName),
                new CommandParameter("Folder", "Shared Documents"));
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
        //TODO: This is a scaffold of the cmdlet - complete the unit test
        [TestMethod]
        public void RemovePnPFolderTest()
        {
            var results = TestScope.ExecuteCommand("Remove-PnPFolder",
                new CommandParameter("Name", folderName),
                new CommandParameter("Folder", "Shared Documents"),
                new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
        #endregion
    }
}
