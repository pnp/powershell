# GitHub Copilot Instructions for PnP PowerShell

## Project Overview

**PnP PowerShell** is a .NET 8 based PowerShell Module providing over 750 cmdlets that work with Microsoft 365 environments such as SharePoint Online, Microsoft Teams, Microsoft Project, Security & Compliance, Entra ID (Azure AD), and more.

This is a cross-platform module (Windows, macOS, Linux) that requires PowerShell 7.4.0 or newer and is based on .NET 8.0. It is the successor of the PnP-PowerShell module which only worked on Windows PowerShell.

### Key Characteristics
- **Open-source community project** - No SLA or direct Microsoft support
- **750+ cmdlets** for Microsoft 365 services
- **Cross-platform** - Works on Windows, Linux, and macOS
- **Modern stack** - .NET 8 / C# 12 / PowerShell 7.4+
- **Active development** - Nightly builds and regular releases

## Tech Stack

- **Language**: C# 12
- **Framework**: .NET 8.0
- **Target Platform**: PowerShell 7.4+
- **Build System**: .NET SDK 8
- **Dependencies**:
  - PnP Framework
  - PnP Core SDK
  - Microsoft.SharePoint.Client (CSOM)
  - Microsoft Graph SDK

## Repository Structure

```
/
├── .github/              # GitHub workflows and configurations
├── build/                # Build scripts (PowerShell)
├── src/
│   ├── Commands/         # Cmdlet implementations (organized by feature)
│   │   ├── Admin/        # Tenant administration cmdlets
│   │   ├── Apps/         # App catalog cmdlets
│   │   ├── Lists/        # List management cmdlets
│   │   ├── Sites/        # Site collection cmdlets
│   │   ├── Graph/        # Microsoft Graph cmdlets
│   │   └── ...           # Many other feature areas
│   ├── ALC/              # Assembly Load Context for dependency isolation
│   └── Resources/        # Embedded resources
├── documentation/        # Markdown documentation for each cmdlet
├── pages/                # Documentation website content
└── samples/              # Sample scripts and usage examples
```

## Cmdlet Development Patterns

### Cmdlet Class Structure

All cmdlets should follow this pattern:

```csharp
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.FeatureArea
{
    [Cmdlet(VerbsCommon.Get, "PnPSomething")]
    [OutputType(typeof(SomeType))]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    public class GetSomething : PnPWebRetrievalsCmdlet<SomeType>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public SomePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            // Implementation
        }
    }
}
```

### Key Conventions

1. **Cmdlet Naming**: Always use `PnP` prefix (e.g., `Get-PnPList`, `Set-PnPSite`)
2. **Verb Usage**: Follow PowerShell approved verbs (`Get`, `Set`, `Add`, `Remove`, `New`, etc.)
3. **Namespace Organization**: Group cmdlets by feature area in `PnP.PowerShell.Commands.FeatureArea`
4. **Base Classes**:
   - `PnPWebRetrievalsCmdlet<T>`: For cmdlets that return SharePoint objects with retrievals
   - `PnPGraphCmdlet`: For cmdlets that use Microsoft Graph
   - `PnPAdminCmdlet`: For tenant admin operations
   - `PnPSharePointCmdlet`: For general SharePoint operations

5. **Attributes**:
   - Always include `[RequiredApiApplicationPermissions]` and `[RequiredApiDelegatedPermissions]` to document required permissions
   - Use `[OutputType]` to specify return type
   - Use `[Parameter]` attributes with appropriate settings (Mandatory, ValueFromPipeline, Position)

6. **PipeBinds**: Use PipeBind classes for flexible parameter input (e.g., `ListPipeBind` accepts name, ID, or object)

7. **Error Handling**: 
   - Use `ThrowTerminatingError()` for fatal errors
   - Use `WriteWarning()` for non-fatal issues
   - Use `WriteVerbose()` for detailed logging

8. **Resource Strings**: Store error messages in `Resources.resx` and reference via `Resources.MessageName`

9. **Backward Compatibility**: When renaming a cmdlet or fixing a typo in a cmdlet name, always add an `[Alias()]` attribute with the old cmdlet name to maintain backward compatibility. Example:
   ```csharp
   [Cmdlet(VerbsCommon.Get, "PnPEntraIDAppSitePermission")]
   [Alias("Get-PnPAzureADAppSitePermission")]
   public class GetEntraIDAppSitePermission : PnPGraphCmdlet
   ```

## Coding Standards

### C# Style Guide

1. **Indentation**: Use tabs (not spaces) - this repository uses tabs for indentation
2. **Braces**: Opening brace on same line for methods, properties; new line for classes
3. **Naming**:
   - PascalCase for classes, methods, properties, public fields
   - camelCase for parameters, local variables, private fields
   - Prefix interfaces with `I` (e.g., `IListItem`)
