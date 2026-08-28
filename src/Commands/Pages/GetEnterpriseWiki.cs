using Microsoft.SharePoint.Client;
using PnP.Framework.EnterpriseWiki;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Get, "PnPEnterpriseWiki", DefaultParameterSetName = ParameterSetIdentity)]
    [OutputType(typeof(EnterpriseWikiMigrationPackage))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    public class GetEnterpriseWiki : PnPWebCmdlet
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
        [ValidateNotNull]
        public PnPConnection TargetConnection { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OutputPath { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetIdentity)]
        public string TargetPageName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = ParameterSetAll)]
        public string TargetPagePrefix { get; set; } = "pnp-ewiki";

        [Parameter(Mandatory = false)]
        public SwitchParameter Draft { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter NoWebParts { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowUniquePermissions { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowManagedMetadataSubstitution { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter BlockExternalResources { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateRange(1, long.MaxValue)]
        public long MaximumDependencyBytes { get; set; } = 10 * 1024 * 1024;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ExecuteCmdlet()
        {
            var service = new EnterpriseWikiMigrationService();
            var sourceContext = Connection.Context;
            var targetContext = TargetConnection.Context;
            var targetPages = targetContext.Web.GetPagesLibrary();
            targetContext.Load(targetPages.RootFolder, folder => folder.ServerRelativeUrl);
            targetContext.ExecuteQueryRetry();

            IReadOnlyList<string> sourcePages = ParameterSetName == ParameterSetAll
                ? service.Discover(sourceContext)
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
                var targetName = ParameterSetName == ParameterSetAll
                    ? $"{TargetPagePrefix}-{index + 1:D3}-{GetLeafName(sourcePage)}"
                    : string.IsNullOrWhiteSpace(TargetPageName) ? GetLeafName(sourcePage) : TargetPageName;
                targetName = EnsureAspx(targetName.ReplaceInvalidUrlChars("-"));
                var targetPagePath = $"{targetPages.RootFolder.ServerRelativeUrl.TrimEnd('/')}/{targetName}";
                var itemOutputPath = ParameterSetName == ParameterSetAll
                    ? Path.Combine(outputRoot, $"{index + 1:D3}-{MakeSafeDirectoryName(Path.GetFileNameWithoutExtension(targetName))}")
                    : outputRoot;

                WriteProgress(new ProgressRecord(
                    181,
                    "Capture Enterprise Wiki migration package",
                    $"{index + 1}/{sourcePages.Count}: {sourcePage}")
                {
                    PercentComplete = (index * 100) / sourcePages.Count
                });

                var package = service.Capture(sourceContext, targetContext, new EnterpriseWikiCaptureOptions
                {
                    SourcePageServerRelativeUrl = sourcePage,
                    TargetPageServerRelativeUrl = targetPagePath,
                    IncludeWebParts = !NoWebParts,
                    Publish = !Draft,
                    RequireInheritedPermissions = !AllowUniquePermissions,
                    BlockOnManagedMetadata = !AllowManagedMetadataSubstitution,
                    AllowExternalResourceReferences = !BlockExternalResources,
                    MaximumDependencyBytes = MaximumDependencyBytes
                });
                var packagePath = EnterpriseWikiPackageSerializer.Save(itemOutputPath, package, Force);
                WriteVerbose($"Enterprise Wiki package written to '{packagePath}'. Plan digest: {package.PlanDigest}");
                foreach (var warning in package.Plan.Warnings)
                {
                    WriteWarning(warning);
                }
                foreach (var blocker in package.Plan.Blockers)
                {
                    WriteWarning($"BLOCKER: {blocker}");
                }
                WriteObject(package);
            }

            WriteProgress(new ProgressRecord(181, "Capture Enterprise Wiki migration package", "Completed")
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

        private static string EnsureAspx(string value)
        {
            return value.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase) ? value : value + ".aspx";
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
