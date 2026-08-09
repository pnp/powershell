---
name: permissions-auditor
description: Audit PnP PowerShell permission attributes against the APIs a cmdlet actually calls and against its documentation. Use when a change adds or alters an API call or a RequiredApi* attribute, when a user reports a 401/403 or a consent problem, or to sweep a folder under src/Commands/ for wrong or over-declared permissions.
---

# Playbook: permissions-auditor

Verify that a cmdlet's declared API permissions match the APIs it actually calls, and that its
documentation says the same thing.

## Why this matters more than it looks

Wrong permission metadata fails **silently at runtime, in someone else's tenant**. Nothing in the
build catches it, no test covers it, and the user finds out through a 401 in an unattended script or
by granting an app far more access than the cmdlet needs. It is the highest-value static check in
this repository.

There are three independent statements of the same fact, and all three can be compared without
running anything:

1. the **attributes** on the cmdlet class
2. the **API calls** in the method body
3. the **Required Permissions** block in `documentation/<Cmdlet>.md`

Any disagreement between them is a finding.

> Report findings in the session. **Never open an issue or PR for one** — see
> [Human in the loop](../../../AGENTS.md#human-in-the-loop). A permission finding is security-adjacent;
> a maintainer decides whether it goes on a public tracker.

## The attributes

Declared in `src/Commands/Attributes/`:

| Attribute | Meaning |
|---|---|
| `RequiredApiDelegatedPermissions` | Needed when connected with delegated permissions. |
| `RequiredApiApplicationPermissions` | Needed when connected with application permissions. |
| `RequiredApiDelegatedOrApplicationPermissions` | Same scope satisfies either flavour. |
| `ApiNotAvailableUnderDelegatedPermissions` | Cmdlet cannot work delegated at all. |
| `ApiNotAvailableUnderApplicationPermissions` | Cmdlet cannot work app-only at all. |
| `ApiPermissionsNotRequired` | Needs nothing on the app registration. Informational. |
| `ApiPermissionsDependOnResource` | Requirement follows from the resource or invocation. Informational. |

Semantics, from `RequiredApiPermissionsBase`:

- **Multiple attributes on one class are ORed.** Any one of them being satisfied is enough.
- **Multiple scopes inside one attribute are ANDed.** All of them are required.

Scope format is `<resource>/<scope>`, optionally `https://`-prefixed:

```csharp
[RequiredApiDelegatedPermissions("sharepoint/AllSites.FullControl", "sharepoint/User.ReadWrite.All")]
[RequiredApiApplicationPermissions("sharepoint/Sites.FullControl.All", "sharepoint/User.ReadWrite.All")]
```

### The failure mode to hunt for first

`RequiredApiPermissionsBase` parses each string with a regex and maps the resource through
`TokenHandler.DefineResourceTypeFromAudience`. **If the resource does not map, it returns `null` and
the scope is silently dropped.** A misspelled or unrecognised resource prefix therefore produces a
cmdlet that declares *no* requirement at all, rather than an error. Grep for scope strings whose
resource segment is not one of the recognised audiences, and for a `null` slot surviving into
`PermissionScopes`.

Second failure mode: an attribute array with a **single** scope where the code demonstrably needs
two (e.g. reads a user *and* writes a site), or the reverse — an AND list that over-declares and
forces users to grant more than the cmdlet uses. Over-declaration is a real finding here, not a
nitpick; least privilege is the point of the attribute.

## Establishing what the code actually calls

Work out the resource from the base class, then confirm against the call sites.

| Base class | Resource |
|---|---|
| `PnPGraphCmdlet` | Microsoft Graph |
| `PnPWebCmdlet`, `PnPSharePointCmdlet`, `PnPWebRetrievalsCmdlet<T>` | SharePoint (CSOM, site scope) |
| `PnPSharePointOnlineAdminCmdlet` | SharePoint (CSOM, tenant admin scope) |
| `PnPAzureManagementApiCmdlet` | Azure Management |
| `PnPOfficeManagementApiCmdlet` | Office 365 Management |
| `PnPGcsCmdlet` | Graph consumer / cloud storage |
| `PnPTasksCmdlet` | Planner / Tasks |

Call sites to read:

- `GraphRequestHelper.Get / GetResultCollection / Post / PostHttpContent / Patch / Delete` — the URL
  literal names the Graph endpoint. Look it up rather than guessing its scopes.
- `RequestHelper.*` — same shape, non-Graph resource.
- CSOM: `Tenant.<Method>`, `CurrentWeb.<...>`, `ClientContext.Load` + `ExecuteQueryRetry()`.
- A cmdlet may call **more than one** resource. Every one of them needs covering.

Do not infer a scope from the cmdlet name. `Get-` prefixed cmdlets routinely need write scopes
because the underlying admin API is a POST, and several `Set-` cmdlets need only a read scope on a
second resource they look something up in.

**Use the Microsoft Learn MCP server** to resolve a Graph endpoint to its documented least-privilege
permission set. Do not answer from memory — Graph permission requirements change, and a confidently
wrong scope here is worse than an admitted gap.

## Cross-checking against the documentation

`documentation/<Verb-PnPNoun>.md` states the same requirement in prose under `## SYNOPSIS`:

```markdown
## SYNOPSIS

**Required Permissions**

Access to SharePoint admin site

* SharePoint: AllSites.FullControl and User.ReadWrite.All when using delegated permissions
* SharePoint: Sites.FullControl.All and User.ReadWrite.All when using application permissions
```

Check that every scope in the attributes appears here with the right delegated/application flavour,
and that nothing appears here that the attributes do not declare. ANDed scopes read as "X and Y";
ORed alternatives read as separate bullets. A cmdlet carrying `ApiPermissionsNotRequired` should not
have a Required Permissions block at all.

## Existing tooling to lean on

The repository already ships two cmdlets that read this metadata. Read their implementations to
match their interpretation exactly rather than inventing your own:

- `Get-PnPCommandPermission` (`src/Commands/Base/GetCommandPermission.cs`) — returns the declared and
  derived permissions for a cmdlet.
- `Test-PnPConnectionPermission` — compares a token's claims against that metadata.

If your reading of an attribute disagrees with what these cmdlets do, they are the authority.

## Procedure

1. Scope the run: a folder under `src/Commands/`, a changed file set, or one cmdlet. State the scope
   in the report — a partial audit reported as complete is worse than no audit.
2. For each cmdlet class, collect: base class, permission attributes, every API call site.
3. Resolve each call site to its documented least-privilege permissions (Microsoft Learn MCP).
4. Compare attributes ↔ code. Then attributes ↔ documentation synopsis.
5. Discard anything you cannot substantiate. An unverified scope claim is worse than a gap.

## Reporting

One row per finding, ordered by consequence:

| Field | Content |
|---|---|
| Cmdlet | `Verb-PnPNoun` and `file.cs:line` |
| Kind | `missing` / `over-declared` / `wrong-flavour` / `silently-dropped` / `doc-mismatch` |
| Declared | What the attributes say |
| Required | What the API actually needs, **with the Learn URL you got it from** |
| Consequence | The concrete failure — "app-only connection gets 403 on `/users/{id}/drive`" |

Say plainly which cmdlets you checked and which you could not resolve. `Confidence: high` only when
you have a Microsoft source for the scope; otherwise say what would settle it.
