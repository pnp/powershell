using PnP.Framework.Sites;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Set, "PnPTeamifyPromptHidden")]
    [OutputType(typeof(void))]
    public class SetTeamifyPromptHidden : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var hidden = SiteCollection.IsTeamifyPromptHiddenAsync(ClientContext).GetAwaiter().GetResult();
            if (!hidden)
            {
                SiteCollection.HideTeamifyPromptAsync(ClientContext).GetAwaiter().GetResult();
            }
            else
            {
                WriteWarning("Teamify prompt was already hidden");
            }
        }
    }
}