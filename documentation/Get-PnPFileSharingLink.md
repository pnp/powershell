---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFileSharingLink
---
  
# Get-PnPFileSharingLink

## SYNOPSIS
Retrieves sharing links to associated with the file.

## SYNTAX

```powershell
Get-PnPFileSharingLink -FileUrl <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Retrieves sharing links for a file.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFileSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx"
```

This will fetch sharing links for `Test.docx` file in the `Shared Documents` library.

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

### -FileUrl
The file in the site

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
