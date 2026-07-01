---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListPermissions.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPListPermissions
---
  
# Get-PnPListPermissions

## SYNOPSIS
Returns the permissions for a specific SharePoint List given a user or group by id.

## SYNTAX

```powershell
Get-PnPListPermissions [-Identity] <ListPipeBind> -PrincipalId <Int32>
```

## DESCRIPTION

This cmdlet retrieves the list permissions (role definitions) for a specific user or group in a provided list.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListPermissions -Identity DemoList -PrincipalId 60
```

Returns the permissions for the SharePoint group with id for the list DemoList.

### EXAMPLE 2
```powershell
Get-PnPListPermissions -Identity DemoList -PrincipalId (Get-PnPGroup -Identity DemoGroup).Id
```

Returns the permissions for the SharePoint group call DemoGroup for the list DemoList.

## PARAMETERS


### -Identity
The id, name or server relative url of the list to retrieve the permissions for.

```yaml
Type: ListPipeBind
Parameter Sets: (All)
Aliases: Name

Required: True
Position: 0
Default value: None
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
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


