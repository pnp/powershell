---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileSensitivityLabelInfo.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFileSensitivityLabelInfo
---

# Get-PnPFileSensitivityLabelInfo

## SYNOPSIS
Retrieves the sensitivity label information for a file in SharePoint.

## SYNTAX
```powershell
Get-PnPFileSensitivityLabelInfo -Url <String>
```

## DESCRIPTION

The Get-PnPFileSensitivityLabelInfo cmdlet retrieves the sensitivity label information for a file in SharePoint. It takes a URL as input, decodes it, and specifically encodes the '+' character if it is part of the filename.

## EXAMPLES

### Example 1
This example retrieves the sensitivity label information for the file at the specified URL.

```powershell
Get-PnPFileSensitivityLabelInfo -Url "https://contoso.sharepoint.com/sites/Marketing/Shared Documents/Report.pdf"
```

This example retrieves the sensitivity label information for the file at the specified URL.

## PARAMETERS

### -Url
Specifies the URL of the file for which to retrieve the sensitivity label information.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
