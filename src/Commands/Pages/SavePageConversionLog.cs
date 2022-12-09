using PnP.Framework.Modernization.Cache;
using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.Pages
{
    [Cmdlet(VerbsData.Save, "PnPPageConversionLog")]

    public class SavePageConversionLog : PnPWebCmdlet
    {
        private Assembly sitesCoreAssembly;
        //private Assembly newtonsoftAssembly;

        protected override void ExecuteCmdlet()
        {
            //Fix loading of modernization framework
            FixLocalAssemblyResolving();

            // Get last used transformator instance from cache
            var transformator = CacheManager.Instance.GetLastUsedTransformator();

            if (transformator != null)
            {
                transformator.FlushObservers();
            }
        }

        private string AssemblyDirectory
        {
            get
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
#if NETFRAMEWORK
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
#else
                string codeBase = executingAssembly.Location;
#endif
                UriBuilder uri = new UriBuilder(codeBase);
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