4. **Null Checking**: Use null-conditional operators (`?.`, `??`) where appropriate
5. **LINQ**: Prefer LINQ for collection operations
6. **Async/Await**: Use async patterns for asynchronous operations

### Code Analysis
- EnforceCodeStyleInBuild is enabled
- EnableNETAnalyzers is enabled
- Address all warnings before committing

## Common Patterns

### Retrieving SharePoint Objects with Specific Properties

```csharp
DefaultRetrievalExpressions = [
    l => l.Id, 
    l => l.Title, 
    l => l.RootFolder.ServerRelativeUrl
];

var list = Identity.GetList(CurrentWeb);
list?.EnsureProperties(RetrievalExpressions);
WriteObject(list);
```

### Working with PipeBinds

```csharp
// Accepts ID, name, or object instance
[Parameter(Mandatory = true)]
public ListPipeBind Identity { get; set; }

// In ExecuteCmdlet
var list = Identity.GetList(CurrentWeb);
```

### Using Graph API

```csharp
public class GetGraphSomething : PnPGraphCmdlet
{
    protected override void ExecuteCmdlet()
    {
        var result = GraphHelper.GetAsync<SomeType>(
            Connection, 
            "/v1.0/endpoint",
            AccessToken
        ).GetAwaiter().GetResult();
        
        WriteObject(result);
    }
}
```

## Documentation

Every cmdlet must have:
1. **XML Documentation Comments** in the C# source
2. **Markdown Documentation** in `/documentation/{Cmdlet-Name}.md`
3. **Examples** showing typical usage

### Markdown Documentation Template

```markdown
# Get-PnPSomething

## Description
Brief description of what the cmdlet does.

## Syntax

### Parameter Set 1
```powershell
Get-PnPSomething [-Identity <String>] [-Connection <PnPConnection>]
```

## Examples

### Example 1
```powershell
Get-PnPSomething -Identity "Value"
```
Description of what this example does.

## Parameters

### -Identity
Description of the parameter.

## Outputs

### Type
Description of output type.
```

## Do's and Don'ts

### Do's
✅ Follow PowerShell naming conventions (Verb-PnPNoun)
✅ Use appropriate base classes (PnPWebRetrievalsCmdlet, PnPGraphCmdlet, etc.)
✅ Include all required permission attributes
✅ Write comprehensive parameter documentation
✅ Add examples to documentation
✅ Use PipeBind classes for flexible parameter input
✅ Handle errors gracefully with meaningful messages
✅ Use existing helper methods and utilities from base classes
✅ Follow the existing code structure and patterns
✅ Use `ClientContext.ExecuteQueryRetry()` instead of `ExecuteQuery()` for resilience
✅ Add `[Alias()]` attribute when renaming cmdlets to maintain backward compatibility

### Don'ts
❌ Don't add cmdlets without proper documentation
❌ Don't use `Console.WriteLine()` - use `WriteObject()`, `WriteWarning()`, `WriteVerbose()`
❌ Don't hardcode credentials or sensitive data
❌ Don't break backward compatibility without discussion
❌ Don't add unnecessary dependencies
❌ Don't commit commented-out code
❌ Don't ignore compiler warnings
❌ Don't use deprecated APIs or methods
❌ Don't create cmdlets that bypass standard authentication flows
❌ Don't use `ExecuteQuery()` directly - use `ExecuteQueryRetry()` for automatic retry logic

## Contributing Workflow

1. **Fork** the repository
2. **Clone** your fork locally
3. **Create a branch** for your feature/fix from `dev` branch
4. **Make changes** following the patterns above
5. **Update documentation** in `/documentation/` folder
6. **Commit** with clear, descriptive messages
7. **Push** to your fork
8. **Create Pull Request** to the `dev` branch

## Additional Resources

- [Main Documentation](https://pnp.github.io/powershell/)
- [Getting Started Contributing](https://pnp.github.io/powershell/articles/gettingstartedcontributing.html)
- [Migration Guides](https://github.com/pnp/powershell/blob/dev/MIGRATE-2.0-to-3.0.md)
- [Changelog](https://github.com/pnp/powershell/blob/dev/CHANGELOG.md)

## Notes for Copilot

When generating or modifying code:
- Always check existing cmdlets in the same feature area for patterns
- Maintain consistency with existing code style
- Consider cross-platform compatibility
- Remember that this module runs in PowerShell 7.4+ (not Windows PowerShell 5.1)
- Use modern C# 12 features where appropriate
- Prioritize readability and maintainability
- Follow the principle of least surprise for PowerShell users
- When starting from a GitHub issue, be sure to reference and link that issue in the proposed PR that would fix it
- When creating a PR to propose a code change, please include adding an entry to the [Changelog.md](https://github.com/pnp/powershell/blob/dev/CHANGELOG.md) file under the [Current nightly] section picking either subcategory as feels appropriate for the change at hand: Added, Changed, Fixed, Removed. Ensure it contains a link to the PR.
