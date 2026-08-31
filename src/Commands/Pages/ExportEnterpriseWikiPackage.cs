using PnP.Framework.EnterpriseWiki;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsData.Export, "PnPEnterpriseWikiPackage", DefaultParameterSetName = ParameterSetIdentity)]
    [OutputType(typeof(EnterpriseWikiExportResult))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    public class ExportEnterpriseWikiPackage : PnPWebCmdlet
    {
        private const string ParameterSetIdentity = "Identity";
        private const string ParameterSetAll = "All";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSetIdentity)]
        [Alias("ServerRelativeUrl")]
        [ValidateNotNullOrEmpty]
        public string Identity { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAll)]
        public SwitchParameter All { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OutputPath { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWebParts { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateRange(1, long.MaxValue)]
        public long MaximumDependencyBytes { get; set; } = 10 * 1024 * 1024;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ExecuteCmdlet()
        {
            var service = new EnterpriseWikiMigrationService();
            IReadOnlyList<string> sourcePages = ParameterSetName == ParameterSetAll
                ? service.Discover(Connection.Context)
                : new[] { Identity };
            if (sourcePages.Count == 0)
            {
                WriteVerbose("No Enterprise Wiki pages were found in the current web.");
                return;
            }

            var outputRoot = ResolveLocalPath(OutputPath);
            for (var index = 0; index < sourcePages.Count; index++)
            {
                var sourcePage = sourcePages[index];
                var itemOutputPath = ParameterSetName == ParameterSetAll
                    ? Path.Combine(outputRoot, $"{index + 1:D3}-{MakeSafeDirectoryName(Path.GetFileNameWithoutExtension(GetLeafName(sourcePage)))}")
                    : outputRoot;

                WriteProgress(new ProgressRecord(
                    181,
                    "Export Enterprise Wiki source snapshot",
                    $"{index + 1}/{sourcePages.Count}: {sourcePage}")
                {
                    PercentComplete = (index * 100) / sourcePages.Count
                });

                var export = service.Export(Connection.Context, new EnterpriseWikiExportOptions
                {
                    SourcePageServerRelativeUrl = sourcePage,
                    IncludeWebParts = !NoWebParts,
                    MaximumDependencyBytes = MaximumDependencyBytes
                });
                var exportPath = EnterpriseWikiPackageSerializer.SaveExport(itemOutputPath, export, Force);
                WriteVerbose($"Enterprise Wiki export written to '{exportPath}'. Snapshot digest: {export.SnapshotDigest}");
                foreach (var warning in export.Snapshot.Warnings)
                {
                    WriteWarning(warning);
                }
                foreach (var blocker in export.Snapshot.Blockers)
                {
                    WriteWarning($"BLOCKER: {blocker}");
                }
                WriteObject(new EnterpriseWikiExportResult(export, exportPath));
            }

            WriteProgress(new ProgressRecord(181, "Export Enterprise Wiki source snapshot", "Completed")
            {
                RecordType = ProgressRecordType.Completed
            });
        }

        private string ResolveLocalPath(string value)
        {
            return Path.IsPathRooted(value)
                ? Path.GetFullPath(value)
                : Path.GetFullPath(Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, value));
        }

        private static string GetLeafName(string value)
        {
            var path = Uri.TryCreate(value, UriKind.Absolute, out var absolute) ? absolute.AbsolutePath : value;
            path = Uri.UnescapeDataString(path ?? string.Empty).Replace('\\', '/').TrimEnd('/');
            var separator = path.LastIndexOf('/');
            return separator < 0 ? path : path.Substring(separator + 1);
        }

        private static string MakeSafeDirectoryName(string value)
        {
            var invalid = Path.GetInvalidFileNameChars();
            return new string((value ?? "enterprise-wiki")
                .Select(character => invalid.Contains(character) ? '-' : character)
                .ToArray());
        }
    }
}
