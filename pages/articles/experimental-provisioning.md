---
uid: pnp.powershell.articles.experimental-provisioning
title: The experimental PnP.Core.Provisioning engine
description: What changes when a provisioning cmdlet runs with -Experimental, the permissions it needs, and which .pnp packages it can open.
---

# The experimental PnP.Core.Provisioning engine

The provisioning cmdlets run on the provisioning engine of PnP Framework. Add `-Experimental` and they run on PnP.Core.Provisioning instead, the provisioning engine built on the PnP Core SDK. It is a preview, so compare what it extracts or applies with the result of PnP Framework before you rely on it.

```powershell
Get-PnPSiteTemplate -Out template.xml -Experimental
Invoke-PnPSiteTemplate -Path template.xml -Experimental
```

`-Experimental` is available on `Get-PnPSiteTemplate`, `Invoke-PnPSiteTemplate`, `Get-PnPTenantTemplate`, `Invoke-PnPTenantTemplate`, `Read-PnPSiteTemplate`, `Save-PnPSiteTemplate`, `Read-PnPTenantTemplate`, `Save-PnPTenantTemplate`, `Convert-PnPSiteTemplate`, `Export-PnPListToSiteTemplate`, `Add-PnPDataRowsToSiteTemplate`, `Add-PnPListFoldersToSiteTemplate`, `Add-PnPFileToSiteTemplate`, `Remove-PnPFileFromSiteTemplate` and `Set-PnPSiteTemplateMetadata`.

## Permissions

PnP Framework works through CSOM, for which `AllSites.FullControl` on SharePoint is enough. PnP.Core.Provisioning also calls Microsoft Graph and the SharePoint REST API, so the application you connect with needs more permissions to use every part of the engine. Grant these delegated permissions to it, see [Register an Entra ID Application to use with PnP PowerShell](registerapplication.md) for how:

| API | Delegated permission |
| --- | --- |
| Microsoft Graph | `Group.ReadWrite.All` |
| Microsoft Graph | `openid` |
| Microsoft Graph | `profile` |
| Microsoft Graph | `Sites.Manage.All` |
| Microsoft Graph | `User.Read.All` |
| SharePoint | `AllSites.FullControl` |
| SharePoint | `AllSites.Manage` |
| SharePoint | `TermStore.ReadWrite.All` |
| SharePoint | `User.Read.All` |

## .pnp packages

For now, a .pnp package can only be opened by the engine that created it. A package saved without `-Experimental` cannot be opened with `-Experimental`. The cmdlets say which of the two to use when they are given a package of the other engine. An .xml template can be read by both engines.

## Differences from PnP Framework

**Pages and PageContents are one handler.** PnP.Core.Provisioning processes the pages of a site and their contents with a single `Pages` handler, so excluding either `Pages` or `PageContents` with `-ExcludeHandlers` excludes both, and a warning says so.

**At least one handler has to remain.** The engine runs every handler when it is given none, so `-Handlers` and `-ExcludeHandlers` values that leave no handler, such as `-ExcludeHandlers All`, are rejected rather than processing everything.

**Template provider extensions are not run.** `-TemplateProviderExtensions` has no effect, and a warning says so.

**`Get-PnPSiteTemplate` does not write markdown reports.** Extract to an .xml or .pnp file instead.
