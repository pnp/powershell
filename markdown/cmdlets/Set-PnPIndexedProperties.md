---
Module Name: PnP.PowerShell
title: Set-PnPIndexedProperties
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPIndexedProperties.html
---
 
# Set-PnPIndexedProperties

## SYNOPSIS
Marks values of the propertybag to be indexed by search.

## SYNTAX

```powershell
Set-PnPIndexedProperties -Keys <System.Collections.Generic.List`1[System.String]> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Marks values of the propertybag to be indexed by search. Notice that this will overwrite the existing flags, i.e. only the properties you define with the cmdlet will be indexed.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPIndexedProperties -Keys SiteClosed, PolicyName
```

Example 1 overwrites the existing properties from the index and sets `SiteClosed` and `PolicyName` properties to be indexed.

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

### -Keys
Property keys to be indexed.

```yaml
Type: System.Collections.Generic.List`1[System.String]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

