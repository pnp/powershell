---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPBuiltInSiteTemplateSettings.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPBuiltInSiteTemplateSettings
---

# Set-PnPBuiltInSiteTemplateSettings

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Allows configuration of the built-in SharePoint Online site templates.

## SYNTAX

### Configure through the site template identifier

```
Set-PnPBuiltInSiteTemplateSettings -Identity <BuiltInSiteTemplateSettingsPipeBind>
 -IsHidden <Boolean> [-Connection <PnPConnection>] [-WhatIf]
```

### Configure through the site template name

```
Set-PnPBuiltInSiteTemplateSettings -Template <BuiltInSiteTemplates> -IsHidden <Boolean>
 [-Connection <PnPConnection>] [-WhatIf]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows the built-in SharePoint Online site templates to be shown or hidden.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity 9522236e-6802-4972-a10d-e98dc74b3344 -IsHidden $false
```

Makes the Event Planning template visible.

### EXAMPLE 2

```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity 00000000-0000-0000-0000-000000000000 -IsHidden $true
```

Hides all the default built-in SharePoint Online site templates, except those specifically configured to be visible again.

### EXAMPLE 3

```powershell
Set-PnPBuiltInSiteTemplateSettings -Template CrisisManagement -IsHidden $true
```

Hides the Crisis Management template.

### EXAMPLE 4

```powershell
Set-PnPBuiltInSiteTemplateSettings -Template All -IsHidden $false
```

Shows by the default all the built-in SharePoint Online site templates, except those specifically configured to be hidden.

## PARAMETERS

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

