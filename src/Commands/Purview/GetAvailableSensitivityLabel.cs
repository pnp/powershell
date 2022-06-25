using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPAvailableSensitivityLabel")]
    [RequiredMinimalApiPermissions("InformationProtectionPolicy.Read.All")]
    [OutputType(typeof(IEnumerable<Model.Graph.Purview.InformationProtectionLabel>))]
    [OutputType(typeof(Model.Graph.Purview.InformationProtectionLabel))]
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
                var user = User.GetUser(AccessToken);

                if(user == null)
                {
                    WriteWarning("Provided user not found");
                    return;
                }

                url = $"/beta/users/{user.UserPrincipalName}/informationProtection/policy/labels";
            }
            else
            {
                url = "/beta/informationProtection/policy/labels";
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                url += $"/{Identity}";

                var labels = GraphHelper.GetAsync<Model.Graph.Purview.InformationProtectionLabel>(Connection, url, AccessToken).GetAwaiter().GetResult();
                WriteObject(labels, false);
            }
            else
            {
                var labels = GraphHelper.GetResultCollectionAsync<Model.Graph.Purview.InformationProtectionLabel>(Connection, url, AccessToken).GetAwaiter().GetResult();
                WriteObject(labels, true);
            }
        }
    }
}