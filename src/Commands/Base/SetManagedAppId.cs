using System;
using System.Management.Automation;
using System.Security;
using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Set, "PnPManagedAppId")]
    [OutputType(typeof(void))]
    public class SetManagedAppId : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;

        [Parameter(Mandatory = true)]
        public string AppId;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite;
        protected override void ProcessRecord()
        {
            Uri uri = new Uri(Url);
            Utilities.CredentialManager.AddAppId(uri.ToString(), AppId, Overwrite.ToBool());
        }
    }
}