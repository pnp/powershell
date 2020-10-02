using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.Framework.Sites;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Add, "Microsoft365GroupToSite")]
    public class AddMicrosoft365GroupToSite: PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = true)]
        public string Alias;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = true)]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public string Classification;

        [Parameter(Mandatory = false)]
        public SwitchParameter IsPublic;

        [Parameter(Mandatory = false)]
        public SwitchParameter KeepOldHomePage;

        [Parameter(Mandatory = false)]
        public GuidPipeBind HubSiteId;

        [Parameter(Mandatory = false)]
        public string[] Owners;
        protected override void ExecuteCmdlet()
        {            
            var groupifyInformation = new TeamSiteCollectionGroupifyInformation()
            {
                Alias = Alias,
                DisplayName = DisplayName,
                Description = Description,
                Classification = Classification,
                IsPublic = IsPublic,
                KeepOldHomePage = KeepOldHomePage,
                Owners = Owners
            };

            if (ParameterSpecified(nameof(HubSiteId)))
            {
                groupifyInformation.HubSiteId = HubSiteId.Id;
            }

            Tenant.GroupifySite(Url, groupifyInformation);
        }
    }
}