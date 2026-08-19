using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// A dedicated <see cref="AssemblyLoadContext"/> that owns the complete private dependency
    /// graph shipped with PnP PowerShell (PnP.Framework, PnP.Core, PnP.Core.Auth, PnP.Core.Admin,
    /// the CSOM libraries, Microsoft.Extensions.*, Microsoft.Identity.*, Microsoft.ApplicationInsights,
    /// PnP.PowerShell.ALC, Newtonsoft.Json, AngleSharp, and so on).
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
        /// Absolute path to the folder that holds the private dependency graph (the module's "Common" folder).
        /// </summary>
        private readonly string _dependencyPath;

        /// <summary>
        /// Candidate runtime identifiers used to locate native (unmanaged) libraries, most specific first.
        /// </summary>
        private readonly string[] _nativeRuntimeIdentifiers;

        public PnPAssemblyLoadContext(string dependencyPath)
            : base(name: "PnP.PowerShell", isCollectible: false)
        {
            _dependencyPath = dependencyPath ?? throw new ArgumentNullException(nameof(dependencyPath));
            _nativeRuntimeIdentifiers = BuildNativeRuntimeIdentifiers();
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
            // sides. If the host process already loaded one into the default context, reuse that copy (return null
            // to defer to the default context) instead of loading our own, which would create two identities.
            // The concrete failure this prevents is MSAL's MsalCacheHelper.RegisterCache throwing across contexts
            // when a host (e.g. an Az module) preloaded Microsoft.Identity.Client. Assemblies NOT on this list -
            // above all Microsoft.Extensions.* - are always isolated, so a mismatched host version cannot break us
            // (that is the whole purpose of this context and the fix for issue #5350).
            if (IsSharedBoundaryAssembly(assemblyName.Name) && IsLoadedInDefaultContext(assemblyName.Name))
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
        /// True for assemblies whose types cross the boundary with the default context AND for which sharing the
        /// host's already-loaded copy is safe/required (the MSAL family, and System.Text.Json which is always a
        /// shared framework assembly). Deliberately excludes Microsoft.Extensions.* so those stay strictly isolated.
        /// </summary>
        private static bool IsSharedBoundaryAssembly(string simpleName)
        {
            return simpleName.StartsWith("Microsoft.Identity.Client", StringComparison.OrdinalIgnoreCase)
                || simpleName.Equals("System.Text.Json", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Checks whether an assembly with the given simple name is already loaded in the default context.
        /// </summary>
        private static bool IsLoadedInDefaultContext(string simpleName)
        {
            foreach (Assembly assembly in Default.Assemblies)
            {
                if (string.Equals(assembly.GetName().Name, simpleName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
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
