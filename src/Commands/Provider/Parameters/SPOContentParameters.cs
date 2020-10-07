using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provider.Parameters
{
    public class SPOContentParameters
    {
        [Parameter]
        public SwitchParameter IsBinary { get; set; }
    }
}