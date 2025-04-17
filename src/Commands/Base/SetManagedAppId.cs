using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Set, "PnPManagedAppId")]
    [OutputType(typeof(void))]
    public class SetManagedAppId : BasePSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Url;

        [Parameter(Mandatory = true)]
        public string AppId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite;

        public SetManagedAppId()
        {
        }

        protected override void ProcessRecord()
        {
            Uri uri = new Uri(Url);
            Utilities.CredentialManager.AddAppId(uri.ToString(), AppId, Overwrite.ToBool());
        }
    }
}