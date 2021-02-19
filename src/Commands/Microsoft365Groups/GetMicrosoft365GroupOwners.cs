using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Get, "PnPMicrosoft365GroupOwners")]
    [RequiredMinimalApiPermissions("Group.Read.All")]
    public class GetMicrosoft365GroupOwners : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            UnifiedGroupEntity group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(AccessToken, false);
            }

            if (group != null)
            {
                // Get Owners of the group
                List<UnifiedGroupUser> owners = UnifiedGroupsUtility.GetUnifiedGroupOwners(group, AccessToken, azureEnvironment: PnPConnection.Current.AzureEnvironment);
                WriteObject(owners);
            }
        }
    }
}