using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Reflection.Metadata;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class ContentTypeCompleter : PnPArgumentCompleter
    {
        public override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            wordToComplete = wordToComplete.Trim('"');

            IEnumerable<ContentType> result = PnPConnection.Current.Context.LoadQuery(PnPConnection.Current.Context.Web.AvailableContentTypes.Include(f => f.Name));
            PnPConnection.Current.Context.ExecuteQueryRetry();
            foreach (var ct in result.Where(l => l.Name.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                yield return new CompletionResult($"\"{ct.Name}\"");
            }
        }
    }
}