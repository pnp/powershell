using System.Management.Automation;

using System;
using PnP.PowerShell.Commands.Properties;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPContext")]
    [OutputType(typeof(Microsoft.SharePoint.Client.ClientContext))]
    public class GetSPOContext : PSCmdlet
    {

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (PnPConnection.Current == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
            if (PnPConnection.Current.Context == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
        }

        protected override void ProcessRecord()
        {
            WriteObject(PnPConnection.Current.Context);
        }
    }
}
