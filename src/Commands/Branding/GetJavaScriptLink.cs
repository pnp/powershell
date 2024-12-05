using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPJavaScriptLink")]
    public class GetJavaScriptLink : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public string Name = string.Empty;

        [Parameter(Mandatory = false)]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = false)]
        public SwitchParameter ThrowExceptionIfJavaScriptLinkNotFound;

        protected override void ExecuteCmdlet()
        {
            var actions = new List<UserCustomAction>();

            if (Scope == CustomActionScope.All || Scope == CustomActionScope.Web)
            {
                actions.AddRange(CurrentWeb.GetCustomActions().Where(c => c.Location == "ScriptLink"));
            }
            if (Scope == CustomActionScope.All || Scope == CustomActionScope.Site)
            {
                actions.AddRange(ClientContext.Site.GetCustomActions().Where(c => c.Location == "ScriptLink"));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                var foundAction = actions.FirstOrDefault(x => x.Name == Name);
                if (foundAction != null || !ThrowExceptionIfJavaScriptLinkNotFound)
                {
                    WriteObject(foundAction, true);
                }
                else
                {
                    throw new PSArgumentException($"No JavaScriptLink found with the name '{Name}' within the scope '{Scope}'", "Name");
                }
            }
            else
            {
                WriteObject(actions, true);
            }
        }
    }
}