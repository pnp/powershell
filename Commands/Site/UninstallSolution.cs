using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsLifecycle.Uninstall, "Solution")]
    public class UninstallSolution : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public GuidPipeBind PackageId;

        [Parameter(Mandatory = true)]
        public string PackageName;

        [Parameter(Mandatory = false)]
        public int MajorVersion = 1;

        [Parameter(Mandatory = false)]
        public int MinorVersion = 0;

        protected override void ExecuteCmdlet()
        {
            ClientContext.Site.UninstallSolution(PackageId.Id, PackageName, MajorVersion, MinorVersion);
        }
    }
}
