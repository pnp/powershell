using PnP.Framework.Provisioning.Model;
using PnP.PowerShell.Commands.Base;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsCommon.New, "PnPTenantSequenceTeamNoGroupSubSite")]
    public class NewTenantSequenceTeamNoGroupSubSite : BasePSCmdlet
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
        public string Description;

        [Parameter(Mandatory = false)]
        public string[] TemplateIds;

        [Parameter(Mandatory = false)]
        public SwitchParameter QuickLaunchDisabled;

        [Parameter(Mandatory = false)]
        public SwitchParameter UseDifferentPermissionsFromParentSite;

        protected override void ProcessRecord()
        {
            var site = new TeamNoGroupSubSite()
            {
                Url = Url,
                Language = (int)Language,
                QuickLaunchEnabled = !QuickLaunchDisabled.IsPresent,
                UseSamePermissionsAsParentSite = !UseDifferentPermissionsFromParentSite.IsPresent,
                TimeZoneId = (int)TimeZoneId,
                Description = Description,
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