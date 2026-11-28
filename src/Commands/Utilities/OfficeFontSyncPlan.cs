using System;
using System.Collections.Generic;
using System.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// What Sync-PnPBrandCenterOfficeFont changes in the Office font library, from file names only.
    /// File names are compared case-insensitively.
    /// </summary>
    internal sealed class OfficeFontSyncPlan
    {
        private static readonly StringComparer Names = StringComparer.OrdinalIgnoreCase;

        /// <summary>Source fonts the Office library does not have.</summary>
        public IReadOnlyList<string> Added { get; }
        /// <summary>Office fonts not in the source (only with removeMissing).</summary>
        public IReadOnlyList<string> Removed { get; }
        /// <summary>Office fonts that stay.</summary>
        public IReadOnlyList<string> Kept { get; }

        public bool HasChanges => Added.Count > 0 || Removed.Count > 0;
        /// <summary>True when the library would be left with no font at all.</summary>
        public bool RemovesEverything => Kept.Count + Added.Count == 0;

        private OfficeFontSyncPlan(List<string> added, List<string> removed, List<string> kept)
        {
            Added = added;
            Removed = removed;
            Kept = kept;
        }

        public static OfficeFontSyncPlan Create(IEnumerable<string> sourceFonts, IEnumerable<string> officeFonts, bool removeMissing)
        {
            var source = new HashSet<string>(sourceFonts, Names);
            var office = new HashSet<string>(officeFonts, Names);
            var added = source.Where(n => !office.Contains(n)).OrderBy(n => n, Names).ToList();
            var removed = removeMissing ? office.Where(n => !source.Contains(n)).OrderBy(n => n, Names).ToList() : [];
            var kept = office.Where(n => !removed.Contains(n, Names)).OrderBy(n => n, Names).ToList();
            return new OfficeFontSyncPlan(added, removed, kept);
        }

        public bool IsRemoved(string fileName) => Removed.Contains(fileName, Names);

        /// <summary>"add a.ttf, b.ttf; remove c.otf", for the confirmation prompt.</summary>
        public string Describe() => string.Join("; ", new[]
        {
            Added.Count > 0 ? $"add {string.Join(", ", Added)}" : null,
            Removed.Count > 0 ? $"remove {string.Join(", ", Removed)}" : null,
        }.Where(p => p != null));
    }
}
