using System;
using System.IO;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Provisioning.Model;
using PnP.Framework.Provisioning.Providers.Xml;
using PnP.Framework.Provisioning.Connectors;
using System.Collections;
using PnP.Framework.Provisioning.Providers;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Provisioning.Site
{
    [Cmdlet(VerbsCommon.Set, "PnPSiteTemplateMetadata")]
    public class SetSiteTemplateMetadata : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string Path;

        [Parameter(Mandatory = false)]
        public string TemplateDisplayName;

        [Parameter(Mandatory = false)]
        public string TemplateImagePreviewUrl;

        [Parameter(Mandatory = false)]
        public Hashtable TemplateProperties;

        [Parameter(Mandatory = false)]
        public ITemplateProviderExtension[] TemplateProviderExtensions;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.Url);
            bool templateFromFileSystem = !Path.ToLower().StartsWith("http");
            FileConnectorBase fileConnector;
            string templateFileName = System.IO.Path.GetFileName(Path);
            if (templateFromFileSystem)
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                FileInfo fileInfo = new FileInfo(Path);
                fileConnector = new FileSystemConnector(fileInfo.DirectoryName, "");
            }
            else
            {                
                Uri fileUri = new Uri(Path);
                var webUrl = Microsoft.SharePoint.Client.Web.WebUrlFromFolderUrlDirect(ClientContext, fileUri);
                var templateContext = ClientContext.Clone(webUrl.ToString());

                string library = Path.ToLower().Replace(templateContext.Url.ToLower(), "").TrimStart('/');
                int idx = library.IndexOf("/");
                library = library.Substring(0, idx);
                fileConnector = new SharePointConnector(templateContext, templateContext.Url, library);
            }
            XMLTemplateProvider provider;
            ProvisioningTemplate provisioningTemplate;
            Stream stream = fileConnector.GetFileStream(templateFileName);
            var isOpenOfficeFile = FileUtilities.IsOpenOfficeFile(stream);
            if (isOpenOfficeFile)
            {
                var openXmlConnector = new OpenXMLConnector(templateFileName, fileConnector);
                provider = new XMLOpenXMLTemplateProvider(openXmlConnector);
                if (!String.IsNullOrEmpty(openXmlConnector.Info?.Properties?.TemplateFileName))
                {
                    templateFileName = openXmlConnector.Info.Properties.TemplateFileName;
                }
                else
                {
                    templateFileName = templateFileName.Substring(0, templateFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".xml";
                }
            }
            else
            {
                if (templateFromFileSystem)
                {
                    provider = new XMLFileSystemTemplateProvider(fileConnector.Parameters[FileConnectorBase.CONNECTIONSTRING] + "", "");
                }
                else
                {
                    throw new NotSupportedException("Only .pnp package files are supported from a SharePoint library");
                }
            }
            provisioningTemplate = provider.GetTemplate(templateFileName, TemplateProviderExtensions);

            if (provisioningTemplate == null) return;

            GetSiteTemplate.SetTemplateMetadata(provisioningTemplate, TemplateDisplayName, TemplateImagePreviewUrl, TemplateProperties);

            provider.SaveAs(provisioningTemplate, templateFileName, TemplateProviderExtensions);
        }
    }
}
