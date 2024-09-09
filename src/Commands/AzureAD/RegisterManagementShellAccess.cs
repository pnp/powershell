using PnP.Framework;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsLifecycle.Register, "PnPManagementShellAccess")]
    public class RegisterManagementShellAccess : PSCmdlet
    {
        private const string ParameterSet_REGISTER = "Register access";
        private const string ParameterSet_SHOWURL = "Show Consent Url";        

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_REGISTER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SHOWURL)]
        public AzureEnvironment AzureEnvironment = AzureEnvironment.Production;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_REGISTER)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SHOWURL)]
        public SwitchParameter LaunchBrowser;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SHOWURL)]
        public SwitchParameter ShowConsentUrl;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SHOWURL)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_REGISTER)]
        public string TenantName;

        protected override void ProcessRecord()
        {
            CmdletMessageWriter.WriteFormattedMessage(this, new CmdletMessageWriter.Message { Text = "Creating PnP Management Shell multi-tenant App for authentication is not supported as of September 9th, 2024. Please use Register-PnPEntraIDApp or Register-PnPEntraIDAppForInteractiveLogin. Refer to https://pnp.github.io/powershell/articles/registerapplication.html on how to register your own application.", Formatted = true, Type = CmdletMessageWriter.MessageType.Warning });
            ThrowTerminatingError(new ErrorRecord(new NotSupportedException(), "PNPMGTSHELLNOTSUPPORTED", ErrorCategory.AuthenticationError, this));
        }
    }
}
