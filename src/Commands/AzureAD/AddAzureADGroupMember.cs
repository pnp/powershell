using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;
using Group = PnP.PowerShell.Commands.Model.Graph.Group;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Add, "PnPAzureADGroupMember")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    [Alias("Add-PnPEntraIDGroupMember")]
    public class AddAzureADGroupMember : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public AzureADGroupPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string[] Users;

        [Parameter(Mandatory = false)]
        public SwitchParameter RemoveExisting;

        protected override void ExecuteCmdlet()
        {
            Group group = null;

            if (Identity != null)
            {
                group = Identity.GetGroup(GraphRequestHelper);
            }
            if (group != null)
            {
                Guid emptyGuid = Guid.Empty;

                var userArray = Users.Where(x => !Guid.TryParse(x, out emptyGuid)).ToArray();

                if (userArray.Length > 0)
                {
                    Microsoft365GroupsUtility.AddMembers(GraphRequestHelper, new Guid(group.Id), userArray, RemoveExisting.ToBool());
                }

                var secGroups = Users.Where(x => Guid.TryParse(x, out emptyGuid)).Select(x => emptyGuid).ToArray();

                if (secGroups.Length > 0)
                {
                    Microsoft365GroupsUtility.AddDirectoryMembers(GraphRequestHelper, new Guid(group.Id), secGroups, RemoveExisting.ToBool());
                }
            }
        }
    }
}