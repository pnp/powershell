using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsData.Update, "PnPTeamsApp")]
    [RequiredApiApplicationPermissions("graph/Group.ReadWrite.All")]
    public class UpdateTeamsApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsAppPipeBind Identity;

        [Parameter(Mandatory = true)]
        public string Path;

        protected override void ExecuteCmdlet()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            if (System.IO.File.Exists(Path))
            {
                var app = Identity.GetApp(RequestHelper);
                if (app != null)
                {

                    var bytes = System.IO.File.ReadAllBytes(Path);
                    var response = TeamsUtility.UpdateApp(RequestHelper, bytes, app.Id);
                    if (!response.IsSuccessStatusCode)
                    {
                        if (RequestHelper.TryGetGraphException(response, out GraphException ex))
                        {
                            throw new PSInvalidOperationException(ex.Error.Message);
                        }
                        else
                        {
                            throw new PSInvalidOperationException("Update app failed");
                        }
                    }
                    else
                    {
                        WriteObject("App updated");
                    }
                }
                else
                {
                    throw new PSArgumentException("App not found");
                }
            }
            else
            {
                throw new PSArgumentException("File not found");
            }
        }
    }
}