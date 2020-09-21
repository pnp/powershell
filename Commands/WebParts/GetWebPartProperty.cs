using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Get, "PnPWebPartProperty")]
    public class GetWebPartProperty : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true)]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Key;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }


            var properties = SelectedWeb.GetWebPartProperties(Identity.Id, ServerRelativePageUrl);
            var values = properties.FieldValues.Select(x => new PropertyBagValue() { Key = x.Key, Value = x.Value });
            if (!string.IsNullOrEmpty(Key))
            {
                var value = values.FirstOrDefault(v => v.Key == Key);
                if (value != null)
                {
                    WriteObject(value.Value);
                }
            }
            else
            {
                WriteObject(values, true);
            }
        }



    }
}
