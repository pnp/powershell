using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    /// <summary>
    /// Removes a font file from the tenant's Brand Center fonts. Accepts the output of
    /// Get-PnPBrandCenterFont through the pipeline (by its FileName property).
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "PnPBrandCenterFont", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveBrandCenterFont : PnPSharePointOnlineAdminCmdlet
    {
        /// <summary>File name of the font, for example "Contoso-Regular.ttf".</summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string FileName;

        protected override void ExecuteCmdlet()
        {
            if (!ShouldProcess(FileName, "Remove the font file from the Brand Center fonts"))
            {
                return;
            }

            LogDebug($"Removing font file '{FileName}' from the tenant's brand fonts library");
            var result = Tenant.RemoveBrandFontFile(FileName);
            AdminContext.ExecuteQueryRetry();

            WriteObject(result, true);
        }
    }
}
