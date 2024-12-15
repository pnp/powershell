using PnP.Framework.Provisioning.Model;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.New, "PnPTenantSequenceTeamNoGroupSite")]
    public class NewTenantSequenceTeamNoGroupSite : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;
        
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public uint TimeZoneId;

        [Parameter(Mandatory = false)]
        public uint Language;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public SwitchParameter HubSite;

        [Parameter(Mandatory = false)]
        public string[] TemplateIds;

        protected override void ProcessRecord()
        {
            var site = new TeamNoGroupSiteCollection
            {
                Url = Url,
                Language = (int)Language,
                Owner = Owner,
                TimeZoneId = (int)TimeZoneId,
                Description = Description,
                IsHubSite = HubSite.IsPresent,
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