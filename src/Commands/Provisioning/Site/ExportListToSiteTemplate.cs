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
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsData.Export, "PnPListToSiteTemplate")]
    public class ExportListToSiteTemplate : PnPWebCmdlet
    {
        //private ProgressRecord mainProgressRecord = new ProgressRecord(0, "Processing", "Status");
        private ProgressRecord subProgressRecord = new ProgressRecord(1, "Activity", "Status");

        [Parameter(Mandatory = true)]
        public List<string> List;

        [Parameter(Mandatory = false, Position = 0)]
        public string Out;

        [Parameter(Mandatory = false, Position = 1)]
        public XMLPnPSchemaVersion Schema = XMLPnPSchemaVersion.LATEST;
        
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter OutputInstance;

        protected override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(Out))
            {
                if (!Path.IsPathRooted(Out))
                {
                    Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
                }
                if (File.Exists(Out))
                {
                    if (Force || ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
                    {
                        ExtractTemplate(Schema, new FileInfo(Out).DirectoryName, new FileInfo(Out).Name);
                    }
                }
                else
                {
                    ExtractTemplate(Schema, new FileInfo(Out).DirectoryName, new FileInfo(Out).Name);
                }
            }
            else
            {
                ExtractTemplate(Schema, SessionState.Path.CurrentFileSystemLocation.Path, null);
            }
        }

        private void ExtractTemplate(XMLPnPSchemaVersion schema, string path, string packageName)
        {
            CurrentWeb.EnsureProperty(w => w.Url);

            var creationInformation = new ProvisioningTemplateCreationInformation(CurrentWeb)
            {
                HandlersToProcess = Handlers.Lists
            };

            var extension = "";
            if (packageName != null)
            {
                if (packageName.IndexOf(".", StringComparison.Ordinal) > -1)
                {
                    extension = packageName.Substring(packageName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                }
                else
                {
                    packageName += ".pnp";
                    extension = ".pnp";
                }
            }

            var fileSystemConnector = new FileSystemConnector(path, "");
            if (extension == ".pnp")
            {
                creationInformation.FileConnector = new OpenXMLConnector(packageName, fileSystemConnector);
            }
            else
            {
                creationInformation.FileConnector = fileSystemConnector;
            }

            creationInformation.ProgressDelegate = (message, step, total) =>
            {
                var percentage = Convert.ToInt32((100 / Convert.ToDouble(total)) * Convert.ToDouble(step));

                WriteProgress(new ProgressRecord(0, $"Extracting Template from {CurrentWeb.Url}", message) { PercentComplete = percentage });
            };
            creationInformation.MessagesDelegate = (message, type) =>
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

            if (List != null && List.Count > 0)
            {
                creationInformation.ListsToExtract.AddRange(List);
            }

            var template = CurrentWeb.GetProvisioningTemplate(creationInformation);

            
      
            if (!OutputInstance)
            {
                var formatter = ProvisioningHelper.GetFormatter(schema);

                if (extension == ".pnp")
                {
                    XMLTemplateProvider provider = new XMLOpenXMLTemplateProvider(
                          creationInformation.FileConnector as OpenXMLConnector);
                    var templateFileName = packageName.Substring(0, packageName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";

                    provider.SaveAs(template, templateFileName, formatter, null);
                }
                else
                {
                    if (Out != null)
                    {
                        XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(path, "");
                        provider.SaveAs(template, Path.Combine(path, packageName), formatter, null);
                    }
                    else
                    {
                        var outputStream = formatter.ToFormattedTemplate(template);
                        var reader = new StreamReader(outputStream);

                        WriteObject(reader.ReadToEnd());
                    }
                }
            }
            else
            {
                WriteObject(template);
            }
        }
    }
}
