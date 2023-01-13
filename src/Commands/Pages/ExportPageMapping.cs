using System;
using System.Management.Automation;
using System.Reflection;
using Microsoft.SharePoint.Client;
using System.IO;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.Framework.Modernization.Transform;
using PnP.Framework.Modernization.Publishing;
using PnP.Framework.Modernization.Telemetry.Observers;
using PnP.PowerShell.ALC;
using PnP.Framework.Modernization.Cache;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsData.Export, "PnPPageMapping")]
    [Alias("Export-PnPClientSidePageMapping")]
    [WriteAliasWarning("Please use 'Export-PnPPageMapping'. The alias 'Export-PnPClientSidePageMapping' will be removed in the 1.5.0 release")]

    public class ExportPageMapping : PnPWebCmdlet
    {
        private Assembly sitesCoreAssembly;

        [Parameter(Mandatory = false)]
        public SwitchParameter BuiltInWebPartMapping = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter BuiltInPageLayoutMapping = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter CustomPageLayoutMapping = false;

        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public ClassicPagePipeBind PublishingPage;

        [Parameter(Mandatory = false)]
        public SwitchParameter AnalyzeOOBPageLayouts = false;

        [Parameter(Mandatory = false)]
        public string Folder;

        [Parameter(Mandatory = false)]
        public SwitchParameter Overwrite = false;

        [Parameter(Mandatory = false)]
        public SwitchParameter Logging = false;


        protected override void ExecuteCmdlet()
        {
            //Fix loading of modernization framework
            FixLocalAssemblyResolving();

            // Configure folder to export
            string folderToExportTo = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(this.Folder))
            {
                if (!Directory.Exists(this.Folder))
                {
                    throw new Exception($"Folder '{this.Folder}' does not exist");
                }

                folderToExportTo = this.Folder;
            }

            // Export built in web part mapping
            if (this.BuiltInWebPartMapping)
            {
                string fileName = Path.Combine(folderToExportTo, "webpartmapping.xml");

                if (System.IO.File.Exists(fileName) && !Overwrite)
                {
                    Console.WriteLine($"Skipping the export from the built-in webpart mapping file {fileName} as this already exists. Use the -Overwrite flag to overwrite if needed.");
                }
                else
                {
                    // Load the default one from resources into a model, no need for persisting this file
                    string webpartMappingFileContents = PageTransformator.LoadDefaultWebPartMappingFile();
                    System.IO.File.WriteAllText(fileName, webpartMappingFileContents);
                }
            }

            // Export built in page layout mapping
            if (this.BuiltInPageLayoutMapping)
            {
                string fileName = Path.Combine(folderToExportTo, "pagelayoutmapping.xml");

                if (System.IO.File.Exists(fileName) && !Overwrite)
                {
                    Console.WriteLine($"Skipping the export from the built-in pagelayout mapping file {fileName} as this already exists. Use the -Overwrite flag to overwrite if needed.");
                }
                else
                {
                    // Load the default one from resources into a model, no need for persisting this file
                    string pageLayoutMappingFileContents = PublishingPageTransformator.LoadDefaultPageLayoutMappingFile();
                    System.IO.File.WriteAllText(fileName, pageLayoutMappingFileContents);
                }
            }

            // Export custom page layout mapping
            if (this.CustomPageLayoutMapping)
            {
                if (!this.ClientContext.Web.IsPublishingWeb())
                {
                    throw new Exception("The -CustomPageLayoutMapping parameter only works for publishing sites.");
                }

                ListItem page = null;

                if (PublishingPage != null)
                {
                    page = PublishingPage.GetPage(this.ClientContext.Web, CacheManager.Instance.GetPublishingPagesLibraryName(this.ClientContext));
                }

                Guid siteId = this.ClientContext.Site.EnsureProperty(p => p.Id);

                string fileName = $"custompagelayoutmapping-{siteId.ToString()}.xml";

                if (page != null)
                {
                    fileName = $"custompagelayoutmapping-{siteId.ToString()}-{page.FieldValues["FileLeafRef"].ToString().ToLower().Replace(".aspx", "")}.xml";
                }

                if (System.IO.File.Exists(Path.Combine(folderToExportTo, fileName)) && !Overwrite)
                {
                    Console.WriteLine($"Skipping the export from the custom pagelayout mapping file {Path.Combine(folderToExportTo, fileName)} as this already exists. Use the -Overwrite flag to overwrite if needed.");
                }
                else
                {
                    var analyzer = new PageLayoutAnalyser(this.ClientContext);

                    if (Logging)
                    {
                        analyzer.RegisterObserver(new ConsoleObserver(false));
                    }

                    if (page != null)
                    {
                        analyzer.AnalysePageLayoutFromPublishingPage(page);
                    }
                    else
                    {
                        analyzer.AnalyseAll(!this.AnalyzeOOBPageLayouts);
                    }

                    analyzer.GenerateMappingFile(folderToExportTo, fileName);
                }
            }
        }

        private string AssemblyDirectory
        {
            get
            {
                string location = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(location);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private void FixLocalAssemblyResolving()
        {
            try
            {
                sitesCoreAssembly = Assembly.LoadFrom(Path.Combine(AssemblyDirectory, "PnP.Framework.dll"));
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_LocalAssemblyResolve;
            }
            catch { }
        }

        private Assembly CurrentDomain_LocalAssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("PnP.Framework"))
            {
                return sitesCoreAssembly;
            }
            return null;
        }

    }
}