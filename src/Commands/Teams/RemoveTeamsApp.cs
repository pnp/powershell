using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Remove, "PnPTeamsApp")]
    [RequiredMinimalApiPermissions("AppCatalog.ReadWrite.All")]
    public class RemoveTeamsApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsAppPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            var app = Identity.GetApp(this, Connection, AccessToken);
            if (app == null)
            {
                throw new PSArgumentException("App not found");
            }
            if (Force || ShouldContinue($"Do you want to remove {app.DisplayName}?", Properties.Resources.Confirm))
            {
                var response = TeamsUtility.DeleteApp(this, Connection, AccessToken, app.Id);
                if (!response.IsSuccessStatusCode)
                {
                    if (GraphHelper.TryGetGraphException(response, out GraphException ex))
                    {
                        if (ex.Error != null)
                        {
                            throw new PSInvalidOperationException(ex.Error.Message);
                        }
                    }
                    else
                    {
                        throw new PSInvalidOperationException("Removing app failed");
                    }
                }
                else
                {
                    WriteObject("App removed");
                }
            }
        }
    }
}