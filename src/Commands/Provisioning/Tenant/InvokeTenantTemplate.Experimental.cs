using PnP.Core.Provisioning.Connectors;
using PnP.Core.Provisioning.Model;
using PnP.Core.Provisioning.Model.Configuration;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    public partial class InvokeTenantTemplate
    {
        private void ExecuteCmdletExperimental()
        {
            var hierarchy = LoadHierarchyExperimental();
            if (hierarchy == null)
            {
                LogError("The -Path parameter targets an invalid repository or template object.");
                return;
            }

            if (Parameters != null)
            {
                foreach (var parameter in Parameters.Keys)
                {
                    hierarchy.Parameters[parameter.ToString()] = Parameters[parameter].ToString();
                }
            }

            if ((hierarchy.Teams != null || hierarchy.AzureActiveDirectory != null) && !HasGraphConsent())
            {
                throw new PSInvalidOperationException($"Your template contains artifacts that require an access token for https://{Connection.GraphEndPoint}. Please provide consent to the EntraID application first by executing: Register-PnPEntraIDApp or Register-PnPEntraIDAppForInteractiveLogin");
            }

            var sitesProvisioned = new List<ProvisionedSite>();
            var sitesProvisionedLock = new object();
            var configuration = BuildApplyConfigurationExperimental();
            configuration.SiteProvisionedDelegate = (title, url) =>
            {
                // The engine reports this from its own thread.
                lock (sitesProvisionedLock)
                {
                    if (sitesProvisioned.All(site => site.Url != url))
                    {
                        sitesProvisioned.Add(new ProvisionedSite { Title = title, Url = url });
                    }
                }
            };

            var reporter = new CoreProvisioningReporter("Applying template to tenant", WriteProgress, LogWarning);
            configuration.ProgressDelegate = reporter.ProgressDelegate;
            configuration.MessagesDelegate = reporter.MessagesDelegate;

            var manager = PnPContext.GetProvisioningManager();
            reporter.Run(async () =>
            {
                if (!string.IsNullOrEmpty(SequenceId))
                {
                    await manager.ApplyTenantTemplateAsync(hierarchy, SequenceId, configuration).ConfigureAwait(false);
                }
                else if (hierarchy.Sequences.Count > 0)
                {
                    foreach (var sequence in hierarchy.Sequences)
                    {
                        await manager.ApplyTenantTemplateAsync(hierarchy, sequence.ID, configuration).ConfigureAwait(false);
                    }
                }
                else
                {
                    await manager.ApplyTenantTemplateAsync(hierarchy, null, configuration).ConfigureAwait(false);
                }
            });

            WriteObject(sitesProvisioned, true);
        }

        private ProvisioningHierarchy LoadHierarchyExperimental()
        {
            if (ParameterSpecified(nameof(TemplateProviderExtensions)))
            {
                LogWarning("-TemplateProviderExtensions has no effect with -Experimental: template provider extensions are not run by the PnP.Core.Provisioning engine.");
            }

            if (ParameterSetName == ParameterSet_PATH)
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                if (!System.IO.File.Exists(Path))
                {
                    throw new FileNotFoundException($"File {Path} does not exist.");
                }
                return CoreProvisioningHelper.LoadTenantTemplateFromFile(Path, LogError);
            }

            var hierarchy = Template?.GetCoreHierarchy();
            if (hierarchy != null)
            {
                hierarchy.Connector = ResolveResourceConnectorExperimental();
            }
            return hierarchy;
        }

        private FileConnectorBase ResolveResourceConnectorExperimental()
        {
            if (ResourceFolder != null)
            {
                if (!System.IO.Path.IsPathRooted(ResourceFolder))
                {
                    ResourceFolder = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, ResourceFolder);
                }
                return new FileSystemConnector(ResourceFolder, string.Empty);
            }

            if (Path == null)
            {
                return new FileSystemConnector(SessionState.Path.CurrentFileSystemLocation.Path, string.Empty);
            }

            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            return new FileSystemConnector(new FileInfo(Path).DirectoryName, string.Empty);
        }

        private ApplyConfiguration BuildApplyConfigurationExperimental()
        {
            var configuration = ParameterSpecified(nameof(Configuration))
                ? Configuration.GetCoreConfiguration(SessionState.Path.CurrentFileSystemLocation.Path, LogWarning)
                : new ApplyConfiguration();

            var handlers = this.Handlers;
            if (ParameterSpecified(nameof(ExcludeHandlers)))
            {
                handlers |= CoreProvisioningHelper.InvertExcludedHandlers(this.ExcludeHandlers);
            }
            if (ParameterSpecified(nameof(Handlers)) || ParameterSpecified(nameof(ExcludeHandlers)))
            {
                configuration.Handlers = CoreProvisioningHelper.ToConfigurationHandlers(handlers);
            }

            if (ExtensibilityHandlers != null)
            {
                configuration.Extensibility.Handlers = CoreProvisioningHelper.ToCoreExtensibilityHandlers(ExtensibilityHandlers);
            }

            configuration.PropertyBag.OverwriteSystemValues = OverwriteSystemPropertyBagValues;
            configuration.Lists.IgnoreDuplicateDataRowErrors = IgnoreDuplicateDataRowErrors;
            configuration.Navigation.ClearNavigation = ClearNavigation;
            configuration.ContentTypes.ProvisionContentTypesToSubWebs = ProvisionContentTypesToSubWebs;
            configuration.Fields.ProvisionFieldsToSubWebs = ProvisionFieldsToSubWebs;

            return configuration;
        }

        private bool HasGraphConsent()
        {
            try
            {
                return !string.IsNullOrEmpty(GraphAccessToken);
            }
            catch
            {
                return false;
            }
        }
    }
}
