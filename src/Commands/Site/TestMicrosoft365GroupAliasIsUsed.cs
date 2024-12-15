using PnP.Framework.Sites;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsDiagnostic.Test, "PnPMicrosoft365GroupAliasIsUsed")]
    [OutputType(typeof(bool))]
    public class TestMicrosoft365GroupAliasIsUsed : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Alias;

        protected override void ExecuteCmdlet()
        {
            var results = SiteCollection.AliasExistsAsync(ClientContext, Alias);
            var returnedBool = results.GetAwaiter().GetResult();
            WriteObject(returnedBool);
        }
    }
}