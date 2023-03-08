---
Module Name: PnP.PowerShell
title: Request-PnPAccessToken
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Request-PnPAccessToken.html
---
 
# Request-PnPAccessToken

## SYNOPSIS
Requests an OAuth Access token.

## SYNTAX

```powershell
Request-PnPAccessToken [-ClientId <String>] [-Resource <String>] [-Scopes <String[]]>] [-Decoded] [-Credentials <PSCredential>] [-TenantUrl <String>]
```

## DESCRIPTION
Returns an access token using the password grant, using the PnP O365 Management Shell client id by default and the AllSites.FullControl scope by default.

## EXAMPLES

### EXAMPLE 1
```powershell
Request-PnPAccessToken
```

Returns the access token using the default client id and scope.

### EXAMPLE 2
```powershell
Request-PnPAccessToken -ClientId 26e29fec-aa10-4f99-8381-d96cddc650c2
```

Returns the access token using the specified client id and the default scope of AllSites.FullControl

### EXAMPLE 3
```powershell
Request-PnPAccessToken -ClientId 26e29fec-aa10-4f99-8381-d96cddc650c2 -Scopes Group.ReadWrite.All
```

Returns the access token using the specified client id and the specified scope.

### EXAMPLE 4
```powershell
Request-PnPAccessToken -ClientId 26e29fec-aa10-4f99-8381-d96cddc650c2 -Scopes Group.ReadWrite.All, AllSites.FullControl
```

Returns the access token using the specified client id and the specified scopes.

## PARAMETERS

### -ClientId
The Azure Application Client Id to use to retrieve the token. Defaults to the PnP Office 365 Management Shell.

```yaml
Type: String
Parameter Sets: (All)
Aliases: ApplicationId

Required: False
Position: Named
Default value: 31359c7f-bd7e-475c-86db-fdb8c937548e
Accept pipeline input: False
Accept wildcard characters: False
```

### -Credentials
Optional credentials to use when retrieving the access token. If not present you need to connect first with Connect-PnPOnline.

```yaml
Type: PSCredential
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Decoded
Returns the token in a decoded / human-readable manner.

```yaml
Type: SwitchParameter
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

### -TenantUrl
Optional tenant URL to use when retrieving the access token. The Url should be in the shape of https://yourtenant.sharepoint.com. See examples for more info.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AzureEnvironment
The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
Parameter Sets: (All)
Aliases:
Accepted values: Production, PPE, China, Germany, USGovernment, USGovernmentHigh, USGovernmentDoD

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

