﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsData.Update, "PnPTeamsApp")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
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
                var app = Identity.GetApp(Connection, AccessToken);
                if (app != null)
                {

                    var bytes = System.IO.File.ReadAllBytes(Path);
                    var response = TeamsUtility.UpdateAppAsync(Connection, AccessToken, bytes, app.Id).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        if (GraphHelper.TryGetGraphException(response, out GraphException ex))
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