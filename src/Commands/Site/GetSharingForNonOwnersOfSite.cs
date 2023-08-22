using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPSharingForNonOwnersOfSite")]
    [OutputType(typeof(bool))]
    public class GetSharingForNonOwnersOfSite : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [Alias("Url")]
        public SitePipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var context = ClientContext;
            var siteUrl = ClientContext.Url;

            if (ParameterSpecified(nameof(Identity)))
            {
                context = ClientContext.Clone(Identity.Url);
                siteUrl = context.Url;
            }

            Office365Tenant office365Tenant = new Office365Tenant(context);
            var isSharingDisabledForNonOwnersOfSite = office365Tenant.IsSharingDisabledForNonOwnersOfSite(siteUrl);
            context.ExecuteQueryRetry();

            // Inverting the outcome here on purpose as the wording of the cmdlet indicates that a true means sharing for owners and members is allowed and false would mean only sharing for owners would be allowed
            WriteObject(!isSharingDisabledForNonOwnersOfSite.Value);
        }
    }
}