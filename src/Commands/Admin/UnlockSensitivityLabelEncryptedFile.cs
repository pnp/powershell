using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Files
{
    [Cmdlet(VerbsCommon.Unlock, "PnPSensitivityLabelEncryptedFile")]
    public class UnlockSensitivityLabelEncryptedFile : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Url = string.Empty;

        [Parameter(Mandatory = true)]
        public string JustificationText = string.Empty;
        protected override void ExecuteCmdlet()
        {
            // Remove URL decoding from the Url as that will not work. We will encode the + character specifically, because if that is part of the filename, it needs to stay and not be decoded.
            Url = Utilities.UrlUtilities.UrlDecode(Url.Replace("+", "%2B"));

            Tenant.UnlockSensitivityLabelEncryptedFile(Url, JustificationText);
            AdminContext.ExecuteQueryRetry();
        }
    }
}
