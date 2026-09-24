using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Model.Configuration;
using PnP.Core.Provisioning.Providers.Xml;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Utilities;
using System.IO;
using System.Management.Automation;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    public partial class GetTenantTemplate
    {
        private void ExecuteCmdletExperimental()
        {
            var configuration = ParameterSpecified(nameof(Configuration))
                ? Configuration.GetCoreConfiguration(SessionState.Path.CurrentFileSystemLocation.Path, LogWarning)
                : new ExtractConfiguration();

            if (string.IsNullOrEmpty(SiteUrl))
            {
                SiteUrl = Connection.Url;
            }

            configuration.Tenant.Sequence ??= new PnP.Core.Provisioning.Model.Configuration.Tenant.Sequence.ExtractSequenceConfiguration();
            configuration.Tenant.Sequence.SiteUrls.Add(SiteUrl);

            if (ParameterSetName != PARAMETERSET_ASFILE)
            {
                WriteObject(ExtractTemplateExperimental(configuration));
                return;
            }

            if (!Path.IsPathRooted(Out))
            {
                Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
            }
            if (Out.EndsWith(".pnp", System.StringComparison.OrdinalIgnoreCase))
            {
                LogWarning("This cmdlet does not save a tenant template as a PnP file.");
            }
            if (File.Exists(Out) && !Force && !ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
            {
                return;
            }

            var fileInfo = new FileInfo(Out);
            configuration.FileConnector = new FileSystemConnector(fileInfo.DirectoryName, string.Empty);

            var tenantTemplate = ExtractTemplateExperimental(configuration);
            new XMLFileSystemTemplateProvider(fileInfo.DirectoryName, string.Empty).SaveAs(tenantTemplate, Out);
        }

        private ProvisioningHierarchy ExtractTemplateExperimental(ExtractConfiguration configuration)
        {
            var reporter = new CoreProvisioningReporter("Extracting tenant template", WriteProgress, LogWarning);
            configuration.ProgressDelegate = reporter.ProgressDelegate;
            configuration.MessagesDelegate = reporter.MessagesDelegate;

            var tenantTemplate = reporter.Run(() => PnPContext.GetProvisioningManager().GetTenantTemplateAsync(configuration));

            // The engine reports a site it cannot extract as a warning and carries on, so an extract in which
            // every site failed comes back empty rather than throwing. Saving that would look like success.
            if (tenantTemplate == null || (tenantTemplate.Templates.Count == 0 && tenantTemplate.Sequences.Count == 0 && tenantTemplate.Teams.Teams.Count == 0))
            {
                ThrowTerminatingError(new ErrorRecord(
                    new PSInvalidOperationException($"Nothing could be extracted from {SiteUrl}. The warnings above give the reason."),
                    "TenantTemplateExtractionReturnedNothing",
                    ErrorCategory.InvalidResult,
                    SiteUrl));
            }

            return tenantTemplate;
        }
    }
}
