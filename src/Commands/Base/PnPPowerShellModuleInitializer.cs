using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Threading;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Bootstraps PnP PowerShell's dependency isolation.
    ///
    /// PnP.PowerShell.dll (this assembly, containing all cmdlets) is loaded by PowerShell into the
    /// default <see cref="AssemblyLoadContext"/> so its cmdlet types remain discoverable. Every other
    /// assembly we ship lives in the sibling "Common" folder and is loaded into a dedicated, fully
    /// isolated <see cref="PnPAssemblyLoadContext"/>. A single <see cref="AssemblyLoadContext.Resolving"/>
    /// handler on the default context routes any assembly we ship into that private context; from there the
    /// private context resolves the entire transitive graph internally, so our Microsoft.Extensions.* (and
    /// friends) can never bind against a version the host process already loaded.
    ///
    /// The handler MUST be registered before PowerShell reflects over this assembly to discover cmdlets,
    /// because the cmdlet base types statically reference PnP.Framework/PnP.Core and reflection therefore
    /// forces those dependencies to load. Neither <see cref="ModuleInitializerAttribute"/> nor
    /// <see cref="IModuleAssemblyInitializer.OnImport"/> run early enough (they fire only once code in this
    /// assembly executes, which is after discovery). The module manifest therefore uses <c>ScriptsToProcess</c>
    /// - which the module system runs before <c>NestedModules</c> - to load this assembly by path and call
    /// <see cref="EnsureDependencyResolverRegistered"/> up front. The module initializer and OnImport are kept
    /// as idempotent, defense-in-depth fallbacks. Registration is idempotent.
    /// </summary>
    public sealed class PnPPowerShellModuleInitializer : IModuleAssemblyInitializer
    {
        /// <summary>
        /// The private context that owns the shipped dependency graph. Created once, lives for the process
        /// lifetime (the module's binaries cannot meaningfully be unloaded from the default context anyway).
        /// </summary>
        private static readonly PnPAssemblyLoadContext s_dependencyContext;

        /// <summary>
        /// Absolute path to the folder holding the private dependency graph.
        /// </summary>
        private static readonly string s_dependencyPath;

        /// <summary>
        /// Guards against registering the resolver more than once (module initializer + OnImport + re-import).
        /// </summary>
        private static int s_resolverRegistered;

        /// <summary>
        /// Whether dependency isolation is active. Disabled for in-IDE (Visual Studio F5) debugging.
        /// </summary>
        private static readonly bool s_isolationEnabled;

        static PnPPowerShellModuleInitializer()
        {
            // This assembly (PnP.PowerShell.dll) ships in "<module>/Core"; the private dependency graph ships
            // in the sibling "<module>/Common" folder.
            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            s_dependencyPath = Path.GetFullPath(Path.Combine(executingDirectory, "..", "Common"));

            // In-IDE (Visual Studio F5) debugging imports the raw build output, where every dependency sits in
            // the same folder as this assembly. PowerShell's own directory probing already resolves that whole
            // graph into the default context, so engaging our private context on top of it only creates split
            // assembly identities (the same DLL loaded into two contexts - e.g. Microsoft.SharePoint.Client.Runtime -
            // which breaks cmdlets such as Get-PnPList). Isolation is unnecessary there anyway (there is no
            // conflicting host), so we disable it in that mode. Isolation applies to the packaged Core/Common
            // layout produced by the build scripts.
            s_isolationEnabled = Environment.GetEnvironmentVariable("PNP_PS_DEBUG_IN_VISUAL_STUDIO") != "True";

            s_dependencyContext = s_isolationEnabled ? new PnPAssemblyLoadContext(s_dependencyPath) : null;
        }

        /// <summary>
        /// Registers the default-context dependency resolver. This is the public entry point invoked by the
        /// module manifest's <c>ScriptsToProcess</c> script before the binary module is processed, so the
        /// resolver is active before cmdlet discovery forces PnP.Framework/PnP.Core to load. Also wired as a
        /// <see cref="ModuleInitializerAttribute"/> for defense in depth. Safe to call multiple times.
        /// </summary>
        [SuppressMessage("Usage", "CA2255:The ModuleInitializer attribute should not be used in libraries", Justification = "PowerShell loads this binary module into a host process; the resolver must be registered before cmdlet discovery loads private dependencies.")]
        [ModuleInitializer]
        public static void EnsureDependencyResolverRegistered()
        {
            if (Interlocked.Exchange(ref s_resolverRegistered, 1) == 0)
            {
                AssemblyLoadContext.Default.Resolving += ResolveDependency;
            }
        }

        /// <summary>
        /// Defensive fallback registration for hosts that defer <see cref="ModuleInitializerAttribute"/> methods.
        /// </summary>
        public void OnImport()
        {
            EnsureDependencyResolverRegistered();
        }

        /// <summary>
        /// Default-context resolver. When the default context cannot satisfy an assembly, we check whether we
        /// ship it. If so, we hand it to the private context; otherwise we return <c>null</c> and let the runtime
        /// continue its normal resolution (shared framework, PowerShell, host).
        /// </summary>
        private static Assembly ResolveDependency(AssemblyLoadContext defaultContext, AssemblyName assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName?.Name))
            {
                return null;
            }

            string candidate = Path.Combine(s_dependencyPath, assemblyName.Name + ".dll");
            if (!File.Exists(candidate))
            {
                // Not ours - let the default resolution logic (shared framework / PowerShell / host) handle it.
                return null;
            }

            // Route the assembly into the private context. Because that context overrides Load() to probe the
            // same folder, this assembly and its entire transitive dependency graph resolve to our shipped
            // copies, isolated from whatever the host already loaded into the default context.
            return s_dependencyContext.LoadFromAssemblyName(assemblyName);
        }
    }
}
