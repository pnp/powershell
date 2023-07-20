//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.IO;
//using System.Management.Automation;
//using System.Reflection;
//using System.Runtime.Loader;
//using System.Text;

//namespace PnP.PowerShell.Commands
//{
//    public class DependencyAssemblyLoadContext : AssemblyLoadContext
//    {
//        private static readonly string s_psHome = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

//        private static readonly ConcurrentDictionary<string, DependencyAssemblyLoadContext> s_dependencyLoadContexts = new ConcurrentDictionary<string, DependencyAssemblyLoadContext>();

//        internal static DependencyAssemblyLoadContext GetForDirectory(string directoryPath)
//        {
//            return s_dependencyLoadContexts.GetOrAdd(directoryPath, (path) => new DependencyAssemblyLoadContext(path));
//        }

//        private readonly string _dependencyDirPath;

//        public DependencyAssemblyLoadContext(string dependencyDirPath)
//            : base(nameof(DependencyAssemblyLoadContext))
//        {
//            _dependencyDirPath = dependencyDirPath;
//        }

//        protected override Assembly Load(AssemblyName assemblyName)
//        {
//            string assemblyFileName = $"{assemblyName.Name}.dll";

//            // Make sure we allow other common PowerShell dependencies to be loaded by PowerShell
//            // But specifically exclude Microsoft.ApplicationInsightssince we want to use a different version here
//            if (!assemblyName.Name.Equals("Microsoft.ApplicationInsights", StringComparison.OrdinalIgnoreCase))
//            {
//                string psHomeAsmPath = Path.Join(s_psHome, assemblyFileName);
//                if (File.Exists(psHomeAsmPath))
//                {
//                    // With this API, returning null means nothing is loaded
//                    return null;
//                }
//            }

//            // Now try to load the assembly from the dependency directory
//            string dependencyAsmPath = Path.Join(_dependencyDirPath, assemblyFileName);
//            if (File.Exists(dependencyAsmPath))
//            {
//                return LoadFromAssemblyPath(dependencyAsmPath);
//            }

//            return null;
//        }
//    }
//}
