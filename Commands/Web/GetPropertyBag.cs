using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPPropertyBag")]
    public class GetPropertyBag : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public string Key = string.Empty;

        [Parameter(Mandatory = false)]
        public string Folder = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrEmpty(Folder))
            {
                if (!string.IsNullOrEmpty(Key))
                {
                    WriteObject(SelectedWeb.GetPropertyBagValueString(Key, string.Empty));
                }
                else
                {
                    SelectedWeb.EnsureProperty(w => w.AllProperties);
                    
                    var values = SelectedWeb.AllProperties.FieldValues.Select(x => new PropertyBagValue() { Key = x.Key, Value = x.Value });
                    WriteObject(values, true);
                }
            }
            else
            {
                // Folder Property Bag

                SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);
                
                var folderUrl = UrlUtility.Combine(SelectedWeb.ServerRelativeUrl, Folder);
                var folder = SelectedWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));
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

    public class PropertyBagValue
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
