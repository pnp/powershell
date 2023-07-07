using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.ObjectHandlers;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.PowerShell.Commands.Base.PipeBinds;
using File = System.IO.File;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Taxonomy
{
    [Cmdlet(VerbsData.Export, "PnPTermGroupToXml")]
    public class ExportTermGroup : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false,
            ValueFromPipeline = true)]
        public TermGroupPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string Out;

        [Parameter(Mandatory = false)]
        public SwitchParameter FullTemplate;

        [Parameter(Mandatory = false)]
        public Encoding Encoding = Encoding.Unicode;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            // var template = new ProvisioningTemplate();

            var templateCi = new ProvisioningTemplateCreationInformation(ClientContext.Web) { IncludeAllTermGroups = true };

            templateCi.HandlersToProcess = Handlers.TermGroups;

            var template = ClientContext.Web.GetProvisioningTemplate(templateCi);

            if (ParameterSpecified(nameof(Identity)))
            {
                if (Identity.Id != Guid.Empty)
                {
                    // Find the site collection term group name
                    var tg = template.TermGroups?.FirstOrDefault(g => g.Name == "{sitecollectiontermgroupname}");
                    if (tg != null)
                    {
                        var tokenParser = new TokenParser(ClientContext.Web, template);
                        // parse the group name
                        var siteCollectionTermGroupName = tokenParser.ParseString(tg.Name);
                        var taxonomySession = TaxonomySession.GetTaxonomySession(ClientContext);
                        var termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
                        var group = termStore.Groups.GetByName(siteCollectionTermGroupName);
                        group.EnsureProperties(g => g.Id, g => g.Name);

                        // if group found and it's ID equals the one that we need, set the ID value so we can remove others
                        if (group != null && group.Id == Identity.Id)
                        {
                            tg.Id = group.Id;
                        }
                    }
                    template?.TermGroups?.RemoveAll(t => t.Id != Identity.Id);
                    if (template?.TermGroups?.Count == 1)
                    {
                        template.TermGroups[0].Id = Guid.Empty;
                    }
                }
                else if (Identity.Name != string.Empty)
                {
                    var tg = template.TermGroups?.FirstOrDefault(g => g.Name == "{sitecollectiontermgroupname}");
                    if (tg != null)
                    {
                        var tokenParser = new TokenParser(ClientContext.Web, template);
                        var siteCollectionTermGroupName = tokenParser.ParseString(tg.Name);
                        if (!string.IsNullOrEmpty(siteCollectionTermGroupName) && Identity.Name == siteCollectionTermGroupName)
                        {
                            tg.Name = siteCollectionTermGroupName;
                        }
                    }
                    template?.TermGroups?.RemoveAll(t => t.Name != Identity.Name);
                }
            }
            var outputStream = XMLPnPSchemaFormatter.LatestFormatter.ToFormattedTemplate(template);

            var reader = new StreamReader(outputStream);

            var fullxml = reader.ReadToEnd();

            var xml = string.Empty;

            if (!FullTemplate)
            {
                var document = XDocument.Parse(fullxml);

                XNamespace pnp = document.Root.GetNamespaceOfPrefix("pnp");

                var termGroupsElement = document.Root.Descendants(pnp + "TermGroups").FirstOrDefault();

                if (termGroupsElement != null)
                {
                    xml = termGroupsElement.ToString();
                }
            }
            else
            {
                xml = fullxml;
            }

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
                        File.WriteAllText(Out, xml, Encoding);
                    }
                }
                else
                {
                    File.WriteAllText(Out, xml, Encoding);
                }
            }
            else
            {
                WriteObject(xml);
            }
        }
    }
}
