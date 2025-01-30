using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
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

                url = $"/beta/users/{user.Id.Value}/security/informationProtection/sensitivityLabels";
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

                var labels = GraphRequestHelper.Get<Model.Graph.Purview.InformationProtectionLabel>(url);
                WriteObject(labels, false);
            }
            else
            {
                var labels = GraphRequestHelper.GetResultCollection<Model.Graph.Purview.InformationProtectionLabel>(url);
                WriteObject(labels, true);
            }
        }
    }
}