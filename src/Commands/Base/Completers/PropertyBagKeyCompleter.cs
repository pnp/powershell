using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Reflection.Metadata;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class PropertyBagKeyCompleter : PnPArgumentCompleter
    {
        public override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters, char quoteChar)
        {
            IEnumerable<string> keys = null;
            if (fakeBoundParameters["Folder"] == null)
            {
                PnPConnection.Current.Context.Web.EnsureProperty(w => w.AllProperties);

                keys = PnPConnection.Current.Context.Web.AllProperties.FieldValues.Select(x => x.Key);
            }
            else
            {
                var folderName = fakeBoundParameters["Folder"] as string;
                PnPConnection.Current.Context.Web.EnsureProperty(w => w.ServerRelativeUrl);

                var folderUrl = UrlUtility.Combine(PnPConnection.Current.Context.Web.ServerRelativeUrl, folderName);
                var folder = PnPConnection.Current.Context.Web.GetFolderByServerRelativePath(ResourcePath.FromDecodedUrl(folderUrl));
                folder.EnsureProperty(f => f.Properties);

                keys = folder.Properties.FieldValues.Select(x => x.Key);

            }

            foreach (var key in keys.Where(k => k.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                yield return new CompletionResult($"{quoteChar}{key}{quoteChar}");
            }
        }
    }
}