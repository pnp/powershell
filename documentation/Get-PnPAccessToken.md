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

### Graph Token

```powershell
Get-PnPAccessToken [-ResourceTypeName] [-Decoded] [-Scopes] [-Connection <PnPConnection>]
```

### Specific resource by type

```powershell
Get-PnPAccessToken -ResourceTypeName <ResourceTypeName> [-Decoded] [-Scopes] [-Connection <PnPConnection>]
```

### Specific resource by URL

```powershell
Get-PnPAccessToken -ResourceUrl <String> [-Decoded] [-Scopes] [-Connection <PnPConnection>]
```

### List Permission Scopes in current access token

```powershell
Get-PnPAccessToken -ListPermissionScopes [-ResourceTypeName <String>]
```


## DESCRIPTION
Returns the OAuth 2.0 Access Token.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAccessToken
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API

### EXAMPLE 2
```powershell
Get-PnPAccessToken -Decoded
```

Gets the OAuth 2.0 Access Token to consume the Microsoft Graph API and shows the token with its content decoded

### EXAMPLE 3
```powershell
Get-PnPAccessToken -ResourceTypeName SharePoint
```

Gets the OAuth 2.0 Access Token to consume the SharePoint APIs and perform CSOM operations.

### EXAMPLE 4
```powershell
Get-PnPAccessToken -ResourceTypeName ARM
```

Gets the OAuth 2.0 Access Token to consume the Azure Resource Manager APIs and perform related operations. In PnP, you can use them in cmdlets related to Flow and PowerPlatform etc.

### EXAMPLE 5
```powershell
Get-PnPAccessToken -ResourceUrl "https://management.azure.com/.default"
```

Gets the OAuth 2.0 Access Token to consume the SharePoint APIs and perform CSOM operations.

### EXAMPLE 6
```powershell
Get-PnPAccessToken -ListPermissionScopes
```

Lists the current permission scopes for the Microsoft Graph API on the access token. Specify -ResourceTypeName to list permissions for other resource types, like SharePoint.

## PARAMETERS

### -ResourceTypeName
Specify the Resource Type for which you want the access token. If not specified, it will by default return a Microsoft Graph access token.

```yaml
Type: ResourceTypeName
Parameter Sets: Resource Type Name, Resource Type Name (decoded)
Accepted values: Graph, SharePoint, ARM

Required: False
Position: Named
Default value: Graph
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceUrl
Specify the Resource URL for which you want the access token, i.e. https://graph.microsoft.com/.default. If not specified, it will by default return a Microsoft Graph access token.

```yaml
Type: String
Parameter Sets: Resource Url, Resource Url (decoded)

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
Parameter Sets: Resource Type Name (decoded), Resource Url (decoded)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Scopes
The scopes to retrieve the token for. Defaults to AllSites.FullControl

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListPermissionScopes
If specified the current permission scopes on the access token will be listed

```yaml
Type: SwitchParameter
Parameters Set: List Permission Scopes
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)