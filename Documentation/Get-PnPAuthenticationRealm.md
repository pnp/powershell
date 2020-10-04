---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpauthenticationrealm
schema: 2.0.0
title: Get-PnPAuthenticationRealm
---

# Get-PnPAuthenticationRealm

## SYNOPSIS
Returns the authentication realm

## SYNTAX

```
Get-PnPAuthenticationRealm [[-Url] <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Gets the authentication realm for the current web

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAuthenticationRealm
```

This will get the authentication realm for the current connected site

### EXAMPLE 2
```powershell
Get-PnPAuthenticationRealm -Url https://contoso.sharepoint.com
```

This will get the authentication realm for https://contoso.sharepoint.com

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
Specifies the URL of the site

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)