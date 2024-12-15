using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provider.SPOProxy
{
    [Cmdlet(CmdletVerb, CmdletNoun, DefaultParameterSetName = "Path", SupportsShouldProcess = true, SupportsTransactions = true)]
    
    public class SPOProxyCopyItem : SPOProxyCmdletBase
    {
        public const string CmdletVerb = "Copy";

        internal override string CmdletType => CmdletVerb;

        [Parameter]
        public override SwitchParameter Recurse { get; set; }

        protected override void ProcessRecord()
        {
            SPOProxyImplementation.CopyMoveImplementation(this);
        }
    }
}
