using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;
using System;
using PnP.PowerShell.Commands.Properties;

namespace PnP.PowerShell.Commands.Base
{
    [Cmdlet(VerbsCommon.Get, "PnPContext")]
    public class GetSPOContext : PSCmdlet
    {

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (PnPConnection.CurrentConnection == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
            if (PnPConnection.CurrentConnection.Context == null)
            {
                throw new InvalidOperationException(Resources.NoSharePointConnection);
            }
        }

        protected override void ProcessRecord()
        {
            WriteObject(PnPConnection.CurrentConnection.Context);
        }
    }
}
