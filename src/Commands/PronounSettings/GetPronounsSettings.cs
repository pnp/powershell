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
            try
            {
                WriteVerbose("Getting access token for Microsoft Graph");
                var requestUrl = $"/v1.0/admin/people/pronouns";

                var pronouns = RequestHelper.Get<Model.Graph.PronounsSettings>(requestUrl);
                WriteObject(pronouns, false);
            }
            catch (Exception e)
            {
                WriteError(new ErrorRecord(new Exception("Make sure you have neccesary access via Application permissions or Delegated permissions, To help understand the required permissions visit https://learn.microsoft.com/en-us/graph/api/peopleadminsettings-list-pronouns?view=graph-rest-1.0&tabs=http#permissions"), e.Message, ErrorCategory.AuthenticationError, null));
            }

        }
    }
}
