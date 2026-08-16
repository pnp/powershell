using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPStoredCredential", DefaultParameterSetName = "Name")]
    [OutputType(typeof(PSCredential), ParameterSetName = ["Name"])]
    [OutputType(typeof(string), ParameterSetName = ["List"])]
    public class GetStoredCredential : BasePSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Name")]
        [ValidateNotNullOrEmpty]
        public string Name;

        [Parameter(Mandatory = true, ParameterSetName = "List")]
        public SwitchParameter List;

        protected override void ProcessRecord()
        {
            if (List.IsPresent)
            {
                var credentialNames = Utilities.CredentialManager.ListCredentials();
                if (credentialNames != null && credentialNames.Count > 0)
                {
                    WriteObject(credentialNames, true);
                }
                return;
            }

            var creds = Utilities.CredentialManager.GetCredential(Name);
            if (creds != null)
            {
                WriteObject(creds);
            }
            else
            {
                LogError(new System.Exception("Credentials not found"));
            }
        }
    }
}
