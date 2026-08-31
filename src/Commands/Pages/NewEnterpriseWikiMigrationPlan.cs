using Microsoft.SharePoint.Client;
using PnP.Framework.EnterpriseWiki;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Model;
using System;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.New, "PnPEnterpriseWikiMigrationPlan")]
    [OutputType(typeof(EnterpriseWikiMigrationPlanResult))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    public class NewEnterpriseWikiMigrationPlan : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Path")]
        [ValidateNotNullOrEmpty]
        public string ExportPath { get; set; }

        [Parameter(Mandatory = false)]
        public string OutputPath { get; set; }

        [Parameter(Mandatory = false)]
        public string TargetPageName { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowUniquePermissions { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter AllowManagedMetadataSubstitution { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter BlockExternalResources { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ExecuteCmdlet()
        {
            var resolvedExportPath = ResolveLocalPath(ExportPath);
            var export = EnterpriseWikiPackageSerializer.LoadExport(resolvedExportPath);
            Connection.Context.Load(Connection.Context.Web, web => web.ServerRelativeUrl);
            Connection.Context.ExecuteQueryRetry();
            var targetPages = Connection.Context.Web.GetPagesLibrary();
            var targetPagesRoot = $"{Connection.Context.Web.ServerRelativeUrl.TrimEnd('/')}/Pages";
            if (targetPages != null)
            {
                Connection.Context.Load(targetPages.RootFolder, folder => folder.ServerRelativeUrl);
                Connection.Context.ExecuteQueryRetry();
                targetPagesRoot = targetPages.RootFolder.ServerRelativeUrl;
            }

            var targetPagePath = ResolveTargetPagePath(
                targetPagesRoot,
                string.IsNullOrWhiteSpace(TargetPageName)
                    ? GetLeafName(export.Snapshot.Source.PageServerRelativeUrl)
                    : TargetPageName);

            var service = new EnterpriseWikiMigrationService();
            var package = service.Plan(Connection.Context, export, new EnterpriseWikiPlanningOptions
            {
                TargetPageServerRelativeUrl = targetPagePath,
                RequireInheritedPermissions = !AllowUniquePermissions,
                BlockOnManagedMetadata = !AllowManagedMetadataSubstitution,
                AllowExternalResourceReferences = !BlockExternalResources,
                CreateOnly = true
            });
            var packageOutput = string.IsNullOrWhiteSpace(OutputPath)
                ? Path.GetDirectoryName(ResolveExportFile(resolvedExportPath))
                : ResolveLocalPath(OutputPath);
            var packagePath = EnterpriseWikiPackageSerializer.SaveMigration(packageOutput, package, Force);
            var reportPath = Path.Combine(
                Path.GetDirectoryName(packagePath) ?? string.Empty,
                EnterpriseWikiPackageSerializer.DefaultReportFileName);
            WriteVerbose($"Enterprise Wiki migration plan written to '{packagePath}'. Plan digest: {package.PlanDigest}");
            foreach (var warning in package.Plan.Warnings)
            {
                WriteWarning(warning);
            }
            foreach (var blocker in package.Plan.Blockers)
            {
                WriteWarning($"BLOCKER: {blocker}");
            }
            WriteObject(new EnterpriseWikiMigrationPlanResult(package, packagePath, reportPath));
        }

        private string ResolveLocalPath(string value)
        {
            return Path.IsPathRooted(value)
                ? Path.GetFullPath(value)
                : Path.GetFullPath(Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, value));
        }

        private static string ResolveTargetPagePath(string pagesRoot, string value)
        {
            var candidate = value.Trim();
            if (Uri.TryCreate(candidate, UriKind.Absolute, out _) || candidate.StartsWith("/", StringComparison.Ordinal))
            {
                return candidate;
            }

            var fileName = candidate.ReplaceInvalidUrlChars("-");
            if (!fileName.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase))
            {
                fileName += ".aspx";
            }
            return $"{pagesRoot.TrimEnd('/')}/{fileName}";
        }

        private static string ResolveExportFile(string value)
        {
            return Directory.Exists(value) || string.IsNullOrEmpty(Path.GetExtension(value))
                ? Path.Combine(value, EnterpriseWikiPackageSerializer.DefaultExportFileName)
                : value;
        }

        private static string GetLeafName(string value)
        {
            var path = Uri.TryCreate(value, UriKind.Absolute, out var absolute) ? absolute.AbsolutePath : value;
            path = Uri.UnescapeDataString(path ?? string.Empty).Replace('\\', '/').TrimEnd('/');
            var separator = path.LastIndexOf('/');
            return separator < 0 ? path : path.Substring(separator + 1);
        }
    }
}
