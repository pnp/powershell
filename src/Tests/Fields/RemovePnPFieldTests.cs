using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Fields
{
    [TestClass]
    public class RemoveFieldTests
    {
        private static string fieldName;
        private static Microsoft.SharePoint.Client.List list;
        private static PSTestScope scope;
        #region Test Setup/CleanUp
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            fieldName = $"Field{Guid.NewGuid()}";
            scope = new PSTestScope();

            var listResults = scope.ExecuteCommand("Get-PnPList");
            if (listResults.Count > 0)
            {
                list = listResults[0].BaseObject as Microsoft.SharePoint.Client.List;
            }

            scope.ExecuteCommand("Add-PnPField",
               new CommandParameter("DisplayName", fieldName),
               new CommandParameter("InternalName", fieldName),
               new CommandParameter("Type", Microsoft.SharePoint.Client.FieldType.Text));

            scope.ExecuteCommand("Add-PnPField",
                    new CommandParameter("List", list.Title),
                    new CommandParameter("DisplayName", $"{fieldName}2"),
                    new CommandParameter("InternalName", $"{fieldName}2"),
                    new CommandParameter("Type", Microsoft.SharePoint.Client.FieldType.Text));
        }

        [ClassCleanup]
        public static void Cleanup(TestContext testContext)
        {
            scope?.Dispose();
        }


        #endregion

        #region Scaffolded Cmdlet Tests
        //TODO: This is a scaffold of the cmdlet - complete the unit test
        //[TestMethod]
        public void RemovePnPFieldTest()
        {
            var results = scope.ExecuteCommand("Remove-PnPField",
                new CommandParameter("Identity", fieldName),
                new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }

        public void RemovePnPFieldFromListTest()
        {
            var results = scope.ExecuteCommand("Remove-PnPField",
                           new CommandParameter("Identity", fieldName),
                           new CommandParameter("List", list.Title),
                           new CommandParameter("Force"));

            Assert.AreEqual(results.Count, 0);
        }
        #endregion
    }
}
