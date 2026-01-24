using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Search
{
    [TestClass]
    public class GetUnsuccesfulCrawledUrlsTests
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
        public void GetPnPUnsuccesfulCrawledUrlsTest()
        {
            using (var scope = new PSTestScope(true))
            {
                // Complete writing cmd parameters

				// From Cmdlet Help: Filter to limit what is being returned. Has to be a URL prefix for SharePoint content, and part of a user principal name for user profiles. Wildcard characters are not supported.
				var filter = "";
				// From Cmdlet Help: Start date to start getting entries from. Defaults to start of time.
				var startDate = "";
				// From Cmdlet Help: End date to stop getting entries from. Default to current time.
				var endDate = "";
				// From Cmdlet Help: Show raw crawl log data
				var rawFormat = "";
                // From Cmdlet Help: Increases the request timeout for this command to accommodate large result sets
                var increaseRequestTimeout = "";

                var results = scope.ExecuteCommand("Get-PnPUnsuccesfulCrawledUrls",
					new CommandParameter("Filter", filter),
					new CommandParameter("StartDate", startDate),
					new CommandParameter("EndDate", endDate),
					new CommandParameter("RawFormat", rawFormat));
					new CommandParameter("IncreaseRequestTimeout", increaseRequestTimeout);
                
                Assert.IsNotNull(results);
            }
        }
        #endregion
    }
}
            