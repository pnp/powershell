using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Get, "PnPWebPartProperty")]
    [OutputType(typeof(PropertyBagValue), ParameterSetName = new[] { ParameterSet_All })]
    [OutputType(typeof(object), ParameterSetName = new[] { ParameterSet_Key })]
    public class GetWebPartProperty : PnPWebCmdlet
    {
        private const string ParameterSet_All = "All";
        private const string ParameterSet_Key = "Key";

        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true)]
        public Guid Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Key)]
        public string Key;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }

            var properties = CurrentWeb.GetWebPartProperties(Identity, ServerRelativePageUrl);

            if (!string.IsNullOrEmpty(Key))
            {
                if (properties.FieldValues.TryGetValue(Key, out var value))
                {
                    WriteObject(value);
                }
            }
            else
            {
                var values = properties.FieldValues.Select(x => new PropertyBagValue() { Key = x.Key, Value = x.Value });
                WriteObject(values, enumerateCollection: true);
            }
        }
    }
}
