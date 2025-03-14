using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Remove, "PnPStoredCredential")]
    [OutputType(typeof(void))]
    public class RemoveStoredCredential : BasePSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            var cred = Utilities.CredentialManager.GetCredential(Name);
            if (cred != null)
            {
                if (Force || ShouldContinue($"Remove credential {Name}?", Properties.Resources.Confirm))
                {
                    if (!Utilities.CredentialManager.RemoveCredential(Name))
                    {
                        LogError($"Credential {Name} not removed");
                    }
                }
            }
            else
            {
                LogError($"Credential {Name} not found");
            }
        }
    }
}
