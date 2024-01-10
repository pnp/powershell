---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPWebPermissions.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPWebPermissions
---
  
# Get-PnPWebPermissions

## SYNOPSIS
Returns the explicit permissions for a specific SharePoint Web given a user or group by id.

## SYNTAX

```powershell
Get-PnPWebPermissions [-Identity] <WebPipeBind> -PrincipalId <Int32>
```

## DESCRIPTION

This cmdlet retrieves the web permissions (role definitions) for a specific user or group in a provided web.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPWebPermissions -Identity (Get-PnPWeb) -PrincipalId 60
```

Returns the permissions for the SharePoint group with id for the current Web.

### EXAMPLE 2
```powershell
Get-PnPListPermissions -Identity "subsite" -PrincipalId (Get-PnPGroup -Identity DemoGroup).Id
```

Returns the permissions for the SharePoint group called DemoGroup for a given subsite path.

## PARAMETERS


### -Identity
The id, name or server relative url of the Web to retrieve the permissions for.

```yaml
Type: WebPipeBand
Parameter Sets: (All)
Aliases: Name

Required: False
Position: 0
Default value: (CurrentWeb)
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -PrincipalId
The id of a user or a SharePoint group. See Get-PnPUser and Get-PnPGroup.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: Name

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
