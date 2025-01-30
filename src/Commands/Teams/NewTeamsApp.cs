using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.New, "PnPTeamsApp")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppCatalog.ReadWrite.All")]
    public class NewTeamsApp : PnPGraphCmdlet
    {
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
                try
                {
                    var bytes = System.IO.File.ReadAllBytes(Path);
                    TeamsUtility.AddApp(GraphRequestHelper, bytes);
                }
                catch (GraphException ex)
                {
                    if (ex.Error != null)
                    {
                        throw new PSInvalidOperationException(ex.Error.Message);
                    }
                    else
                    {
                        throw new PSInvalidOperationException(ex.Message);
                    }
                }
            }
            else
            {
                new PSArgumentException("File not found");
            }
        }
    }
}