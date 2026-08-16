using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// The outcome of enumerating the credential store behind the PnP PowerShell stored credentials
    /// </summary>
    internal sealed class StoredCredentialList
    {
        /// <summary>The names credentials are stored under, with the internal storage prefix removed.</summary>
        public List<string> Names { get; } = new List<string>();

        /// <summary>Description of the credential store the names were read from, for logging purposes.</summary>
        public string Source { get; set; }

        /// <summary>Set when the store could not be read. When set, an empty <see cref="Names"/> does not mean nothing is stored.</summary>
        public string Warning { get; set; }

        /// <summary>How names are compared when de-duplicating. Native stores are case insensitive, a vault is not.</summary>
        public IEqualityComparer<string> NameComparer { get; set; } = System.StringComparer.OrdinalIgnoreCase;
    }
}
