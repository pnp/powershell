using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Remove, "PnPManagedAppId")]
    [OutputType(typeof(void))]
    public class RemoveManagedAppId : BasePSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Url;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ProcessRecord()
        {
            Uri uri = new Uri(Url);
            var appId = Utilities.CredentialManager.GetAppId(uri.ToString());
            if (appId != null)
            {
                if (Force || ShouldContinue($"Remove App Id: {Url}?", Properties.Resources.Confirm))
                {
                    if (!Utilities.CredentialManager.RemoveAppid(uri.ToString()))
                    {
                        LogError($"AppId for {Url} not removed");
                    }
                }
            }
            else
            {
                LogError($"AppId not found for {Url}");
            }
        }
    }
}
