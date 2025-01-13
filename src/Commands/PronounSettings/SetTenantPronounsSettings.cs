using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PronounSettings
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantPronounsSettings")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/PeopleSettings.ReadWrite.All")]
    [OutputType(typeof(Model.Graph.PronounsSettings))]
    public class SetTenantPronounsSettings : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public bool IsEnabledInOrganization { get; set; }
        protected override void ExecuteCmdlet()
        {
            var pronouns = RequestHelper.Patch("/v1.0/admin/people/pronouns", new Model.Graph.PronounsSettings { IsPronounsEnabledInOrganization = IsEnabledInOrganization });
            WriteObject(pronouns, false);
        }
    }
}
