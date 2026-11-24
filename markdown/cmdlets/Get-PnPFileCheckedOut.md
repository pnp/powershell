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

Notice: if this cmdlet would return more then 5,000 results, so 5,000 or more checked out files, it will not work and will throw an error. This is unfortunately a limitation of SharePoint Online and not something that can be fixed in the cmdlet. As long as the number of checked out files is below 5,000, this cmdlet will work as expected, even on document libraries that contain more than 5,000 files.

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