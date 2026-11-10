using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteDesign", DefaultParameterSetName = "Default")]
    [OutputType(typeof(TenantSiteDesign), ParameterSetName = new[] { "Default" })]
    [OutputType(typeof(IEnumerable<BuiltInSiteDesign>), ParameterSetName = new[] { "BuiltIn" })]
    public class GetSiteDesign : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "Default")]
        public TenantSiteDesignPipeBind Identity;

        /// <summary>
        /// When specified, returns built-in Microsoft site designs (store 1) instead of tenant-custom ones
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "BuiltIn")]
        public SwitchParameter BuiltIn;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "BuiltIn")
            {
                LogDebug("Retrieving built-in Microsoft site designs from store 1");

                AdminContext.Load(AdminContext.Web, w => w.Url);
                AdminContext.ExecuteQueryRetry();

                var results = SiteTemplates.GetBuiltInSiteDesigns(SharePointRequestHelper, AdminContext.Web.Url);

                if (results?.Items != null)
                {
                    WriteObject(results.Items.ToList(), true);
                }
                return;
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var siteDesigns = Identity.GetTenantSiteDesign(Tenant);

                if (siteDesigns == null || siteDesigns.Length == 0)
                {
                    LogDebug("No site designs with the identity provided through Identity have been found");
                    return;
                }

                WriteObject(siteDesigns, true);
            }
            else
            {
                var designs = Tenant.GetSiteDesigns();
                AdminContext.Load(designs);
                AdminContext.ExecuteQueryRetry();

                WriteObject(designs.ToList(), true);
            }
        }
    }
}
