using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.Loader;

namespace PnP.PowerShell.Commands.Base
{
    public class PnPPowerShellModuleInitializer : IModuleAssemblyInitializer
    {
        private static readonly string s_binBasePath;
        private static readonly string s_binCommonPath;
        private static readonly HashSet<string> s_dependencies;
        private static readonly HashSet<string> s_psEditionDependencies;
        private static readonly AssemblyLoadContext s_proxy;

        static PnPPowerShellModuleInitializer()
        {
            s_binBasePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            s_binCommonPath = Path.Combine(Path.GetDirectoryName(s_binBasePath), "Common");
            if (Environment.GetEnvironmentVariable("PNP_PS_DEBUG_IN_VISUAL_STUDIO") == "True")
            {
                s_binCommonPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", "..", "..", "src", "ALC", "bin", "Debug", "net8.0"));
            }

            s_dependencies = new HashSet<string>(StringComparer.Ordinal);
            s_psEditionDependencies = new HashSet<string>(StringComparer.Ordinal);
            s_proxy = new AssemblyLoadContext("pnp-powershell-load-context");

            // Add shared dependencies.
            foreach (string filePath in Directory.EnumerateFiles(s_binBasePath, "*.dll"))
            {
                try
                {
                    s_dependencies.Add(AssemblyName.GetAssemblyName(filePath).FullName);
                }
                catch (BadImageFormatException)
                {
                    // Skip files without metadata.
                    continue;
                }
            }

            // Add the dependencies for the current PowerShell edition. Can be either Desktop (PS 5.1) or Core (PS 7+).
            foreach (string filePath in Directory.EnumerateFiles(s_binCommonPath, "*.dll"))
            {
                try
                {
                    s_psEditionDependencies.Add(AssemblyName.GetAssemblyName(filePath).FullName);
                }
                catch (BadImageFormatException)
                {
                    // Skip files without metadata.
                    continue;
                }
            }
        }

        public void OnImport()
        {
            AssemblyLoadContext.Default.Resolving += ResolveAssembly_NetCore;
        }


        private static Assembly ResolveAssembly_NetCore(
            AssemblyLoadContext assemblyLoadContext,
            AssemblyName assemblyName)
        {
            if (assemblyName.Name.Equals("PnP.PowerShell.ALC"))
            {
                string filePath = GetRequiredAssemblyPath(assemblyName);
                if (!string.IsNullOrEmpty(filePath))
                {
                    // - In .NET, load the assembly into the custom assembly load context.                    
                    return s_proxy.LoadFromAssemblyPath(filePath);
                }
            }

            if (IsAssemblyMatching(assemblyName))
            {
                string filePath = GetRequiredAssemblyPath(assemblyName);
                if (!string.IsNullOrEmpty(filePath))
                {
                    // - In .NET, load the assembly into the custom assembly load context.                    
                    return s_proxy.LoadFromAssemblyPath(filePath);
                }
            }
            return null;
        }

        /// <summary>
        /// Checks to see if the assembly is present in the shared or PSEdition dependencies folder.
        /// Check is done by first matching the assembly by its full name; otherwise, we match using the assembly name.
        /// </summary>
        /// <param name="assemblyName"><see cref="AssemblyName"/> to match.</param>
        /// <returns>True if assembly is present in dependencies folder; otherwise False.</returns>
        private static bool IsAssemblyPresent(AssemblyName assemblyName)
        {
            return s_binBasePath.Contains(assemblyName.FullName) || s_binCommonPath.Contains(assemblyName.FullName)
                ? true
                : !string.IsNullOrEmpty(s_dependencies.SingleOrDefault((x) => x.StartsWith($"{assemblyName.Name},"))) || !string.IsNullOrEmpty(s_psEditionDependencies.SingleOrDefault((x) => x.StartsWith($"{assemblyName.Name},")));
        }

        /// <summary>
        /// Checks to see if the requested assembly matches the assemblies in our dependencies folder.
        /// The requesting assembly is always available in .NET, but could be null in .NET Framework.
        /// - When the requesting assembly is available, we check whether the loading request came from this
        ///   module (the 'Microsoft.Graph*' assembly in this case), so as to make sure we only act on the request
        ///   from this module.
        /// - When the requesting assembly is not available, we just have to depend on the assembly name only.
        /// </summary>
        /// <param name="assemblyName"><see cref="AssemblyName"/> being requested.</param>
        /// <param name="requestingAssembly">The requesting <see cref="Assembly"/>.</param>
        /// <returns>True if assembly is present and matches in dependencies folder; otherwise False.</returns>
        private static bool IsAssemblyMatching(AssemblyName assemblyName)
        {
            return assemblyName != null
                ? (assemblyName.FullName.StartsWith("Microsoft") || assemblyName.FullName.StartsWith("Azure.Identity")) && IsAssemblyPresent(assemblyName)
                : IsAssemblyPresent(assemblyName);
        }

        /// <summary>
        /// Gets the full path of the assembly from the dependencies folder.
        /// </summary>
        /// <param name="assemblyName"><see cref="AssemblyName"/> to find.</param>
        /// <returns>A <see cref="string"/> representing the full path of the assembly from the dependencies folder; otherwise <see cref="null"/>.</returns>
        private static string GetRequiredAssemblyPath(AssemblyName assemblyName)
        {
            string fileName = assemblyName.Name + ".dll";
            string filePath = Path.Combine(s_binBasePath, fileName);
            if (File.Exists(filePath))
                return filePath;

            filePath = Path.Combine(s_binCommonPath, fileName);
            return File.Exists(filePath) ? filePath : null;
        }
    }
}
