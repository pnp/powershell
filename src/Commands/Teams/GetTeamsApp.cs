
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Attributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsApp")]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Directory_ReadWrite_All)]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.AppCatalog_Read_All)]
    [PnPManagementShellScopes("Directory.ReadWrite.All")]

    [TokenType(TokenType = TokenType.Delegate)]
    public class GetTeamsApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public TeamsAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var app = Identity.GetApp(HttpClient, AccessToken);
                if (app != null)
                {
                    WriteObject(app);
                }
            }
            else
            {
                WriteObject(TeamsUtility.GetAppsAsync(AccessToken, HttpClient).GetAwaiter().GetResult(), true);
            }
        }
    }
}