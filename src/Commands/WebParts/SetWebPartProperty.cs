using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Set, "PnPWebPartProperty")]
    [OutputType(typeof(void))]
    public class SetWebPartProperty : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true)]
        public Guid Identity;

        [Parameter(Mandatory = true)]
        public string Key = string.Empty;

        [Parameter(Mandatory = true)]
        public PSObject Value = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }

            if (Value.BaseObject is string)
            {
                CurrentWeb.SetWebPartProperty(Key, Value.ToString(), Identity, ServerRelativePageUrl);
            }
            else if (Value.BaseObject is int)
            {
                CurrentWeb.SetWebPartProperty(Key, (int)Value.BaseObject, Identity, ServerRelativePageUrl);
            } else if (Value.BaseObject is bool)
            {
                CurrentWeb.SetWebPartProperty(Key, (bool)Value.BaseObject, Identity, ServerRelativePageUrl);
            }
            else
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("Type of value is not supported. Has to be of type string, int or bool"), "UNSUPPORTEDTYPE",ErrorCategory.InvalidType, this));
            }
        }
    }
}
