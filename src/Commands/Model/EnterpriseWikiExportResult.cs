using PnP.Framework.EnterpriseWiki;
using System;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Describes a sealed source-only Enterprise Wiki export and its local file.
    /// </summary>
    public sealed class EnterpriseWikiExportResult
    {
        public EnterpriseWikiExportResult(EnterpriseWikiExportPackage export, string exportPath)
        {
            Export = export ?? throw new ArgumentNullException(nameof(export));
            ExportPath = string.IsNullOrWhiteSpace(exportPath)
                ? throw new ArgumentException("An export path is required.", nameof(exportPath))
                : exportPath;
        }

        public EnterpriseWikiExportPackage Export { get; }

        public string ExportPath { get; }

        public string SchemaVersion => Export.SchemaVersion;

        public DateTimeOffset ExportedAtUtc => Export.ExportedAtUtc;

        public string SnapshotDigest => Export.SnapshotDigest;

        public EnterpriseWikiSnapshot Snapshot => Export.Snapshot;

        public override string ToString()
        {
            return ExportPath;
        }
    }
}
