using System.Linq;
using System.Management.Automation;

using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPPropertyBag", DefaultParameterSetName = ParameterSet_All)]
    [OutputType(typeof(PropertyBagValue), ParameterSetName = new[] { ParameterSet_All })]
    [OutputType(typeof(object), ParameterSetName = new[] { ParameterSet_Key })]
    public class GetPropertyBag : PnPWebCmdlet
    {
        public const string ParameterSet_All = "All";
        public const string ParameterSet_Key = "Key";

        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_Key)]
        [ArgumentCompleter(typeof(PropertyBagKeyCompleter))]
        public string Key = string.Empty;

        [Parameter(Mandatory = false)]
        public string Folder = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrEmpty(Folder))
            {
                if (!string.IsNullOrEmpty(Key))
                {
                    WriteObject(CurrentWeb.GetPropertyBagValueString(Key, string.Empty));
                }
                else
                {
                    CurrentWeb.EnsureProperty(w => w.AllProperties);

                    var values = CurrentWeb.AllProperties.FieldValues.Select(x => new PropertyBagValue() { Key = x.Key, Value = x.Value });
                    WriteObject(values, true);
                }
            }
            else
            {
                // Folder Property Bag

                CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

                var folderUrl = UrlUtility.Combine(CurrentWeb.ServerRelativeUrl, Folder);
                var folder = CurrentWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));
                folder.EnsureProperty(f => f.Properties);

                if (!string.IsNullOrEmpty(Key))
                {
                    var value = folder.Properties.FieldValues.FirstOrDefault(x => x.Key == Key);
                    WriteObject(value.Value, true);
                }
                else
                {
                    var values = folder.Properties.FieldValues.Select(x => new PropertyBagValue() { Key = x.Key, Value = x.Value });
                    WriteObject(values, true);
                }

            }
        }
    }
}
