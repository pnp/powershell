using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Base.Completers
{
    public sealed class BrandCenterFontPackageCompleter : PnPArgumentCompleter
    {
        protected override IEnumerable<CompletionResult> GetArguments(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            ClientContext.Web.EnsureProperty(w => w.Url);
            var fonts = BrandCenterUtility.GetFontPackages(null, ClientContext, ClientContext.Web.Url);
            return fonts.Select(font => new CompletionResult(font.Title)).OrderBy(ct => ct.CompletionText);
        }
    }
}