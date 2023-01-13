using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.ObjectHandlers;
using PnP.Framework.Provisioning.Providers.Xml;
using File = System.IO.File;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsData.Import, "PnPTermGroupFromXml")]
    public class ImportTermGroupFromXml : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = "XML")]
        public string Xml;

        [Parameter(Mandatory = false, ParameterSetName = "File")]
        public string Path;

        protected override void ExecuteCmdlet()
        {
            var template = new ProvisioningTemplate();

            template.Id = "TAXONOMYPROVISIONING";

            var outputStream = XMLPnPSchemaFormatter.LatestFormatter.ToFormattedTemplate(template);

            var reader = new StreamReader(outputStream);

            var fullXml = reader.ReadToEnd();

            var document = XDocument.Parse(fullXml);


            XElement preferencesElement = document.Root.Descendants(document.Root.GetNamespaceOfPrefix("pnp") + "Preferences").FirstOrDefault();
            if (preferencesElement != null && preferencesElement.PreviousNode != null)
            {
                preferencesElement.PreviousNode.AddBeforeSelf(preferencesElement);
                preferencesElement.Remove();
            }

            XElement termGroupsElement;
            if (ParameterSpecified(nameof(Xml)))
            {
                termGroupsElement = XElement.Parse(Xml);
            }
            else
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                termGroupsElement = XElement.Parse(File.ReadAllText(Path));
            }

            //XNamespace pnp = XMLConstants.PROVISIONING_SCHEMA_NAMESPACE_25_12;
            var templateElement = document.Root.Descendants(document.Root.GetNamespaceOfPrefix("pnp") + "ProvisioningTemplate").FirstOrDefault();

            templateElement?.Add(termGroupsElement);

            var stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;

            var completeTemplate = XMLPnPSchemaFormatter.LatestFormatter.ToProvisioningTemplate(stream);

            ProvisioningTemplateApplyingInformation templateAI = new ProvisioningTemplateApplyingInformation();
            templateAI.HandlersToProcess = Handlers.TermGroups;
            ClientContext.Web.ApplyProvisioningTemplate(completeTemplate, templateAI);

        }

    }
}