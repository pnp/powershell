---
Module Name: PnP.PowerShell
title: Set-PnPBuiltInSiteTemplateSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPBuiltInSiteTemplateSettings.html
---
 
# Set-PnPBuiltInSiteTemplateSettings

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Allows configuration of the built-in SharePoint Online site templates.

## SYNTAX

### Configure through the site template identifier

```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity <BuiltInSiteTemplateSettingsPipeBind> -IsHidden <Boolean>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### Configure through the site template name

```powershell
Set-PnPBuiltInSiteTemplateSettings -Template <BuiltInSiteTemplates> -IsHidden <Boolean>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity 9522236e-6802-4972-a10d-e98dc74b3344 -IsHidden $false
```

Makes the Event Planning template visible

### EXAMPLE 2
```powershell
Set-PnPBuiltInSiteTemplateSettings -Identity 00000000-0000-0000-0000-000000000000 -IsHidden $true
```

Hides all the default built-in SharePoint Online site templates, except those specifically configured to be visible again

### EXAMPLE 3
```powershell
Set-PnPBuiltInSiteTemplateSettings -Template CrisisManagement -IsHidden $true
```

Hides the Crisis Management template

### EXAMPLE 4
```powershell
Set-PnPBuiltInSiteTemplateSettings -Template All -IsHidden $false
```

Shows by the default all the built-in SharePoint Online site templates, except those specifically configured to be hidden

## PARAMETERS

### -Identity
Id of the built-in site template to configure. See https://docs.microsoft.com/powershell/module/sharepoint-online/set-spobuiltinsitetemplatesettings?view=sharepoint-ps#description for the full list of available types.

```yaml
Type: Guid
Parameter Sets: ByIdentity

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Template
Internal name of the template

```yaml
Type: BuiltInSiteTemplates
Parameter Sets: ByTemplate

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsHidden
Defines if the built in site template should be hidden ($true) or visible ($false)

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases: cf

Required: True
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

### -WhatIf
Shows what would happen if the cmdlet runs. No changes will be made.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
