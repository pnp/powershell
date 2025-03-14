using System.Management.Automation;
using System.Security;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Add, "PnPStoredCredential")]
    [OutputType(typeof(void))]
    public class AddStoredCredential : BasePSCmdlet
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
            if (Password == null || Password.Length == 0)
            {
                Host.UI.Write("Enter password: ");
                Password = Host.UI.ReadLineAsSecureString();
            }
            Utilities.CredentialManager.AddCredential(Name, Username, Password, Overwrite.ToBool());
        }
    }
}