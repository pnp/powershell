using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.New, "PnPTenantSequenceTeamSite")]
    public class NewTenantSequenceTeamSite : BasePSCmdlet
    {

        [Parameter(Mandatory = true)]
        public string Alias;

        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = false)]
        public string Description = "";

        [Parameter(Mandatory = false)]
        public string DisplayName = "";

        [Parameter(Mandatory = false)]
        public string Classification;

        [Parameter(Mandatory = false)]
        public SwitchParameter Public;

        [Parameter(Mandatory = false)]
        public SwitchParameter HubSite;

        [Parameter(Mandatory = false)]
        public string[] TemplateIds;

        protected override void ProcessRecord()
        {
            var site = new TeamSiteCollection
            {
                Alias = Alias,
                Classification = Classification,
                Description = Description,
                DisplayName = DisplayName,
                IsHubSite = HubSite.IsPresent,
                IsPublic = Public.IsPresent,
                Title = Title
            };
            if (TemplateIds != null)
            {
                site.Templates.AddRange(TemplateIds.ToList());
            } 
            WriteObject(site);
        }
    }
}