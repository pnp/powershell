using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Purview
{
    [Cmdlet(VerbsCommon.Get, "PnPAvailableSensitivityLabel")]
    [OutputType(typeof(IEnumerable<Model.Graph.Purview.InformationProtectionLabel>))]
    [OutputType(typeof(Model.Graph.Purview.InformationProtectionLabel))]
    [RequiredApiApplicationPermissions("graph/InformationProtectionPolicy.ReadAll")]
    [RequiredApiDelegatedPermissions("graph/InformationProtectionPolicy.Read")]
    public class GetAvailableSensitivityLabel : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public AzureADUserPipeBind User;

        [Parameter(Mandatory = false)]
        public Guid Identity;

        protected override void ExecuteCmdlet()
        {
            string url;
            if (ParameterSpecified(nameof(User)))
            {
                var user = User.GetUser(AccessToken, Connection.AzureEnvironment);

                if (user == null)
                {
                    WriteWarning("Provided user not found");
                    return;
                }

                url = $"/beta/users/{user.UserPrincipalName}/security/informationProtection/sensitivityLabels";
            }
            else
            {
                if (Connection.ConnectionMethod == Model.ConnectionMethod.AzureADAppOnly)
                {
                    url = "/beta/security/informationProtection/sensitivityLabels";
                }
                else
                {
                    url = "/beta/me/security/informationProtection/sensitivityLabels";
                }
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                url += $"/{Identity}";

                var labels = GraphHelper.Get<Model.Graph.Purview.InformationProtectionLabel>(this, Connection, url, AccessToken);
                WriteObject(labels, false);
            }
            else
            {
                var labels = GraphHelper.GetResultCollection<Model.Graph.Purview.InformationProtectionLabel>(this, Connection, url, AccessToken);
                WriteObject(labels, true);
            }
        }
    }
}