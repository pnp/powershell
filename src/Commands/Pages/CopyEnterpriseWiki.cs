using PnP.Framework.EnterpriseWiki;
using PnP.PowerShell.Commands.Attributes;
using System;
using System.IO;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsCommon.Copy, "PnPEnterpriseWiki", DefaultParameterSetName = ParameterSetApproved, SupportsShouldProcess = true)]
    [OutputType(typeof(EnterpriseWikiCopyReceipt))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl")]
    public class CopyEnterpriseWiki : PnPWebCmdlet
    {
        private const string ParameterSetApproved = "Approved";
        private const string ParameterSetAutoApprove = "AutoApprove";

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [Alias("Path")]
        [ValidateNotNullOrEmpty]
        public string PackagePath { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetApproved)]
        [ValidateNotNullOrEmpty]
        public string ApprovedPlanDigest { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetAutoApprove)]
        public SwitchParameter AutoApprove { get; set; }

        [Parameter(Mandatory = false)]
        public string ReceiptPath { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ExecuteCmdlet()
        {
            var resolvedPackagePath = ResolveLocalPath(PackagePath);
            var package = EnterpriseWikiPackageSerializer.Load(resolvedPackagePath);
            var approvedDigest = ParameterSetName == ParameterSetAutoApprove
                ? package.PlanDigest
                : ApprovedPlanDigest;
            if (!ShouldProcess(
                    package.Plan.TargetPageServerRelativeUrl,
                    $"Create Enterprise Wiki page from approved plan {approvedDigest}"))
            {
                return;
            }

            var service = new EnterpriseWikiMigrationService();
            var receipt = service.Copy(Connection.Context, package, approvedDigest);
            var receiptPath = string.IsNullOrWhiteSpace(ReceiptPath)
                ? Path.GetDirectoryName(ResolvePackageFile(resolvedPackagePath))
                : ResolveLocalPath(ReceiptPath);
            var savedReceiptPath = EnterpriseWikiPackageSerializer.SaveReceipt(receiptPath, receipt, Force);
            WriteVerbose($"Enterprise Wiki copy receipt written to '{savedReceiptPath}'.");
            foreach (var warning in receipt.Warnings)
            {
                WriteWarning(warning);
            }
            WriteObject(receipt);
        }

        private string ResolveLocalPath(string value)
        {
            return Path.IsPathRooted(value)
                ? Path.GetFullPath(value)
                : Path.GetFullPath(Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, value));
        }

        private static string ResolvePackageFile(string value)
        {
            return Directory.Exists(value) || string.IsNullOrEmpty(Path.GetExtension(value))
                ? Path.Combine(value, EnterpriseWikiPackageSerializer.DefaultPackageFileName)
                : value;
        }
    }
}
