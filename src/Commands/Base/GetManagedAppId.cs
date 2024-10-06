using System;
using System.Management.Automation;
using System.Net;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPManagedAppId")]
    [OutputType(typeof(PSCredential))]
    public class GetManagedAppId : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;

        protected override void ProcessRecord()
        {
            Uri uri = new Uri(Url);
            var appId = Utilities.CredentialManager.GetAppId(uri.ToString());
            if (appId != null)
            {
                WriteObject(appId);
            }
            else
            {
                WriteError(new ErrorRecord(new System.Exception("AppId not found"), "APPIDNOTFOUND", ErrorCategory.AuthenticationError, this));
            }
        }
    }
}
