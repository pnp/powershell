---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPIndexedProperty.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPIndexedProperty
---
  
# Add-PnPIndexedProperty

## SYNOPSIS
Marks the value of the propertybag key specified to be indexed by search.

## SYNTAX

```powershell
Add-PnPIndexedProperty [-Key] <String> [-List <ListPipeBind>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to set search index on propertybag key.

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

### -Key
Key of the property bag value to be indexed

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
The list object or name where to set the indexed property

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


