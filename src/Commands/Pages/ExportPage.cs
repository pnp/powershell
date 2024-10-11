using PnP.Framework.Provisioning.Connectors;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Model.Configuration;
using PnP.Framework.Provisioning.ObjectHandlers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Properties;
using System;
using System.IO;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Provisioning.Tenant
{
    [Cmdlet(VerbsData.Export, "PnPPage")]    
    public class ExportPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public PagePipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter PersistBrandingFiles;

        [Parameter(Mandatory = false)]
        public string Out;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public ExtractConfigurationPipeBind Configuration;

        [Parameter(Mandatory = false)]
        public SwitchParameter OutputInstance;

        protected override void ProcessRecord()
        {
            _ = Identity.GetPage(Connection) ?? throw new Exception($"Page '{Identity?.Name}' does not exist");

            ExtractConfiguration extractConfiguration = null;
            if (ParameterSpecified(nameof(Configuration)))
            {
                extractConfiguration = Configuration.GetConfiguration(SessionState.Path.CurrentFileSystemLocation.Path);
            }

            if (!string.IsNullOrEmpty(Out))
            {
                if (!Path.IsPathRooted(Out))
                {
                    Out = Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
                }
                if (System.IO.File.Exists(Out))
                {
                    if (Force || ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
                    {
                        ExtractTemplate(new FileInfo(Out).DirectoryName, new FileInfo(Out).Name, extractConfiguration);
                    }
                }
                else
                {
                    ExtractTemplate(new FileInfo(Out).DirectoryName, new FileInfo(Out).Name, extractConfiguration);
                }
            }
            else
            {
                ExtractTemplate(null, null, extractConfiguration);
            }
        }


        private void ExtractTemplate(string dirName, string fileName, ExtractConfiguration configuration)
        {
            var outputTemplate = new ProvisioningTemplate();
            outputTemplate.Id = $"TEMPLATE-{Guid.NewGuid():N}".ToUpper();
            var helper = new PnP.Framework.Provisioning.ObjectHandlers.Utilities.ClientSidePageContentsHelper();
            ProvisioningTemplateCreationInformation ci = null;
            if (configuration != null)
            {
                ci = configuration.ToCreationInformation(CurrentWeb);
            }
            else
            {
                ci = new ProvisioningTemplateCreationInformation(CurrentWeb);
            }
            if (ParameterSpecified(nameof(PersistBrandingFiles)))
            {
                ci.PersistBrandingFiles = PersistBrandingFiles;
            }
            if (!string.IsNullOrEmpty(dirName))
            {
                var fileSystemConnector = new FileSystemConnector(dirName, "");
                ci.FileConnector = fileSystemConnector;
            }
            helper.ExtractClientSidePage(CurrentWeb, outputTemplate, ci, new PnP.Framework.Diagnostics.PnPMonitoredScope(), null, Identity.Name, false);

            if (!string.IsNullOrEmpty(fileName))
            {
                System.IO.File.WriteAllText(Path.Combine(dirName, fileName), outputTemplate.ToXML());
            }
            else
            {
                if (OutputInstance)
                {
                    WriteObject(outputTemplate);
                }
                else
                {
                    WriteObject(outputTemplate.ToXML());
                }                
            }
        }
    }
}