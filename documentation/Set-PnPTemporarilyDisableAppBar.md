---
Module Name: PnP.PowerShell
title: Set-PnPTemporarilyDisableAppBar
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPTemporarilyDisableAppBar.html
---
 
# Set-PnPTemporarilyDisableAppBar

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Allows the SharePoint Online App Bar to be disabled. It may take some time for the change to be reflected in SharePoint Online. Support for this may be dropped after October 31st, 2021 after which the SharePoint Online App Bar will become visible anyway. See [https://admin.microsoft.com/#/MessageCenter/:/messages/MC240066] for more information.

## SYNTAX

```powershell
Set-PnPTemporarilyDisableAppBar -Enabled <Boolean> [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPTemporarilyDisableAppBar $true
```

Hides the SharePoint Online App Bar. 

### EXAMPLE 2
```powershell
Set-PnPTemporarilyDisableAppBar $false
```

Shows the SharePoint Online App Bar. 

## PARAMETERS

### -Enable
Specifies whether to show or hide SharePoint Online App Bar.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: True
Position: Named
Default value: True
Accept pipeline input: True
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

