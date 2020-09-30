using System.Management.Automation;
using System.Net;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPStoredCredential")]
    public class GetStoredCredential : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

        protected override void ProcessRecord()
        {
            var creds = Utilities.CredentialManager.GetCredential(Name);
            if (creds != null)
            {
                var spoCreds = new System.Net.NetworkCredential(creds.UserName, creds.Password);
                WriteObject(spoCreds);
            }
            else
            {
                WriteError(new ErrorRecord(new System.Exception("Credentials not found"), "CREDSNOTFOUND", ErrorCategory.AuthenticationError, this));
            }
        }
    }
}
