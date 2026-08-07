---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAuthenticationRealm.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAuthenticationRealm
---
  
# Get-PnPAuthenticationRealm

## SYNOPSIS
Returns the authentication realm

## SYNTAX

```powershell
Get-PnPAuthenticationRealm [[-Url] <String>] [-Connection <PnPConnection>] 
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
Get-PnPAuthenticationRealm -Url "https://contoso.sharepoint.com"
```

This will get the authentication realm for https://contoso.sharepoint.com

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
Specifies the URL of the site

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


