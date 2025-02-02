using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PronounSettings
{
    [Cmdlet(VerbsCommon.Set, "PnPTenantPronounsSetting")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/PeopleSettings.ReadWrite.All")]
    [OutputType(typeof(Model.Graph.PronounsSettings))]
    public class SetTenantPronounsSetting : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public bool IsEnabledInOrganization { get; set; }
        protected override void ExecuteCmdlet()
        {
            var pronouns = GraphRequestHelper.Patch("/v1.0/admin/people/pronouns", new Model.Graph.PronounsSettings { IsPronounsEnabledInOrganization = IsEnabledInOrganization });
            WriteObject(pronouns, false);
        }
    }
}
