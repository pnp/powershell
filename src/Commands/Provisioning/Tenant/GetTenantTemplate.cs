using System;
using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.ObjectHandlers;
using PnP.Framework.Provisioning.Providers.Xml;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.Framework.Provisioning.Model.Configuration;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsCommon.Get, "PnPTenantTemplate")]
    public class GetTenantTemplate : PnPSharePointOnlineAdminCmdlet
    {
        const string PARAMETERSET_ASFILE = "Extract a template to a file";
        const string PARAMETERSET_ASOBJECT = "Extract a template as an object";

        private ProgressRecord subProgressRecord = new ProgressRecord(1, "Activity", "Status");

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ASFILE)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ASOBJECT)]
        public string SiteUrl;

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PARAMETERSET_ASFILE)]
        public string Out;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ASFILE)]
        public SwitchParameter Force;

        [Parameter(Mandatory = true, ParameterSetName = PARAMETERSET_ASOBJECT)]
        public SwitchParameter AsInstance;

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ASFILE)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_ASOBJECT)]
        public ExtractConfigurationPipeBind Configuration;

        protected override void ExecuteCmdlet()
        {

            ExtractConfiguration extractConfiguration;

            if (ParameterSpecified(nameof(Configuration)))
            {
                extractConfiguration = Configuration.GetConfiguration(SessionState.Path.CurrentFileSystemLocation.Path);
            }
            else
            {
                extractConfiguration = new ExtractConfiguration();
            }

            if (string.IsNullOrEmpty(SiteUrl))
            {
                SiteUrl = Connection.Url;
            }

            if (extractConfiguration.Tenant.Sequence == null)
            {
                extractConfiguration.Tenant.Sequence = new Framework.Provisioning.Model.Configuration.Tenant.Sequence.ExtractSequenceConfiguration();
            }
            extractConfiguration.Tenant.Sequence.SiteUrls.Add(SiteUrl);

            if (ParameterSetName == PARAMETERSET_ASFILE)
            {
                ProvisioningHierarchy tenantTemplate = null;

                if (!Path.IsPathRooted(Out))
                {
                    Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
                }
                if (Out.ToLower().EndsWith(".pnp"))
                {
                    LogWarning("This cmdlet does not save a tenant template as a PnP file.");
                }
                var fileInfo = new FileInfo(Out);
                var fileSystemConnector = new FileSystemConnector(fileInfo.DirectoryName, "");

                extractConfiguration.FileConnector = fileSystemConnector;

                var proceed = false;
                if (File.Exists(Out))
                {
                    if (Force || ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
                    {
                        proceed = true;
                    }
                }
                else
                {
                    proceed = true;
                }

                if (proceed)
                {
                    tenantTemplate = ExtractTemplate(extractConfiguration);

                    XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(fileInfo.DirectoryName, "");
                    provider.SaveAs(tenantTemplate, Out);
                }
            }
            else
            {
                WriteObject(ExtractTemplate(extractConfiguration));
            }
        }

        private ProvisioningHierarchy ExtractTemplate(ExtractConfiguration configuration)
        {
            configuration.ProgressDelegate = (message, step, total) =>
            {
                var percentage = Convert.ToInt32((100 / Convert.ToDouble(total)) * Convert.ToDouble(step));

                WriteProgress(new ProgressRecord(0, $"Extracting Tenant Template", message) { PercentComplete = percentage });
                WriteProgress(new ProgressRecord(1, " ", " ") { RecordType = ProgressRecordType.Completed });
            };
            configuration.MessagesDelegate = (message, type) =>
            {
                switch (type)
                {
                    case ProvisioningMessageType.Warning:
                        {
                            LogWarning(message);
                            break;
                        }
                    case ProvisioningMessageType.Progress:
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
                                    subProgressRecord.Activity = messageSplitted[0];
                                    subProgressRecord.StatusDescription = messageSplitted[1];
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
                            break;
                        }
                    case ProvisioningMessageType.Completed:
                        {
                            WriteProgress(new ProgressRecord(1, message, " ") { RecordType = ProgressRecordType.Completed });
                            break;
                        }
                }
            };
            using (var provisioningContext = new PnPProvisioningContext(async (resource, scope) =>
            {
                return await TokenRetrieval.GetAccessTokenAsync(resource, scope, Connection);
            }, azureEnvironment: Connection.AzureEnvironment))
            {
                return Tenant.GetTenantTemplate(configuration);
            }
        }
    }
}