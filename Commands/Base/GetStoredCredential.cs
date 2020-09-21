using System.Management.Automation;
using System.Net;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPStoredCredential")]
    public class GetStoredCredential : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

#if !PNPPSCORE
        [Parameter(Mandatory = false)]
        public CredentialType Type = CredentialType.O365;
#endif

        protected override void ProcessRecord()
        {
#if !PNPPSCORE
            var cred = Utilities.CredentialManager.GetCredential(Name);
            if (cred != null)
            {
                switch (Type)
                {
                    case CredentialType.O365:
                        {
                            if (cred != null)
                            {
                                WriteObject(new SharePointOnlineCredentials(cred.UserName, cred.Password));
                            }
                            break;
                        }
                    case CredentialType.OnPrem:
                        {
                            WriteObject(new NetworkCredential(cred.UserName, cred.Password));
                            break;
                        }
                    case CredentialType.PSCredential:
                        {
                            WriteObject(cred);
                            break;
                        }
                }
            }
#else
            var creds = Utilities.CredentialManager.GetCredential(Name);
            if(creds != null)
            {
                var spoCreds = new System.Net.NetworkCredential(creds.UserName, creds.Password);
                WriteObject(spoCreds);
            } else
            {
                WriteError(new ErrorRecord(new System.Exception("Credentials not found"), "CREDSNOTFOUND", ErrorCategory.AuthenticationError, this));
            }
#endif
        }
    }
}
