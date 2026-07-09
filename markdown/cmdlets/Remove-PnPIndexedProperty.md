---
Module Name: PnP.PowerShell
title: Remove-PnPIndexedProperty
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPIndexedProperty.html
---
 
# Remove-PnPIndexedProperty

## SYNOPSIS
Removes a key from propertybag to be indexed by search. The key and it's value remain in the propertybag, however it will not be indexed anymore.

## SYNTAX

```powershell
Remove-PnPIndexedProperty [-Key] <String> [-List <ListPipeBind>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove indexed property from the current web.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPIndexedProperty -key "MyIndexProperty"
```

Removes the Indexed property "MyIndexProperty" from the current web

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

### -Key
Key of the property bag value to be removed from indexing

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The list object or name from where to remove the indexed properties

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

