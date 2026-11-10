using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>The outcome of enumerating the credential store behind the PnP PowerShell stored credentials.</summary>
    internal sealed class StoredCredentialList
    {
        /// <summary>The names credentials are stored under, with the internal storage prefix removed.</summary>
        public List<string> Names { get; } = new List<string>();

        /// <summary>Description of the credential store the names were read from, for logging purposes.</summary>
        public string Source { get; set; }

        /// <summary>Set when the store was read but the result may be incomplete, or holds nothing this cmdlet can return.</summary>
        public string Warning { get; set; }

        /// <summary>Set when the store could not be read at all, so that a failed request is never reported as an empty store.</summary>
        public string Failure { get; set; }
    }
}
