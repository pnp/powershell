using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADGroupOwner", DefaultParameterSetName = "ByUPN")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Remove-PnPEntraIDGroupOwner")]
    public class RemoveAzureADGroupOwner : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "ByUPN")]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "ByObjectId")]
        public AzureADGroupPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = "ByUPN")]
        public string[] Users;

        [Parameter(Mandatory = true, ParameterSetName = "ByObjectId", ValueFromPipelineByPropertyName = true)]
        [Alias("ObjectId", "Id")]
        public System.Guid[] MemberObjectId;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(GraphRequestHelper);
            }

            if (group != null)
            {
                if (ParameterSetName == "ByUPN" && Users != null && Users.Length > 0)
                {
                    Microsoft365GroupsUtility.RemoveOwners(GraphRequestHelper, new System.Guid(group.Id), Users);
                }
                else if (ParameterSetName == "ByObjectId" && MemberObjectId != null && MemberObjectId.Length > 0)
                {
                    Microsoft365GroupsUtility.RemoveDirectoryOwners(GraphRequestHelper, new System.Guid(group.Id), MemberObjectId);
                }
            }
        }
    }
}