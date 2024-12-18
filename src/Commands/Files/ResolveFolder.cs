using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsDiagnostic.Resolve, "PnPFolder")]
    public class ResolveFolder : PnPWebRetrievalsCmdlet<Folder>
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string SiteRelativePath = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (MyInvocation.InvocationName.ToLower() == "ensure-pnpfolder")
            {
                WriteWarning("Ensure-PnPFolder has been deprecated. Use Resolve-PnPFolder with the same parameters instead.");
            }
            WriteObject(CurrentWeb.EnsureFolderPath(SiteRelativePath, RetrievalExpressions));
        }
    }
}
