---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPListRecordDeclaration.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPListRecordDeclaration
---
  
# Get-PnPListRecordDeclaration

## SYNOPSIS
Returns the manual record declaration settings for a list

## SYNTAX

```powershell
Get-PnPListRecordDeclaration -List <ListPipeBind> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to retrieve the record declaration settings for given list.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPListRecordDeclaration -List "Documents"
```

Returns the record declaration setting for the list "Documents"

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
The list to retrieve the record declaration settings for

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


