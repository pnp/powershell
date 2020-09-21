using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands.Provider.SPOProxy
{
    [Cmdlet(CmdletVerb, CmdletNoun, DefaultParameterSetName = "Path", SupportsShouldProcess = true, SupportsTransactions = true)]
    
    public class SPOProxyMoveItem : SPOProxyCmdletBase
    {
        public const string CmdletVerb = "Move";

        internal override string CmdletType => CmdletVerb;

        public override SwitchParameter Recurse => true;

        protected override void ProcessRecord()
        {
            SPOProxyImplementation.CopyMoveImplementation(this);
        }
    }
}
