---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileSensitivityLabel.html
tags: Available in the current Nightly Release only.
title: Get-PnPFileSensitivityLabel
---
 
# Get-PnPFileSensitivityLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Files.Read.All, Sites.Read.All, Files.ReadWrite.All, Sites.ReadWrite.All

Retrieves the sensitivity label information for a file in SharePoint.

## SYNTAX
```powershell
Get-PnPFileSensitivityLabel -Url <String>
```

## DESCRIPTION

The Get-PnPFileSensitivityLabel cmdlet retrieves the sensitivity label information for a file in SharePoint using Microsoft Graph. It takes a URL as input, decodes it, and specifically encodes the '+' character if it is part of the filename.

## EXAMPLES

### Example 1
This example retrieves the sensitivity label information for the file at the specified URL.

```powershell
Get-PnPFileSensitivityLabel -Url "/sites/Marketing/Shared Documents/Report.pptx"
```

This example retrieves the sensitivity label information for the file at the specified URL.

## PARAMETERS

### -Url
Specifies the URL of the file for which to retrieve the sensitivity label information. Accepts a server relative or a site relative URL. A sequence such as `%20` in the URL is taken literally when a file of that name exists, and is decoded otherwise.

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

