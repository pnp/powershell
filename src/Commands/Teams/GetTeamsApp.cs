
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Get, "PnPTeamsApp")]
    [RequiredApiDelegatedPermissions("graph/AppCatalog.Submit")]
    [RequiredApiDelegatedPermissions("graph/AppCatalog.Read.All")]
    [RequiredApiDelegatedPermissions("graph/AppCatalog.ReadWrite.All")]
    [RequiredApiDelegatedPermissions("graph/Directory.Read.All")]
    [RequiredApiDelegatedPermissions("graph/Directory.ReadWrite.All")]
    [RequiredApiApplicationPermissions("graph/AppCatalog.Read.All")]
    [RequiredApiApplicationPermissions("graph/AppCatalog.ReadWrite.All")]
    public class GetTeamsApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public TeamsAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                var app = Identity.GetApp(GraphRequestHelper);
                if (app != null)
                {
                    WriteObject(app);
                }
            }
            else
            {
                WriteObject(TeamsUtility.GetApps(GraphRequestHelper), true);
            }
        }
    }
}