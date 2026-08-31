---
Module Name: PnP.PowerShell
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
applicable: SharePoint Online
title: Invoke-PnPSiteDesign
online version: https://pnp.github.io/powershell/cmdlets/Invoke-PnPSiteDesign.html
tags: Available in the current Nightly Release only.
---
  
# Invoke-PnPSiteDesign

## SYNOPSIS
Apply a Site Design to an existing site. * Requires Tenant Administration Rights *

## SYNTAX

### By Site Design (Default)

```powershell
Invoke-PnPSiteDesign [-Identity] <TenantSiteDesignPipeBind> [-WebUrl <String>] 
 [-Connection <PnPConnection>]   
```

### By Built-in Template

```powershell
Invoke-PnPSiteDesign -Template <BuiltInSiteTemplates> [-WebUrl <String>] 
 [-Connection <PnPConnection>]   
```

## DESCRIPTION

Applies a Site Design to an existing site.

Use `-Identity` to apply a tenant-registered site design by Id or name. When a name matches multiple designs, all of them will be invoked.

Use `-Template` to apply one of the built-in Microsoft site designs (e.g. Event, Department, Human Resources) directly by name, without needing to look up the design Id first. This calls the SharePoint SiteScriptUtility REST API against the built-in template store (store 1).

## EXAMPLES

### EXAMPLE 1
```powershell
Invoke-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Applies the specified site design to the current site.

### EXAMPLE 2
```powershell
Invoke-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd -WebUrl "https://contoso.sharepoint.com/sites/mydemosite"
```

Applies the specified site design to the specified site.

### EXAMPLE 3
```powershell
Get-PnPSiteDesign | ?{$_.Title -eq "Demo"} | Invoke-PnPSiteDesign
```

Applies the site design named "Demo" to the current site via the pipeline.

### EXAMPLE 4
```powershell
Invoke-PnPSiteDesign -Template Event -WebUrl "https://contoso.sharepoint.com/sites/myevent"
```

Applies the built-in Event site design to the specified communication site.

### EXAMPLE 5
```powershell
Invoke-PnPSiteDesign -Template Department
```

Applies the built-in Department site design to the current site.

## PARAMETERS

### -Identity
The Site Design Id, name, or an actual Site Design object to apply.

```yaml
Type: TenantSiteDesignPipeBind
Parameter Sets: By Site Design

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Template
The name of a built-in Microsoft site design to apply. Uses the SharePoint SiteScriptUtility REST API against the built-in template store (store 1). Cannot be combined with -Identity.

```yaml
Type: BuiltInSiteTemplates
Parameter Sets: By Built-in Template

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WebUrl
The URL of the web to apply the site design to. If not specified it will default to the current web based upon the URL specified with Connect-PnPOnline.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

