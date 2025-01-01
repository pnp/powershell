using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.PronounSettings
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantPronounsSettings")]
    [RequiredApiApplicationPermissions("graph/PeopleSettings.Read.All")]
    [RequiredApiDelegatedPermissions("graph/PeopleSettings.Read.All")]
    public class GetPronounsSettings : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteVerbose("Getting access token for Microsoft Graph");
            var requestUrl = $"/v1.0/admin/people/pronouns";

            var pronouns = RequestHelper.Get<Model.Graph.PronounsSettings>(requestUrl);
            WriteObject(pronouns, false);
        }
    }
}
