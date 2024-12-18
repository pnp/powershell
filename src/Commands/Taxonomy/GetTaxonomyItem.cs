using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsCommon.Get, "PnPTaxonomyItem")]
    public class GetTaxonomyItem : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [Alias("Term")]
        public string TermPath;

        protected override void ExecuteCmdlet()
        {
            var item = ClientContext.Site.GetTaxonomyItemByPath(TermPath);
            WriteObject(item);
        }

    }
}
