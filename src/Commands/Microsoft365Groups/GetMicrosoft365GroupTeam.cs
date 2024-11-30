using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupTeam")]
    [RequiredApiApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class GetMicrosoft365GroupTeam : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Guid groupId;
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteVerbose($"Defining Microsoft 365 Group based on {nameof(Identity)} parameter");
                groupId = Identity.GetGroupId(RequestHelper);
            }
            else
            {
                WriteVerbose($"Validating if the current site at {Connection.Url} has a Microsoft 365 Group behind it");
                ClientContext.Load(ClientContext.Site, s => s.GroupId);
                ClientContext.ExecuteQueryRetry();

                groupId = ClientContext.Site.GroupId;

                if(groupId == Guid.Empty)
                {
                    throw new PSArgumentException("Current site is not backed by a Microsoft 365 Group", nameof(Identity));
                }
                else
                {
                    WriteVerbose($"Current site at {Connection.Url} is backed by the Microsoft 365 Group with Id {groupId}");
                }
            }

            WriteVerbose($"Requesting endpoints of Microsoft 365 Group with Id {groupId}");
            var endpoints = RequestHelper.GetResultCollection<Model.AzureAD.AzureADGroupEndPoint>($"/beta/groups/{groupId}/endpoints");
            WriteVerbose($"{endpoints.Count()} endpoint(s) found in total");

            var yammerEndpoint = endpoints.Where(e => e.ProviderName.Equals("Microsoft Teams", StringComparison.InvariantCultureIgnoreCase));
            WriteVerbose($"{yammerEndpoint.Count()} Teams endpoint(s) found");

            WriteObject(yammerEndpoint, true);
        }
    }
}