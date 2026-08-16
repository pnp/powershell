using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPStoredCredential", DefaultParameterSetName = ParameterSet_NAME)]
    [OutputType(typeof(PSCredential), ParameterSetName = [ParameterSet_NAME])]
    [OutputType(typeof(string), ParameterSetName = [ParameterSet_LIST])]
    public class GetStoredCredential : BasePSCmdlet
    {
        private const string ParameterSet_NAME = "Name";
        private const string ParameterSet_LIST = "List";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_NAME)]
        [ValidateNotNullOrEmpty]
        public string Name;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_LIST)]
        public SwitchParameter List;

        protected override void ProcessRecord()
        {
            if (ParameterSetName == ParameterSet_LIST)
            {
                var storedCredentials = Utilities.CredentialManager.ListCredentials();

                if (!string.IsNullOrEmpty(storedCredentials.Source))
                {
                    WriteVerbose($"Listing the credentials stored in {storedCredentials.Source}.");
                }

                // An empty result must never be reported as "nothing is stored" when the credential store could not be read
                if (!string.IsNullOrEmpty(storedCredentials.Warning))
                {
                    WriteWarning(storedCredentials.Warning);
                }
                else if (storedCredentials.Names.Count == 0)
                {
                    WriteVerbose($"No credentials stored by PnP PowerShell were found in {storedCredentials.Source}.");
                }

                WriteObject(storedCredentials.Names, true);
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
