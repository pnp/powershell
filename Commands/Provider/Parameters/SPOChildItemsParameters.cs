using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provider.Parameters
{
    public class SPOChildItemsParameters
    {
        [Parameter]
        public Limits Limit { get; set; }

        public enum Limits
        {
            Default,
            All = -1
        }
    }
}