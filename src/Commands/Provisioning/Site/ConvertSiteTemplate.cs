using System.IO;
using System.Management.Automation;
using PnP.Framework.Provisioning.Providers.Xml;
using Resources = PnP.PowerShell.Commands.Properties.Resources;
using PnP.Framework.Provisioning.Providers;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.Provisioning
{
    [Cmdlet(VerbsData.Convert, "PnPSiteTemplate")]
    public class ConvertSiteTemplate : BasePSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string Path;

        [Parameter(Mandatory = false)]
        public string Out;

        [Parameter(Mandatory = false, Position = 1)]
        public XMLPnPSchemaVersion ToSchema = XMLPnPSchemaVersion.LATEST;

        [Parameter(Mandatory = false)]
        public System.Text.Encoding Encoding = System.Text.Encoding.Unicode;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void BeginProcessing()
        {
            if (!System.IO.Path.IsPathRooted(Path))
            {
                Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
            }

            if (ParameterSpecified(nameof(Out)))
            {
                if (!System.IO.Path.IsPathRooted(Out))
                {
                    Out = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Out);
                }
            }

            FileInfo fileInfo = new FileInfo(Path);

            XMLTemplateProvider provider =
            new XMLFileSystemTemplateProvider(fileInfo.DirectoryName, "");

            var provisioningTemplate = provider.GetTemplate(fileInfo.Name);

            if (provisioningTemplate != null)
            {
                ITemplateFormatter formatter = null;
                switch (ToSchema)
                {
                    case XMLPnPSchemaVersion.LATEST:
                        {
                            formatter = XMLPnPSchemaFormatter.LatestFormatter;
                            break;
                        }
                    case XMLPnPSchemaVersion.V201909:
                        {
                            formatter = XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2019_09);
                            break;
                        }
                    case XMLPnPSchemaVersion.V202002:
                        {
                            formatter = XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2020_02);
                            break;
                        }
                    case XMLPnPSchemaVersion.V202103:
                        {
                            formatter = XMLPnPSchemaFormatter.GetSpecificFormatter(XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_2021_03);
                            break;
                        }
                }

                if (!string.IsNullOrEmpty(Out))
                {
                    if (File.Exists(Out))
                    {
                        if (Force || ShouldContinue(string.Format(Resources.File0ExistsOverwrite, Out), Resources.Confirm))
                        {
                            File.WriteAllText(Out, provisioningTemplate.ToXML(formatter), Encoding);
                        }
                    }
                    else
                    {
                        File.WriteAllText(Out, provisioningTemplate.ToXML(formatter), Encoding);
                    }
                }
                else
                {
                    WriteObject(provisioningTemplate.ToXML(formatter));
                }
            }
        }
    }
}
