using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// A dedicated <see cref="AssemblyLoadContext"/> that owns the private dependency graph shipped with
    /// PnP PowerShell in the module's "Common" folder (PnP.Framework, PnP.Core, PnP.Core.Auth, PnP.Core.Admin,
    /// Microsoft.Extensions.*, Microsoft.Identity.*, Newtonsoft.Json, and so on).
    ///
    /// The CSOM libraries are the exception: they ship in the "Core" folder next to PnP.PowerShell.dll and live
    /// in the default context, so that PowerShell's [TypeName] resolver can find their types in user scripts. When
    /// an assembly in this context needs one of them, <see cref="ResolveFromModuleAssemblyFolder"/> hands it the
    /// copy in the default context. See that method for why this cannot be left to the runtime.
    ///
    /// The purpose of this context is <b>full isolation</b>. By overriding <see cref="Load"/> to probe
    /// our own dependency folder first, every assembly loaded here - and, crucially, every transitive
    /// dependency of those assemblies - resolves to the exact version we shipped. This holds regardless
    /// of what the host process has already loaded into <see cref="AssemblyLoadContext.Default"/>.
    ///
    /// This is what fixes the Azure Functions "Method not found ... OptionsServiceCollectionExtensions.AddOptions"
    /// class of failures (issue #5350, and previously #2136): the Azure Functions host pre-loads its own
    /// Microsoft.Extensions.* assemblies into the default context, and the old resolver - which only reacted
    /// to the default context's <c>Resolving</c> fallback - could never override an assembly the host had
    /// already loaded. Here, PnP.Framework/PnP.Core are handed to this context and their Microsoft.Extensions.*
    /// dependencies are resolved <i>by this context</i>, never against the host's copies.
    ///
    /// Assemblies we do NOT ship (the shared framework, PowerShell, System.Text.Json, System.Management.Automation,
    /// etc.) deliberately return <c>null</c> from <see cref="Load"/> so they fall back to the default context and
    /// remain a single, shared identity across the ALC boundary. Keep that rule in mind: any type that crosses
    /// the boundary between PnP.PowerShell.dll (default context) and this context must come from an assembly that
    /// is shared (framework) or that resolves to a single copy here - never a type from an assembly that exists in
    /// both contexts.
    /// </summary>
    internal sealed class PnPAssemblyLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Simple name of MSAL, the root of the Microsoft.Identity.Client assembly family.
        /// </summary>
        private const string IdentityClientAssemblyName = "Microsoft.Identity.Client";

        /// <summary>
        /// Absolute path to the folder that holds the private dependency graph (the module's "Common" folder).
        /// </summary>
        private readonly string _dependencyPath;

        /// <summary>
        /// Absolute path to the folder that holds PnP.PowerShell.dll and the CSOM libraries (the module's "Core"
        /// folder), whose assemblies are loaded into the default context.
        /// </summary>
        private readonly string _moduleAssemblyPath;

        /// <summary>
        /// Candidate runtime identifiers used to locate native (unmanaged) libraries, most specific first.
        /// </summary>
        private readonly string[] _nativeRuntimeIdentifiers;

        /// <summary>
        /// Versions of the assemblies in the dependency folder, keyed by simple name; <c>null</c> for assemblies we do
        /// not ship.
        /// </summary>
        private readonly ConcurrentDictionary<string, Version> _shippedVersions = new(StringComparer.OrdinalIgnoreCase);

        public PnPAssemblyLoadContext(string dependencyPath, string moduleAssemblyPath)
            : base(name: "PnP.PowerShell", isCollectible: false)
        {
            _dependencyPath = dependencyPath ?? throw new ArgumentNullException(nameof(dependencyPath));
            _moduleAssemblyPath = moduleAssemblyPath ?? throw new ArgumentNullException(nameof(moduleAssemblyPath));
            _nativeRuntimeIdentifiers = BuildNativeRuntimeIdentifiers();
            Resolving += ResolveFromModuleAssemblyFolder;
        }

        /// <summary>
        /// Gets the folder this context resolves managed and native dependencies from.
        /// </summary>
        public string DependencyPath => _dependencyPath;

        /// <summary>
        /// Resolves a managed assembly. Only assemblies physically present in the private dependency folder are
        /// owned by this context; anything else returns <c>null</c> so the runtime falls back to the default
        /// context (and stays shared with the host / PowerShell / shared framework).
        /// </summary>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName?.Name))
            {
                return null;
            }

            // Boundary assemblies are those whose types cross between the default context (where PnP.PowerShell.dll
            // and its cmdlets live) and this private context - so they must resolve to a single identity on both
            // sides. If the host process already loaded one into the default context at a version that satisfies our
            // references, reuse that copy (return null to defer to the default context) instead of loading our own,
            // which would create two identities. The concrete failure this prevents is MSAL's
            // MsalCacheHelper.RegisterCache throwing across contexts when a host (e.g. an Az module) preloaded
            // Microsoft.Identity.Client. An older host copy cannot satisfy our references, for which we load our own
            // copy and ResolveDependency routes PnP.PowerShell.dll's reference from the default context to it as well.
            // Assemblies NOT on this list - above all Microsoft.Extensions.* - are always isolated, so a mismatched
            // host version cannot break us (that is the whole purpose of this context and the fix for issue #5350).
            if (DefersToDefaultContext(assemblyName.Name))
            {
                return null;
            }

            string candidate = Path.Combine(_dependencyPath, assemblyName.Name + ".dll");

            // Note: we intentionally load by simple name and ignore the requested version. When an assembly is
            // returned from a custom ALC's Load override the runtime does not enforce the requested version, so
            // PnP.Framework/PnP.Core always bind to the version we shipped even if the host asked for another.
            return File.Exists(candidate) ? LoadFromAssemblyPath(candidate) : null;
        }

        /// <summary>
        /// Loads a CSOM library that an assembly in this context needs from the module's "Core" folder into the default
        /// context, and returns <c>null</c> for any assembly that is not in that folder.
        ///
        /// <see cref="Load"/> leaves the CSOM libraries to the default context, but the runtime's fallback to the default
        /// context only finds an assembly that is already loaded there. PowerShell loads the CSOM libraries from "Core" only
        /// when PnP.PowerShell.dll asks for them, as it resolves the dependencies of an assembly from that assembly's
        /// folder for requests from the default context only. Microsoft.SharePoint.Client.UserProfiles, .Search and
        /// .Search.Applications are not needed to import the module, so PnP.Framework failed to load them whenever
        /// PnP.PowerShell.dll had not used them first, for instance when applying a template with localizations
        /// (issue #5495).
        ///
        /// This handler runs only after the default context has no copy, so a copy that is already loaded is never
        /// loaded twice. <see cref="Assembly.LoadFrom(string)"/> loads into the default context the way PowerShell loads
        /// the other assemblies in "Core", so the dependencies of the loaded assembly resolve from "Core" as well.
        /// </summary>
        private Assembly ResolveFromModuleAssemblyFolder(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName?.Name))
            {
                return null;
            }

            string candidate = Path.Combine(_moduleAssemblyPath, assemblyName.Name + ".dll");
            return File.Exists(candidate) ? Assembly.LoadFrom(candidate) : null;
        }

        /// <summary>
        /// True for assemblies whose types cross the boundary with the default context AND for which sharing a
        /// host copy that is recent enough is safe/required (the MSAL family, and System.Text.Json which is always a
        /// shared framework assembly). Deliberately excludes Microsoft.Extensions.* so those stay strictly isolated.
        /// </summary>
        private static bool IsSharedBoundaryAssembly(string simpleName)
        {
            return simpleName.StartsWith(IdentityClientAssemblyName, StringComparison.OrdinalIgnoreCase)
                || simpleName.Equals("System.Text.Json", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns true when the default context provides a boundary assembly instead of this context. This is the
        /// case when the default context already holds a copy that is at least the version we ship, or a copy of an
        /// assembly we do not ship. A Microsoft.Identity.Client.* assembly only defers when Microsoft.Identity.Client
        /// itself defers, so our own MSAL is never combined with an MSAL extension of the host.
        /// </summary>
        internal bool DefersToDefaultContext(string simpleName)
        {
            if (!IsSharedBoundaryAssembly(simpleName))
            {
                return false;
            }

            if (simpleName.StartsWith(IdentityClientAssemblyName + ".", StringComparison.OrdinalIgnoreCase)
                && !DefersToDefaultContext(IdentityClientAssemblyName))
            {
                return false;
            }

            Version loadedVersion = GetVersionLoadedInDefaultContext(simpleName);
            if (loadedVersion == null)
            {
                return false;
            }

            Version shippedVersion = GetShippedVersion(simpleName);
            return shippedVersion == null || loadedVersion >= shippedVersion;
        }

        /// <summary>
        /// Returns the version of the assembly with the given simple name loaded in the default context, or
        /// <c>null</c> when the default context has not loaded it.
        /// </summary>
        private static Version GetVersionLoadedInDefaultContext(string simpleName)
        {
            return FindAssembly(Default.Assemblies, simpleName)?.GetName().Version;
        }

        /// <summary>
        /// Returns a loaded copy of a deferred boundary assembly for a request the default context could not bind.
        /// The copy of this context is preferred. Otherwise the copy of the default context is used if it has at least
        /// the requested version. Returns <c>null</c> when no loaded copy qualifies.
        /// </summary>
        internal Assembly ResolveDeferredAssembly(AssemblyName assemblyName)
        {
            Assembly ownCopy = FindAssembly(Assemblies, assemblyName.Name);
            if (ownCopy != null)
            {
                return ownCopy;
            }

            Assembly hostCopy = FindAssembly(Default.Assemblies, assemblyName.Name);
            if (hostCopy != null && (assemblyName.Version == null || hostCopy.GetName().Version >= assemblyName.Version))
            {
                return hostCopy;
            }
            return null;
        }

        /// <summary>
        /// Returns the assembly with the given simple name, or <c>null</c> when none of the assemblies has that name.
        /// </summary>
        private static Assembly FindAssembly(System.Collections.Generic.IEnumerable<Assembly> assemblies, string simpleName)
        {
            foreach (Assembly assembly in assemblies)
            {
                if (string.Equals(assembly.GetName().Name, simpleName, StringComparison.OrdinalIgnoreCase))
                {
                    return assembly;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the version of the assembly we ship in the dependency folder, or <c>null</c> when we do not ship it.
        /// </summary>
        private Version GetShippedVersion(string simpleName)
        {
            return _shippedVersions.GetOrAdd(simpleName, name =>
            {
                string candidate = Path.Combine(_dependencyPath, name + ".dll");
                return File.Exists(candidate) ? AssemblyName.GetAssemblyName(candidate).Version : null;
            });
        }

        /// <summary>
        /// Resolves a native (unmanaged) library shipped under <c>runtimes/&lt;rid&gt;/native</c> alongside our
        /// managed dependencies (for example msalruntime.dll used by Microsoft.Identity.Client's WAM broker).
        /// Returns <see cref="IntPtr.Zero"/> to fall back to the default probing logic when nothing matches, so
        /// this override is purely additive and never removes existing behavior.
        /// </summary>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            if (string.IsNullOrEmpty(unmanagedDllName))
            {
                return IntPtr.Zero;
            }

            foreach (string rid in _nativeRuntimeIdentifiers)
            {
                string nativeDir = Path.Combine(_dependencyPath, "runtimes", rid, "native");
                if (!Directory.Exists(nativeDir))
                {
                    continue;
                }

                foreach (string fileName in GetNativeFileNameCandidates(unmanagedDllName))
                {
                    string candidate = Path.Combine(nativeDir, fileName);
                    if (File.Exists(candidate))
                    {
                        return LoadUnmanagedDllFromPath(candidate);
                    }
                }
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Builds the list of runtime identifiers to probe for native libraries, most specific first
        /// (e.g. "win-x64" then "win").
        /// </summary>
        private static string[] BuildNativeRuntimeIdentifiers()
        {
            string os;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                os = "win";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                os = "osx";
            }
            else
            {
                os = "linux";
            }

            string arch = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X64 => "x64",
                Architecture.X86 => "x86",
                Architecture.Arm64 => "arm64",
                Architecture.Arm => "arm",
                _ => RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant()
            };

            return new[] { $"{os}-{arch}", os };
        }

        /// <summary>
        /// Yields the platform-specific file name variants for an unmanaged library name.
        /// </summary>
        private static System.Collections.Generic.IEnumerable<string> GetNativeFileNameCandidates(string unmanagedDllName)
        {
            // The name may or may not already include an extension - try it verbatim first.
            yield return unmanagedDllName;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                yield return unmanagedDllName + ".dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                yield return unmanagedDllName + ".dylib";
                yield return "lib" + unmanagedDllName + ".dylib";
            }
            else
            {
                yield return unmanagedDllName + ".so";
                yield return "lib" + unmanagedDllName + ".so";
            }
        }
    }
}
