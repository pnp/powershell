---
name: new-cmdlet
description: The conventions a PnP PowerShell cmdlet must satisfy - base class selection, permission attributes, PipeBinds, parameter validation, Graph and CSOM call patterns, and the documentation plus changelog artefacts that make a cmdlet complete. Use when writing, modifying or reviewing cmdlet code, or when deciding which base class or permission attribute applies.
---

# Playbook: new-cmdlet

The conventions a cmdlet in this repository must satisfy. A reference — read it before writing or
reviewing cmdlet code. For the step-by-step generation procedure see
[`cmdlet-scaffolder`](../cmdlet-scaffolder/SKILL.md); for language-level rules see
[`dotnet-standards`](../dotnet-standards/SKILL.md).

## Pick the base class first

It determines the connection, the resource, the permission flavour and which helpers exist.

| Base class | Use for | Gives you |
|---|---|---|
| `PnPWebCmdlet` | Operations on the connected site/web | `CurrentWeb`, `ClientContext` |
| `PnPWebRetrievalsCmdlet<T>` | The above, returning objects with selectable properties | `RetrievalExpressions`, `-Includes` |
| `PnPSharePointCmdlet` | General SharePoint, no web context needed | `ClientContext` |
| `PnPSharePointOnlineAdminCmdlet` | Tenant admin operations | `Tenant`, `AdminContext` |
| `PnPGraphCmdlet` | Microsoft Graph | `Connection`, `AccessToken`, `GraphRequestHelper` |
| `PnPAzureManagementApiCmdlet` | Azure Management API | |
| `PnPOfficeManagementApiCmdlet` | Office 365 Management API | |
| `PnPGcsCmdlet`, `PnPTasksCmdlet` | Consumer storage, Planner/Tasks | |
| `PnPConnectedCmdlet` | Needs a connection, none of the above | |
| `BasePSCmdlet` | No connection at all | |

Choosing wrong is a real defect: a tenant-admin operation on `PnPWebCmdlet` will use the wrong
context and fail confusingly in someone's tenant.

## Shape

```csharp
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.FeatureArea
{
    [Cmdlet(VerbsCommon.Get, "PnPSomething")]
    [OutputType(typeof(SomeType))]
    [RequiredApiDelegatedPermissions("sharepoint/AllSites.Read")]
    [RequiredApiApplicationPermissions("sharepoint/Sites.Read.All")]
    public class GetSomething : PnPWebRetrievalsCmdlet<SomeType>
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        public SomePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            // ...
            WriteObject(result, true);
        }
    }
}
```

- Namespace mirrors the folder: `src/Commands/Lists/` → `PnP.PowerShell.Commands.Lists`.
- Override `ExecuteCmdlet()`, never `ProcessRecord()` — the base class owns connection handling and
  error translation.
- Renaming an existing cmdlet **requires** `[Alias("Old-PnPName")]` for backward compatibility.

## Permission attributes

`"<resource>/<scope>"`. Multiple attributes are ORed; multiple scopes inside one attribute are ANDed.

```csharp
[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl", "sharepoint/User.ReadWrite.All")]
[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All", "sharepoint/User.ReadWrite.All")]
```

Also available: `RequiredApiDelegatedOrApplicationPermissions`,
`ApiNotAvailableUnderDelegatedPermissions`, `ApiNotAvailableUnderApplicationPermissions`,
`ApiPermissionsNotRequired`, `ApiPermissionsDependOnResource`.

**An unrecognised resource prefix is silently treated as SharePoint.**
`TokenHandler.DefineResourceTypeFromAudience` defaults every audience it does not recognise to
SharePoint (only blank input becomes `Unknown`), so `"garph/Group.Read.All"` declares a *SharePoint*
`Group.Read.All` rather than failing. A string with no `/` at all fails the regex and is dropped.
Declare least privilege, and verify the scope against Microsoft Learn rather than from memory. See
[`permissions-auditor`](../permissions-auditor/SKILL.md).

## Common patterns

**SharePoint retrievals**

```csharp
DefaultRetrievalExpressions = [l => l.Id, l => l.Title, l => l.RootFolder.ServerRelativeUrl];

var list = Identity.GetList(CurrentWeb);
list?.EnsureProperties(RetrievalExpressions);
WriteObject(list);
```

**Graph**

```csharp
var result = GraphRequestHelper.GetResultCollection<SomeType>(this, "v1.0/groups?$select=id,displayName");
WriteObject(result, true);
```

Use `GetResultCollection` for collections — it follows `@odata.nextLink`. `Get` returns the first
page only. Match the signature of a neighbouring cmdlet in the same folder; these helpers have
evolved and older call shapes still exist in the tree.

**CSOM** — always `ExecuteQueryRetry()`, never `ExecuteQuery()`.

## Definition of done

A cmdlet is not finished until all of these exist in the same PR:

1. **Class** — correct base class, `[Cmdlet]`, `[OutputType]`, permission attributes, `[Alias]` if
   renaming.
2. **Parameters** — PipeBinds, validation attributes, deliberate `Mandatory`/`Position`/pipeline
   binding; `ParameterSpecified` where a default is meaningful.
3. **`documentation/<Verb-PnPNoun>.md`** — full platyPS page: front matter, `## SYNOPSIS` — carrying
   a **Required Permissions** block **only when the cmdlet declares API permissions**; a cmdlet
   marked `ApiPermissionsNotRequired` must not have one, and none of the ten such pages does —
   `## SYNTAX` (one block per parameter set), `## DESCRIPTION`,
   `## EXAMPLES` (at least one, ` ```powershell ` fenced, each with a sentence of explanation),
   `## PARAMETERS` **alphabetical, YAML blocks only**, including the standard `-Connection` and
   `-Verbose` sections, then `## RELATED LINKS`. Copy the structure from a sibling page.
4. **`CHANGELOG.md`** — a line under `[Current nightly]` → `Added` / `Changed` / `Fixed` /
   `Removed`, naming the cmdlets in backticks and linking the PR or issue.
5. **Build clean** — `dotnet build src/PnP.PowerShell.sln`, no new warnings.

Removing a cmdlet means deleting its documentation page too.

> "In the same PR" means the same working tree. **Never commit, push, or open the PR yourself** —
> see [Human in the loop](../../../AGENTS.md#human-in-the-loop).

## What an agent cannot do here

You cannot run the cmdlet. Anything calling an API you have not invoked is a **draft**: say which
API shapes, response fields and permission scopes you inferred rather than verified, so a maintainer
knows exactly what to check against a tenant.
