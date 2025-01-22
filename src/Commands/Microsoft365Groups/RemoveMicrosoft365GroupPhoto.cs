using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Remove, "PnPMicrosoft365GroupPhoto")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class RemoveMicrosoft365GroupPicture : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var group = Identity.GetGroup(RequestHelper, false, false, false, false);
            if (group != null)
            {
                var response = Microsoft365GroupsUtility.DeletePhoto(RequestHelper, group.Id.Value);
                if (!response.IsSuccessStatusCode)
                {
                    if (RequestHelper.TryGetGraphException(response, out GraphException ex))
                    {
                        if (ex.Error != null)
                        {
                            throw new PSInvalidOperationException(ex.Error.Message);
                        }
                    }
                    else
                    {
                        throw new PSInvalidOperationException("Photo remove failed");
                    }
                }
            }
            else
            {
                throw new PSArgumentException("Group not found");
            }

        }
    }
}