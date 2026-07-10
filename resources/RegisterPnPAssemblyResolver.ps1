# PnP PowerShell dependency-isolation bootstrap.
#
# This script is referenced by the module manifest's ScriptsToProcess, which the PowerShell module system
# runs BEFORE it processes the binary module in NestedModules (Core/PnP.PowerShell.dll). That ordering is
# essential: as soon as PowerShell reflects over the cmdlet types to discover them, it forces the cmdlet base
# types' dependencies (PnP.Framework / PnP.Core, and their Microsoft.Extensions.* graph) to load. If PnP's
# private-AssemblyLoadContext resolver is not already registered at that moment, those dependencies would bind
# against whatever the host process (for example the Azure Functions runtime) has already loaded into the
# default load context - which is the root cause of the "Method not found ... AddOptions" class of failures.
#
# We load the module assembly by explicit path (so no resolver is needed to find it) and invoke its public
# registration entry point, which wires up the resolver on the default AssemblyLoadContext. Registration is
# idempotent, so running this on every import is harmless.

$moduleRoot = $PSScriptRoot
$coreAssemblyPath = Join-Path -Path $moduleRoot -ChildPath 'Core/PnP.PowerShell.dll'

if (Test-Path -LiteralPath $coreAssemblyPath) {
    $assembly = [System.Runtime.Loader.AssemblyLoadContext]::Default.LoadFromAssemblyPath($coreAssemblyPath)
    $initializerType = $assembly.GetType('PnP.PowerShell.Commands.Base.PnPPowerShellModuleInitializer')
    if ($null -ne $initializerType) {
        $registerMethod = $initializerType.GetMethod(
            'EnsureDependencyResolverRegistered',
            [System.Reflection.BindingFlags]'Public, Static')
        if ($null -ne $registerMethod) {
            [void]$registerMethod.Invoke($null, $null)
        }
    }
}
