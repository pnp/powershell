---
Module Name: PnP.PowerShell
title: Get-PnPRecycleBinItem
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPRecycleBinItem.html
---
 
# Get-PnPRecycleBinItem

## SYNOPSIS

**Required Permissions** 

* SharePoint: Site Collection Administrator. SharePoint Tenant Admin alone is not enough

Returns one or more items from the Recycle Bin.

## SYNTAX

### All (Default)
```powershell
Get-PnPRecycleBinItem [-RowLimit <Int32>] [-Connection <PnPConnection>] [-Includes <String[]>]
 
```

### Identity
```powershell
Get-PnPRecycleBinItem [-Identity <Guid>] [-Connection <PnPConnection>] [-Includes <String[]>]
 
```

### FirstStage
```powershell
Get-PnPRecycleBinItem [-FirstStage] [-RowLimit <Int32>] [-Connection <PnPConnection>] [-Includes <String[]>]
 
```

### SecondStage
```powershell
Get-PnPRecycleBinItem [-SecondStage] [-RowLimit <Int32>] [-Connection <PnPConnection>] [-Includes <String[]>]
 
```

## DESCRIPTION
This command will return all the items in the recycle bin for the SharePoint site you connected to with Connect-PnPOnline. You must connect as a Site Collection Owner or Administrator. The SharePoint Admin Role in the tenant alone will not work. If you are not a Site Collection Admin connect to the Tenant Admin URL with Connect-PnPOnline and use Get-PnPTenantRecycleBinItem. 

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPRecycleBinItem
```

Returns all items in both the first and the second stage recycle bins in the current site collection.

### EXAMPLE 2
```powershell
Get-PnPRecycleBinItem -Identity f3ef6195-9400-4121-9d1c-c997fb5b86c2
```

Returns a specific recycle bin item by id.

### EXAMPLE 3
```powershell
Get-PnPRecycleBinItem -FirstStage
```

Returns all items in only the first stage recycle bin in the current site collection.

### EXAMPLE 4
```powershell
Get-PnPRecycleBinItem -SecondStage
```

Returns all items in only the second stage recycle bin in the current site collection.

### EXAMPLE 5
```powershell
Get-PnPRecycleBinItem -RowLimit 10000
```

Returns items in recycle bin limited by number of results.

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

### -FirstStage
Returns all items in the first stage recycle bin

```yaml
Type: SwitchParameter
Parameter Sets: FirstStage

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Returns a recycle bin item with a specific identity.

```yaml
Type: Guid
Parameter Sets: Identity

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RowLimit
Limits returned results to specified amount

```yaml
Type: Int32
Parameter Sets: All, FirstStage, SecondStage

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SecondStage
Returns all items in the second stage recycle bin.

```yaml
Type: SwitchParameter
Parameter Sets: SecondStage

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


