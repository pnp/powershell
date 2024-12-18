using System.Management.Automation;
using System.Collections.Generic;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.PeopleSettings
{
    [Cmdlet(VerbsCommon.Get, "PnPProfileCardProperty")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/PeopleSettings.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/PeopleSettings.ReadWrite.All")]
    [OutputType(typeof(IEnumerable<Model.Graph.ProfileCard.ProfileCardProperty>))]
    [OutputType(typeof(Model.Graph.ProfileCard.ProfileCardProperty))]
    public class GetProfileCardProperty : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public ProfileCardPropertyName PropertyName;

        protected override void ExecuteCmdlet()
        {
            WriteVerbose("Getting access token for Microsoft Graph");
            var requestUrl = $"/v1.0/admin/people/profileCardProperties";

            if (ParameterSpecified(nameof(PropertyName)))
            {
                if (ParameterSpecified(nameof(PropertyName)))
                {
                    requestUrl += $"/{PropertyName.ToString()}";
                }
                WriteVerbose($"Retrieving profile card property '{PropertyName}'");

                var propertyResult = RequestHelper.Get<Model.Graph.ProfileCard.ProfileCardProperty>(requestUrl);
                WriteObject(propertyResult, false);
            }
            else
            {
                WriteVerbose("Retrieving all profile card properties");

                var propertyResults = RequestHelper.GetResultCollection<Model.Graph.ProfileCard.ProfileCardProperty>(requestUrl);
                WriteObject(propertyResults, true);
            }
        }
    }
}
