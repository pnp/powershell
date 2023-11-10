
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Attributes;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsApp")]
    [RequiredMinimalApiPermissions("Directory.Read.All")]

    [TokenType(TokenType = TokenType.Delegate)]
    public class GetTeamsApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public TeamsAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var app = Identity.GetApp(Connection, AccessToken);
                if (app != null)
                {
                    WriteObject(app);
                }
            }
            else
            {
                WriteObject(TeamsUtility.GetAppsAsync(AccessToken, Connection).GetAwaiter().GetResult(), true);
            }
        }
    }
}