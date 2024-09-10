using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Model.Configuration;
using PnP.Framework.Provisioning.ObjectHandlers;
using PnP.Framework.Provisioning.Providers;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsLifecycle.Invoke, "PnPTenantTemplate")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class InvokeTenantTemplate : PnPAdminCmdlet
    {
        private const string ParameterSet_PATH = "By Path";
        private const string ParameterSet_OBJECT = "By Object";

        private ProgressRecord progressRecord = new ProgressRecord(0, "Activity", "Status");
        private ProgressRecord subProgressRecord = new ProgressRecord(1, "Activity", "Status");

        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_PATH)]
        public string Path;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = ParameterSet_OBJECT)]
        public ProvisioningHierarchy Template;

        [Parameter(Mandatory = false)]
        public string SequenceId;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string ResourceFolder;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Handlers Handlers;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Handlers ExcludeHandlers;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ExtensibilityHandler[] ExtensibilityHandlers;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public Hashtable Parameters;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter OverwriteSystemPropertyBagValues;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SwitchParameter IgnoreDuplicateDataRowErrors;

        [Parameter(Mandatory = false)]
        public SwitchParameter ProvisionContentTypesToSubWebs;

        [Parameter(Mandatory = false)]
        public SwitchParameter ProvisionFieldsToSubWebs;

        [Parameter(Mandatory = false)]
        public SwitchParameter ClearNavigation;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public ApplyConfigurationPipeBind Configuration;

        protected override void ExecuteCmdlet()
        {

            var sitesProvisioned = new List<ProvisionedSite>();
            var configuration = new ApplyConfiguration();
            if (ParameterSpecified(nameof(Configuration)))
            {
                configuration = Configuration.GetConfiguration(SessionState.Path.CurrentFileSystemLocation.Path);
            }

            configuration.SiteProvisionedDelegate = (title, url) =>
            {
                if (sitesProvisioned.FirstOrDefault(s => s.Url == url) == null)
                {
                    sitesProvisioned.Add(new ProvisionedSite() { Title = title, Url = url });
                }
            };

            if (ParameterSpecified(nameof(Handlers)))
            {
                if (!Handlers.Has(Handlers.All))
                {
                    foreach (var enumValue in (Handlers[])Enum.GetValues(typeof(Handlers)))
                    {
                        if (Handlers.Has(enumValue))
                        {
                            if (enumValue == Handlers.TermGroups)
                            {
                                configuration.Handlers.Add(ConfigurationHandler.Taxonomy);
                            }
                            else if (enumValue == Handlers.PageContents)
                            {
                                configuration.Handlers.Add(ConfigurationHandler.Pages);
                            }
                            else if (Enum.TryParse<ConfigurationHandler>(enumValue.ToString(), out ConfigurationHandler configHandler))
                            {
                                configuration.Handlers.Add(configHandler);
                            }
                        }
                    }
                }
            }
            if (ParameterSpecified(nameof(ExcludeHandlers)))
            {
                foreach (var handler in (Handlers[])Enum.GetValues(typeof(Handlers)))
                {
                    if (!ExcludeHandlers.Has(handler) && handler != Handlers.All)
                    {
                        if (handler == Handlers.TermGroups)
                        {
                            if (configuration.Handlers.Contains(ConfigurationHandler.Taxonomy))
                            {
                                configuration.Handlers.Remove(ConfigurationHandler.Taxonomy);
                            }
                            else if (Enum.TryParse<ConfigurationHandler>(handler.ToString(), out ConfigurationHandler configHandler))
                            {
                                if (configuration.Handlers.Contains(configHandler))
                                {
                                    configuration.Handlers.Remove(configHandler);
                                }
                            }
                        }
                    }
                }
            }

            if (ExtensibilityHandlers != null)
            {
                configuration.Extensibility.Handlers = ExtensibilityHandlers.ToList();
            }

            configuration.ProgressDelegate = (message, step, total) =>
            {
                if (message != null)
                {
                    var percentage = Convert.ToInt32((100 / Convert.ToDouble(total)) * Convert.ToDouble(step));
                    progressRecord.Activity = $"Applying template to tenant";
                    progressRecord.StatusDescription = message;
                    progressRecord.PercentComplete = percentage;
                    progressRecord.RecordType = ProgressRecordType.Processing;
                    WriteProgress(progressRecord);
                }
            };

            var warningsShown = new List<string>();

            configuration.MessagesDelegate = (message, type) =>
            {
                switch (type)
                {
                    case ProvisioningMessageType.Warning:
                        {
                            if (!warningsShown.Contains(message))
                            {
                                WriteWarning(message);
                                warningsShown.Add(message);
                            }
                            break;
                        }
                    case ProvisioningMessageType.Progress:
                        {
                            if (message != null)
                            {
                                var activity = message;
                                if (message.IndexOf("|") > -1)
                                {
                                    var messageSplitted = message.Split('|');
                                    if (messageSplitted.Length == 4)
                                    {
                                        var current = double.Parse(messageSplitted[2]);
                                        var total = double.Parse(messageSplitted[3]);
                                        subProgressRecord.RecordType = ProgressRecordType.Processing;
                                        subProgressRecord.Activity = string.IsNullOrEmpty(messageSplitted[0]) ? "-" : messageSplitted[0];
                                        subProgressRecord.StatusDescription = string.IsNullOrEmpty(messageSplitted[1]) ? "-" : messageSplitted[1];
                                        subProgressRecord.PercentComplete = Convert.ToInt32((100 / total) * current);
                                        WriteProgress(subProgressRecord);
                                    }
                                    else
                                    {
                                        subProgressRecord.Activity = "Processing";
                                        subProgressRecord.RecordType = ProgressRecordType.Processing;
                                        subProgressRecord.StatusDescription = activity;
                                        subProgressRecord.PercentComplete = 0;
                                        WriteProgress(subProgressRecord);
                                    }
                                }
                                else
                                {
                                    subProgressRecord.Activity = "Processing";
                                    subProgressRecord.RecordType = ProgressRecordType.Processing;
                                    subProgressRecord.StatusDescription = activity;
                                    subProgressRecord.PercentComplete = 0;
                                    WriteProgress(subProgressRecord);
                                }
                            }
                            break;
                        }
                    case ProvisioningMessageType.Completed:
                        {

                            WriteProgress(new ProgressRecord(1, message, " ") { RecordType = ProgressRecordType.Completed });
                            break;
                        }
                }
            };

            configuration.PropertyBag.OverwriteSystemValues = OverwriteSystemPropertyBagValues;
            configuration.Lists.IgnoreDuplicateDataRowErrors = IgnoreDuplicateDataRowErrors;
            configuration.Navigation.ClearNavigation = ClearNavigation;
            configuration.ContentTypes.ProvisionContentTypesToSubWebs = ProvisionContentTypesToSubWebs;
            configuration.Fields.ProvisionFieldsToSubWebs = ProvisionFieldsToSubWebs;

            ProvisioningHierarchy hierarchyToApply = null;

            switch (ParameterSetName)
            {
                case ParameterSet_PATH:
                    {
                        hierarchyToApply = GetHierarchy();
                        break;
                    }
                case ParameterSet_OBJECT:
                    {
                        hierarchyToApply = Template;
                        if (ResourceFolder != null)
                        {
                            var fileSystemConnector = new FileSystemConnector(ResourceFolder, "");
                            hierarchyToApply.Connector = fileSystemConnector;
                        }
                        else
                        {
                            if (Path != null)
                            {
                                if (!System.IO.Path.IsPathRooted(Path))
                                {
                                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                                }
                            }
                            else
                            {
                                Path = SessionState.Path.CurrentFileSystemLocation.Path;
                            }
                            var fileInfo = new FileInfo(Path);
                            var fileConnector = new FileSystemConnector(fileInfo.DirectoryName, "");
                            hierarchyToApply.Connector = fileConnector;
                        }
                        break;
                    }
            }
            if (Parameters != null)
            {
                foreach (var parameter in Parameters.Keys)
                {
                    if (hierarchyToApply.Parameters.ContainsKey(parameter.ToString()))
                    {
                        hierarchyToApply.Parameters[parameter.ToString()] = Parameters[parameter].ToString();
                    }
                    else
                    {
                        hierarchyToApply.Parameters.Add(parameter.ToString(), Parameters[parameter].ToString());
                    }
                }
            }
            // check if consent is needed and in place
            var consentRequired = false;
            if (hierarchyToApply.Teams != null)
            {
                consentRequired = true;
            }
            if (hierarchyToApply.AzureActiveDirectory != null)
            {
                consentRequired = true;
            }
            if (consentRequired)
            {
                // try to retrieve an access token for the Microsoft Graph:
                try
                {
                    var accessToken = GraphAccessToken;
                }
                catch
                {
                    throw new PSInvalidOperationException($"Your template contains artifacts that require an access token for https://{Connection.GraphEndPoint}. Please provide consent to the EntraID application first by executing: Register-PnPEntraIDApp or Register-PnPEntraIDAppForInteractiveLogin");
                }
            }

            using (var provisioningContext = new PnPProvisioningContext(async (resource, scope) =>
            {
                return await TokenRetrieval.GetAccessTokenAsync(resource, scope, Connection);
            }, azureEnvironment: Connection.AzureEnvironment))
            {
                if (!string.IsNullOrEmpty(SequenceId))
                {
                    Tenant.ApplyTenantTemplate(hierarchyToApply, SequenceId, configuration);
                }
                else
                {
                    if (hierarchyToApply.Sequences.Count > 0)
                    {
                        foreach (var sequence in hierarchyToApply.Sequences)
                        {
                            Tenant.ApplyTenantTemplate(hierarchyToApply, sequence.ID, configuration);
                        }
                    }
                    else
                    {
                        Tenant.ApplyTenantTemplate(hierarchyToApply, null, configuration);
                    }
                }
            }
            WriteObject(sitesProvisioned, true);

        }


        private ProvisioningHierarchy GetHierarchy()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }
            if (System.IO.File.Exists(Path))
            {
                return ProvisioningHelper.LoadTenantTemplateFromFile(Path, (e) =>
                 {
                     WriteError(new ErrorRecord(e, "TEMPLATENOTVALID", ErrorCategory.SyntaxError, null));
                 });
            }
            else
            {
                throw new FileNotFoundException($"File {Path} does not exist.");
            }
        }
    }
}
