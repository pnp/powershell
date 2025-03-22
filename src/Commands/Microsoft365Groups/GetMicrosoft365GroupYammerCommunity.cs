using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupYammerCommunity")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class GetMicrosoft365GroupYammerCommunity : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Guid groupId;
            if (ParameterSpecified(nameof(Identity)))
            {
                LogDebug($"Defining Microsoft 365 Group based on {nameof(Identity)} parameter");
                groupId = Identity.GetGroupId(GraphRequestHelper);
            }
            else
            {
                LogDebug($"Validating if the current site at {Connection.Url} has a Microsoft 365 Group behind it");
                ClientContext.Load(ClientContext.Site, s => s.GroupId);
                ClientContext.ExecuteQueryRetry();

                groupId = ClientContext.Site.GroupId;

                if (groupId == Guid.Empty)
                {
                    throw new PSArgumentException("Current site is not backed by a Microsoft 365 Group", nameof(Identity));
                }
                else
                {
                    LogDebug($"Current site at {Connection.Url} is backed by the Microsoft 365 Group with Id {groupId}");
                }
            }

            LogDebug($"Requesting endpoints of Microsoft 365 Group with Id {groupId}");
            var endpoints = GraphRequestHelper.GetResultCollection<Model.AzureAD.AzureADGroupEndPoint>($"/beta/groups/{groupId}/endpoints");
            LogDebug($"{endpoints.Count()} endpoint(s) found in total");

            var yammerEndpoint = endpoints.Where(e => e.ProviderName.Equals("Yammer", StringComparison.InvariantCultureIgnoreCase));
            LogDebug($"{yammerEndpoint.Count()} Yammer endpoint(s) found");

            WriteObject(yammerEndpoint, true);
        }
    }
}