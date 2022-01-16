---
Module Name: PnP.PowerShell
title: Get-PnPListItemPermissions
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListItemPermissions.html
---
 
# Get-PnPListItemPermission

## SYNOPSIS
Gets list item permissions. 

## SYNTAX

```powershell

Get-PnPListItemPermissions [-List] <ListPipeBind> -Identity <ListItemPipeBind>
 [-Connection <PnPConnection>] [<CommonParameters>]

```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListItemPermissions -List 'Documents' -Identity 1
```

Get the permissions for listitem with id 1 in the list 'Documents'

## PARAMETERS

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

### -Identity
The ID of the listitem, or actual ListItem object

```yaml
Type: ListItemPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -List
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
