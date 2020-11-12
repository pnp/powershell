using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Management.Automation.Runspaces;

namespace PnP.PowerShell.Tests.Lists
{
    [TestClass]
    public class GetViewTests
    {
        private string listTitle;
        private string viewTitle;
        private PSTestScope scope;

        [TestInitialize]
        public void Initialize()
        {
            using (var ctx = TestCommon.CreateClientContext())
            {
                ctx.Load(ctx.Web.Lists);
                ctx.ExecuteQueryRetry();
                listTitle = ctx.Web.Lists[0].Title;
                var views = ctx.Web.Lists[0].EnsureProperty(l => l.Views);
                viewTitle = views[0].Title;
            }

            scope = new PSTestScope(true);
        }

        [TestCleanup]
        public void Cleanup()
        {
            scope?.Dispose();
        }

        #region Scaffolded Cmdlet Tests
        [TestMethod]
        public void GetPnPViewTest()
        {
            var results = scope.ExecuteCommand("Get-PnPView",
                new CommandParameter("List", listTitle));
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetPnPSpecifiedViewTest()
        {
            var results = scope.ExecuteCommand("Get-PnPView",
                new CommandParameter("List", listTitle),
                new CommandParameter("Identity", viewTitle));
            Assert.IsNotNull(results);
        }
        #endregion
    }
}
