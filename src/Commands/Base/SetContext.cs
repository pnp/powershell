using System.Management.Automation;

using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Set, "PnPContext")]
    public class SetContext : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        public ClientContext Context;

        protected override void ProcessRecord()
        {
            PnPConnection.Current.Context = Context;
        }
    }
}
