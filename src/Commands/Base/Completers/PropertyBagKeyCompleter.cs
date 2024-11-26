using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class PropertyBagKeyCompleter : PnPArgumentCompleter
    {
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            IEnumerable<string> keys = null;
            if (fakeBoundParameters["Folder"] == null)
            {
                ClientContext.Web.EnsureProperty(w => w.AllProperties);

                keys = ClientContext.Web.AllProperties.FieldValues.Select(x => x.Key);
            }
            else
            {
                var folderName = fakeBoundParameters["Folder"] as string;
                ClientContext.Web.EnsureProperty(w => w.ServerRelativeUrl);

                var folderUrl = UrlUtility.Combine(ClientContext.Web.ServerRelativeUrl, folderName);
                var folder = ClientContext.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));
                folder.EnsureProperty(f => f.Properties);

                keys = folder.Properties.FieldValues.Select(x => x.Key);

            }

            foreach (var key in keys.Where(k => k.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                yield return new CompletionResult(key);
            }
        }
    }
}