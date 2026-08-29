---
Module Name: PnP.PowerShell
schema: 2.0.0
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPFileCheckedOut
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPFileCheckedOut.html
---
  
# Set-PnPFileCheckedOut

## SYNOPSIS
Checks out a file

## SYNTAX

```powershell
Set-PnPFileCheckedOut [-Url] <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to check out a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPFileCheckedOut -Url "/sites/testsite/subsite/Documents/Contract.docx"
```

Checks out the file "Contract.docx" in the "Documents" library.

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
The server relative url of the file to check out. A sequence such as `%20` in the URL is taken literally when a file of that name exists, and is decoded otherwise.

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


