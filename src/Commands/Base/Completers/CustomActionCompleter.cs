using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.QueryModel;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class CustomerActionCompleter : PnPArgumentCompleter
    {
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            var results = new List<CompletionResult>();
            var scope = CustomActionScope.Web;
            if (fakeBoundParameters["Scope"] != null)
            {
                scope = Enum.Parse<CustomActionScope>(fakeBoundParameters["Scope"].ToString());
            }
            switch (scope)
            {
                case CustomActionScope.Web:
                    {
                        PnPContext.Web.UserCustomActions.LoadAsync(u => u.Id);

                        foreach (var ca in PnPContext.Web.UserCustomActions)
                        {
                            results.Add(new CompletionResult(ca.Id.ToString()));
                        }
                        break;
                    }
                case CustomActionScope.Site:
                    {
                        PnPContext.Site.UserCustomActions.LoadAsync(u => u.Id);
                        foreach (var ca in PnPContext.Site.UserCustomActions)
                        {
                            results.Add(new CompletionResult(ca.Id.ToString()));
                        }
                        break;
                    }
                default:
                    {
                        PnPContext.Web.UserCustomActions.LoadAsync(u => u.Id);
                        PnPContext.Site.UserCustomActions.LoadAsync(u => u.Id);

                        foreach (var ca in PnPContext.Web.UserCustomActions)
                        {
                            results.Add(new CompletionResult(ca.Id.ToString()));
                        }
                        foreach (var ca in PnPContext.Site.UserCustomActions)
                        {
                            results.Add(new CompletionResult(ca.Id.ToString()));
                        }
                        break;
                    }

            }
            return results.Where(c => c.CompletionText.StartsWith(wordToComplete)).OrderBy(c => c.CompletionText);
        }
    }
}