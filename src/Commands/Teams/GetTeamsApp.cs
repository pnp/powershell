
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsApp")]
    [RequiredApiApplicationPermissions("graph/Directory.Read.All")]
    [RequiredApiApplicationPermissions("graph/Directory.ReadWrite.All")]
    [TokenType(TokenType = TokenType.Delegate)]
    public class GetTeamsApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public TeamsAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var app = Identity.GetApp(RequestHelper);
                if (app != null)
                {
                    WriteObject(app);
                }
            }
            else
            {
                WriteObject(TeamsUtility.GetApps(RequestHelper), true);
            }
        }
    }
}