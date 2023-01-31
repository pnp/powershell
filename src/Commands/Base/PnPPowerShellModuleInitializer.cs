using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PnP.PowerShell.Commands.Base
{
    public class PnPPowerShellModuleInitializer : IModuleAssemblyInitializer
    {
        private static string s_binBasePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ".."));

        private static string s_binCommonPath = Path.Combine(s_binBasePath, "Common");

        public void OnImport()
        {
            AssemblyLoadContext.Default.Resolving += ResolveAssembly_NetCore;
        }


        private static Assembly ResolveAssembly_NetCore(
            AssemblyLoadContext assemblyLoadContext,
            AssemblyName assemblyName)
        {
            // In .NET Core, PowerShell deals with assembly probing so our logic is much simpler
            // We only care about our Engine assembly
            if (!assemblyName.Name.Equals("PnP.PowerShell.ALC"))
            {
                return null;
            }

            // Now load the Engine assembly through the dependency ALC, and let it resolve further dependencies automatically
            return DependencyAssemblyLoadContext.GetForDirectory(s_binCommonPath).LoadFromAssemblyName(assemblyName);
        }
    }
}
