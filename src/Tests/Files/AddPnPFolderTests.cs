using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Files
{
    [TestClass]
    public class AddFolderTests
    {
        #region Test Setup/CleanUp

        private static string folderName;
        private static PSTestScope scope;

        // #region Setup
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            folderName = $"Folder_{Guid.NewGuid()}";
            scope = new PSTestScope();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            scope.ExecuteCommand("Remove-PnPFolder",
                new CommandParameter("Name", folderName),
                new CommandParameter("Folder", "documents"),
                new CommandParameter("Force"));

            scope?.Dispose();
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        //TODO: This is a scaffold of the cmdlet - complete the unit test
        [TestMethod]
        public void AddPnPFolderTest()
        {
            using (var scope = new PSTestScope(true))
            {
                var results = scope.ExecuteCommand("Add-PnPFolder",
                    new CommandParameter("Name", folderName),
                    new CommandParameter("Folder", "documents"));

                Assert.AreEqual(results.Count, 1);
            }
        }
        #endregion
    }
}
