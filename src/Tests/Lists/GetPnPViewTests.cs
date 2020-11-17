using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class GetViewTests
    {
        private static string listTitle;
        private static string viewTitle;
        private static PSTestScope scope;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            using (var ctx = TestCommon.CreateClientContext())
            {
                ctx.Load(ctx.Web.Lists);
                ctx.ExecuteQueryRetry();
                listTitle = ctx.Web.Lists[0].Title;
                var views = ctx.Web.Lists[0].EnsureProperty(l => l.Views);
                viewTitle = views[0].Title;
            }
            // scope = new PSTestScope(true);
            scope = new PSTestScope(true);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            scope?.Dispose();
        }

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void GetPnPViewTest()
        {
            var results = scope.ExecuteCommand("Get-PnPView",
                new CommandParameter("List", listTitle));
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void GetPnPSpecifiedViewTest()
        {
            var results = scope.ExecuteCommand("Get-PnPView",
                new CommandParameter("List", listTitle),
                new CommandParameter("Identity", viewTitle));
            Assert.AreEqual(results.Count, 1);
        }
        #endregion
    }
}
