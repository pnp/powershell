using PnP.Framework.Sites;

using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsTeam")]
    public class AddTeamsTeam : PnPSharePointCmdlet
    {

        protected override void ExecuteCmdlet()
        {
            var results = SiteCollection.TeamifySiteAsync(ClientContext);
            var returnedBool = results.GetAwaiter().GetResult();
            WriteObject(returnedBool);
        }
    }
}