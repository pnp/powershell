using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPPropertyBagValue")]
    public class SetPropertyBagValue : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Web")]
        [Parameter(Mandatory = true, ParameterSetName = "Folder")]
        public string Key;

        [Parameter(Mandatory = true, ParameterSetName = "Web")]
        [Parameter(Mandatory = true, ParameterSetName = "Folder")]
        [Parameter(Mandatory = true)]
        public string Value;

        [Parameter(Mandatory = true, ParameterSetName = "Web")]
        public SwitchParameter Indexed;

        [Parameter(Mandatory = false, ParameterSetName = "Folder")]
        public string Folder;

        protected override void ExecuteCmdlet()
        {
            try
            {
                if (!ParameterSpecified(nameof(Folder)))
                {
                    if (!Indexed)
                    {
                        // If it is already an indexed property we still have to add it back to the indexed properties
                        Indexed = !string.IsNullOrEmpty(SelectedWeb.GetIndexedPropertyBagKeys().FirstOrDefault(k => k == Key));
                    }

                    SelectedWeb.SetPropertyBagValue(Key, Value);
                    if (Indexed)
                    {
                        SelectedWeb.AddIndexedPropertyBagKey(Key);
                    }
                    else
                    {
                        SelectedWeb.RemoveIndexedPropertyBagKey(Key);
                    }
                }
                else
                {
                    SelectedWeb.EnsureProperty(w => w.ServerRelativeUrl);

                    var folderUrl = UrlUtility.Combine(SelectedWeb.ServerRelativeUrl, Folder);
                    var folder = SelectedWeb.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));

                    folder.EnsureProperty(f => f.Properties);

                    folder.Properties[Key] = Value;
                    folder.Update();
                    ClientContext.ExecuteQueryRetry();
                }
            }
            catch (Exception ex)
            {
                if (ex is ServerUnauthorizedAccessException)
                {
                    if (SelectedWeb.IsNoScriptSite())
                    {
                        ThrowTerminatingError(new ErrorRecord(new Exception($"{ex.Message} Site might have NoScript enabled, this prevents setting some property bag values.", ex), "NoScriptEnabled", ErrorCategory.InvalidOperation, this));
                        return;
                    }
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
