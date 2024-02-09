using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;

namespace PnP.PowerShell.Tests.InformationManagement
{
    [TestClass]
    public class ResetRetentionLabelTest
    {
        #region Test Setup/CleanUp
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            // This runs on class level once before all tests run
            //using (var ctx = TestCommon.CreateClientContext())
            //{
            //}
        }

        [ClassCleanup]
        public static void Cleanup(TestContext testContext)
        {
            // This runs on class level once
            //using (var ctx = TestCommon.CreateClientContext())
            //{
            //}
        }

        [TestInitialize]
        public void Initialize()
        {
            using (var scope = new PSTestScope())
            {
                // Example
                // scope.ExecuteCommand("cmdlet", new CommandParameter("param1", prop));
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var scope = new PSTestScope())
            {
                try
                {
                    // Do Test Setup - Note, this runs PER test
                }
                catch (Exception)
                {
                    // Describe Exception
                }
            }
        }
        #endregion

        #region Scaffolded Cmdlet Tests
        //TODO: This is a scaffold of the cmdlet - complete the unit test
        //[TestMethod]
        public void ResetPnPRetentionLabel_ListTest()
        {
            using (var scope = new PSTestScope(true))
            {
                // Complete writing cmd parameters

				// This is a mandatory parameter
				// From Cmdlet Help: The ID or Url of the list
				var list = "";
				// From Cmdlet Help: Reset label on existing items in the library
				var syncToItems = "";

                var results = scope.ExecuteCommand("Reset-PnPRetentionLabel",
					new CommandParameter("List", list),
					new CommandParameter("SyncToItems", syncToItems));
                
                Assert.IsNotNull(results);
            }
        }

        //TODO: This is a scaffold of the cmdlet - complete the unit test
        //[TestMethod]
        public void ResetPnPRetentionLabel_BulkItemsTest()
        {
            using (var scope = new PSTestScope(true))
            {
                // Complete writing cmd parameters

				// This is a mandatory parameter
				// From Cmdlet Help: The ID, Title or Url of the list.
				var list = "";
				// This is a mandatory parameter
				// From Cmdlet Help: List of iist item IDs.
				var itemIds = new List<int>();
				
                var results = scope.ExecuteCommand("Reset-PnPRetentionLabel",
					new CommandParameter("List", list),
					new CommandParameter("ItemIds", itemIds));
                
                Assert.IsNotNull(results);
            }
        }
        #endregion
    }
}
            