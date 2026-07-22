---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPIndexedPropertyKeys.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPIndexedPropertyKeys
---
  
# Get-PnPIndexedPropertyKeys

## SYNOPSIS
Returns the keys of the property bag values that have been marked for indexing by search

## SYNTAX

```powershell
Get-PnPIndexedPropertyKeys [-List <ListPipeBind>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

## EXAMPLES

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

### -List
The list object or name from where to get the indexed properties

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


