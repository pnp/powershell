---
Module Name: PnP.PowerShell
title: Get-PnPFileCheckedOut
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileCheckedOut.html
---
 
# Get-PnPFileCheckedOut

## SYNOPSIS
Returns all files that are currently checked out in a library

## SYNTAX

```powershell
Get-PnPFileCheckedOut -List <ListPipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

This cmdlet allows to retrieve all files that are currently checked out in a library.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFileCheckedOut -List "Documents"
```

Returns all files that are currently checked out in the "Documents" library.

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
The list instance, list display name, list url or list id to query for checked out files

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)