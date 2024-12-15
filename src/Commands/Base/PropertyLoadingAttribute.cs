using System;

namespace PnP.PowerShell.Commands.Base
{
    public class PropertyLoadingAttribute : Attribute
    {
        public int Depth { get; set; } = 1;
    }
}
