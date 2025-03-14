using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPManagedAppId")]
    [OutputType(typeof(PSCredential))]
    public class GetManagedAppId : BasePSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Url;

        protected override void ProcessRecord()
        {
            Uri uri = new Uri(Url);
            var appId = Utilities.CredentialManager.GetAppId(uri.ToString());
            if (appId != null)
            {
                WriteObject(appId);
            }
            else
            {
                WriteError(new ErrorRecord(new Exception("AppId not found"), "APPIDNOTFOUND", ErrorCategory.AuthenticationError, this));
            }
        }
    }
}
