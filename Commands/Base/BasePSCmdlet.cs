using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Automation;
using System.Reflection;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Cmdlets
    /// </summary>
    public class BasePSCmdlet : PSCmdlet
    {
        private static bool assembliesResolved = false;

        private static Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            //if (!assembliesResolved)
            //{
            //    FixAssemblyResolving();

            //    assembliesResolved = true;
            //}
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            //AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
        }

        private void FixAssemblyResolving()
        {
            FixAssemblyLoading("Newtonsoft.Json.dll");
            FixAssemblyLoading("Microsoft.Extensions.Options.dll");
            FixAssemblyLoading("Microsoft.ApplicationInsights.dll");
            FixAssemblyLoading("System.Buffers.dll");
            FixAssemblyLoading("System.Runtime.CompilerServices.Unsafe.dll");
            FixAssemblyLoading("System.Threading.Tasks.Extensions.dll");
            FixAssemblyLoading("System.Numerics.Vectors.dll");
        }

        private void FixAssemblyLoading(string assemblyName)
        {
            if (!assemblies.ContainsKey(assemblyName))
            {
                Assembly assembly = null;
                var assemblyPath = Path.Combine(AssemblyDirectoryFromLocation, assemblyName);
                if (File.Exists(assemblyPath))
                {
                    try
                    {
                        assembly = Assembly.LoadFrom(assemblyPath);
                    }
                    catch { }
                }
                else
                {
                    var codebasePath = Path.Combine(AssemblyDirectoryFromCodeBase, assemblyName);
                    try
                    {
                        assembly = Assembly.LoadFrom(codebasePath);
                    }
                    catch { }
                }
                if (assembly != null) { assemblies.Add(assemblyName, assembly); }
            }
        }

        private string AssemblyDirectoryFromLocation
        {
            get
            {
                var location = Assembly.GetExecutingAssembly().Location;
                var escapedLocation = Uri.UnescapeDataString(location);
                return Path.GetDirectoryName(escapedLocation);
            }
        }

        private string AssemblyDirectoryFromCodeBase
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyKP = assemblies.FirstOrDefault(a => a.Key.Equals($"{args.Name.Substring(0, args.Name.IndexOf(","))}.dll"));
            if (assemblyKP.Value != null)
            {
                return assemblyKP.Value;
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName == args.Name)
                {
                    return assembly;
                }
            }
            return null;
        }

        /// <summary>
        /// Checks if a parameter with the provided name has been provided in the execution command
        /// </summary>
        /// <param name="parameterName">Name of the parameter to validate if it has been provided in the execution command</param>
        /// <returns>True if a parameter with the provided name is present, false if it is not</returns>
        public bool ParameterSpecified(string parameterName)
        {
            return MyInvocation.BoundParameters.ContainsKey(parameterName);
        }

        protected virtual void ExecuteCmdlet()
        { }

        protected override void ProcessRecord()
        {
            ExecuteCmdlet();
        }
    }
}
