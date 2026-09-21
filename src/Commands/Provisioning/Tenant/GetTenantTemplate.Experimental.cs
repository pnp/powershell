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

            if (tenantTemplate == null || (tenantTemplate.Templates.Count == 0 && tenantTemplate.Sequences.Count == 0))
            {
                ThrowTerminatingError(new ErrorRecord(
                    new PSNotSupportedException("The experimental PnP.Core.Provisioning engine cannot extract a tenant template yet: it ships no hierarchy extraction handlers, so the extract returns an empty template. Run Get-PnPTenantTemplate without -Experimental to extract with PnP Framework. Applying a tenant template with Invoke-PnPTenantTemplate -Experimental is supported."),
                    "TenantTemplateExtractionNotSupportedByExperimentalEngine",
                    ErrorCategory.NotImplemented,
                    SiteUrl));
            }

            return tenantTemplate;
        }
    }
}
