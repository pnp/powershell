---
Module Name: PnP.PowerShell
title: Undo-PnPFileCheckedOut
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Undo-PnPFileCheckedOut.html
---
 
# Undo-PnPFileCheckedOut

## SYNOPSIS
Discards changes to a file.

## SYNTAX

```powershell
Undo-PnPFileCheckedOut [-Url] <String> [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet discards changes to a single file.

## EXAMPLES

### EXAMPLE 1
```powershell
Undo-PnPFileCheckedOut -Url "/sites/PnP/Shared Documents/Contract.docx"
```

Discards changes in the file "Contract.docx" in the "Documents" library

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

### -Url
The server relative url of the file to discard changes.

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
