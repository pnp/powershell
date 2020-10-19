using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.Graph
{
    public class IdentitySet
    {
        public Identity Application { get; set; }
        public Identity Device { get; set; }
        public Identity User { get; set; }
        public IDictionary<string, object> AdditionalData { get; set; }
    }
}
