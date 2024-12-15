using System.Management.Automation;
using System;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPIsSiteAliasAvailable")]
    public class GetIsSiteAliasAvailable : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [Alias("Alias")]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            var proposedUrl = PnP.Framework.Sites.SiteCollection.GetValidSiteUrlFromAliasAsync(ClientContext, Identity).Result;

            var record = new PSObject();
            record.Properties.Add(new PSVariableProperty(new PSVariable("IsAvailable", proposedUrl.EndsWith($"/{Identity}", StringComparison.InvariantCultureIgnoreCase))));
            record.Properties.Add(new PSVariableProperty(new PSVariable("ProposedAlias", proposedUrl.Remove(0, proposedUrl.LastIndexOf('/') + 1))));
            record.Properties.Add(new PSVariableProperty(new PSVariable("ProposedUrl", proposedUrl)));

            WriteObject(record);
        }
    }
}