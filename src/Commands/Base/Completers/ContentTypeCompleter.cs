using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class ContentTypeCompleter : PnPArgumentCompleter
    {
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            IEnumerable<ContentType> result = ClientContext.LoadQuery(ClientContext.Web.AvailableContentTypes.Include(f => f.Name));
            ClientContext.ExecuteQueryRetry();
            foreach (var ct in result.Where(l => l.Name.StartsWith(wordToComplete, StringComparison.InvariantCultureIgnoreCase)))
            {
                yield return new CompletionResult(ct.Name);
            }
        }
    }
}