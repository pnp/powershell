using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamsTeamPicture")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]

    public class SetTeamsTeamPicture : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true)]
        public string Path;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(this, Connection, AccessToken);
            if (groupId != null)
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);

                }
                if (File.Exists(Path))
                {
                    var contentType = "";
                    var fileInfo = new FileInfo(Path);
                    switch (fileInfo.Extension.ToLower())
                    {
                        case ".jpg":
                        case ".jpeg":
                            {
                                contentType = "image/jpeg";
                                break;
                            }
                        case ".png":
                            {
                                contentType = "image/png";
                                break;
                            }
                    }
                    if (string.IsNullOrEmpty(contentType))
                    {
                        throw new PSArgumentException("File is not of a supported content type (jpg/png)");
                    }
                    var byteArray = File.ReadAllBytes(Path);
                    TeamsUtility.SetTeamPictureAsync(this, Connection, AccessToken, groupId, byteArray, contentType).GetAwaiter().GetResult();
                }
                else
                {
                    throw new PSArgumentException("File not found");
                }
            }
            else
            {
                throw new PSArgumentException("Team not found");
            }

        }
    }
}
