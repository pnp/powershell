using System.Management.Automation;
using System.Security;
using PnP.Framework.Utilities;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Add, "PnPStoredCredential")]
    public class AddStoredCredential : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = true)]
        public string Username;

        [Parameter(Mandatory = false)]
        public SecureString Password;
        
        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite;
        protected override void ProcessRecord()
        {
            if(Password == null || Password.Length == 0)
            {
                Host.UI.Write("Enter password: ");
                Password = Host.UI.ReadLineAsSecureString();
            } 
            Utilities.CredentialManager.AddCredential(Name, Username, Password, Overwrite.ToBool());
        }
    }
}