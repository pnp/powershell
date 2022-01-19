---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAccessToken.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAccessToken
---
  
# Get-PnPAccessToken

## SYNOPSIS
Returns the current Microsoft Graph OAuth Access token. 
If a Resource Type Name or Resource URL is specified, it will fetch the access token of the specified resource.

## SYNTAX

```powershell
Get-PnPAccessToken [-ResourceTypeName] [-ResourceUrl] [-Decoded] [<CommonParameters>]
```

## DESCRIPTION
Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API. Doesn't work with all Connect-PnPOnline options. To retrieve the SharePoint Online access token, instead use `Get-PnPAppAuthAccessToken`.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAccessToken
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API

### EXAMPLE 2
```powershell
Get-PnPAccessToken -ResourceTypeName SharePoint
```

Gets the OAuth 2.0 Access Token to consume the SharePoint APIs and perform CSOM operations.

### EXAMPLE 3
```powershell
Get-PnPAccessToken -ResourceTypeName ARM
```

Gets the OAuth 2.0 Access Token to consume the Azure Resource Manager APIs and perform related operations. In PnP, you can use them in cmdlets related to Flow and PowerPlatform etc.

### EXAMPLE 4
```powershell
Get-PnPAccessToken -ResourceUrl "https://management.azure.com/.default"
```

Gets the OAuth 2.0 Access Token to consume the SharePoint APIs and perform CSOM operations.

## PARAMETERS

### -ResourceTypeName
Specify the Resource Type for which you want the access token.
If not specified, it will by default return Microsoft Graph access token.

```yaml
Type: ResourceTypeName
Parameter Sets: Resource Type Name
Accepted values: Graph, SharePoint, ARM

Required: False
Position: Named
Default value: Graph
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceUrl
Specify the Resource URL for which you want the access token.
If not specified, it will by default return Microsoft Graph access token.

```yaml
Type: String
Parameter Sets: Resource Url

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Decoded
Returns the details from the access token in a decoded manner

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
