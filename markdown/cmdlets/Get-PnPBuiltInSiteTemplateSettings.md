---
Module Name: PnP.PowerShell
title: Get-PnPBuiltInSiteTemplateSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPBuiltInSiteTemplateSettings.html
---
 
# Get-PnPBuiltInSiteTemplateSettings

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves the current configuration of the built into SharePoint Online site templates.

## SYNTAX

```powershell
Get-PnPBuiltInSiteTemplateSettings [-Identity <BuiltInSiteTemplateSettingsPipeBind>] [-Template <BuiltInSiteTemplates>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Retrieves the current configuration of the built into SharePoint Online site templates.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPBuiltInSiteTemplateSettings
```

Returns all available configuration for the all of built into SharePoint Online site templates. If no configuration has been set previously for a template, no configuration for it will be returned, meaning it will be visible.

### EXAMPLE 2
```powershell
Get-PnPBuiltInSiteTemplateSettings -Identity 9522236e-6802-4972-a10d-e98dc74b3344
```

Returns the configuration for the Event Planning built into SharePoint Online site template.

### EXAMPLE 3
```powershell
Get-PnPBuiltInSiteTemplateSettings -Template CrisisManagement
```

Returns the configuration for the Crisis Management built into SharePoint Online site template.

### EXAMPLE 4
```powershell
Get-PnPBuiltInSiteTemplateSettings -Identity 00000000-0000-0000-0000-000000000000
```

Returns the default configuration for the built into SharePoint Online site templates.

### EXAMPLE 5
```powershell
Get-PnPBuiltInSiteTemplateSettings -Template All
```

Returns the default configuration for the built into SharePoint Online site templates.

## PARAMETERS

### -Identity
Id of the built into SharePoint Online site template to return configuration settings for. If no configuration has been set previously for a template, no configuration for it will be returned, meaning it will be visible. See https://learn.microsoft.com/powershell/module/sharepoint-online/set-spobuiltinsitetemplatesettings?view=sharepoint-ps#description for the full list of available types.

```yaml
Type: Guid
Parameter Sets: ByIdentity

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Template
Internal name of the built into SharePoint Online site template to return configuration settings for. If no configuration has been set previously for a template, no configuration for it will be returned, meaning it will be visible.

```yaml
Type: BuiltInSiteTemplates
Parameter Sets: ByTemplate

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