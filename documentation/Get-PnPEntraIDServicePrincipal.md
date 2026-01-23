---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADServicePrincipal.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAzureADServicePrincipal
---
  
# Get-PnPAzureADServicePrincipal

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: Any of Application.Read.All, Application.ReadWrite.All, Directory.Read.All, Directory.ReadWrite.All

Gets service principal/application registrations in Azure Active Directory.

## SYNTAX

### All

```powershell
Get-PnPAzureADServicePrincipal [-Filter <string>] [-Connection <PnPConnection>]
```

### By App Id

```powershell
Get-PnPAzureADServicePrincipal -AppId <Guid> [-Connection <PnPConnection>]
```

### By Object Id

```powershell
Get-PnPAzureADServicePrincipal -ObjectId <Guid> [-Connection <PnPConnection>]
```

### By App Name

```powershell
Get-PnPAzureADServicePrincipal -AppName <String> [-Connection <PnPConnection>]
```

### By built in type

```powershell
Get-PnPAzureADServicePrincipal -BuiltInType <ServicePrincipalBuiltInType> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows retrieval of all service principals/app registrations in Azure Active Directory or a specific service principal/app registration based on the AppId, ObjectId or AppName. This will include both application registrations as well as enterprise applications in Azure Active Directory.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAzureADServicePrincipal
```

Retrieves all application registrations and enterprise applications from Azure Active Directory.

### EXAMPLE 2
```powershell
Get-PnPAzureADServicePrincipal -AppId b8c2a8aa-33a0-43f4-a9d3-fe2851c5293e
```

Retrieves the application registration with AppId/ClientId b8c2a8aa-33a0-43f4-a9d3-fe2851c5293e from Azure Active Directory.

### EXAMPLE 3
```powershell
Get-PnPAzureADServicePrincipal -ObjectId 06ca9985-367a-41ba-9c44-b2ed88c19aec
```

Retrieves the application registration with ObjectId 06ca9985-367a-41ba-9c44-b2ed88c19aec from Azure Active Directory.

### EXAMPLE 4
```powershell
Get-PnPAzureADServicePrincipal -AppName "My application"
```

Retrieves the application registration with the name "My application" from Azure Active Directory.

### EXAMPLE 5
```powershell
Get-PnPAzureADServicePrincipal -Filter "startswith(description, 'contoso')"
```

Retrieves the application registration with the description starting with "contoso" from Azure Active Directory. This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/graph/aad-advanced-queries?tabs=http#group-properties).

## PARAMETERS

### -AppId
The guid of the application registration its App Id/Client Id to retrieve.

```yaml
Type: Guid
Parameter Sets: By App Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ObjectId
The guid of the application registration its object Id to retrieve.

```yaml
Type: Guid
Parameter Sets: By Object Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AppName
The name of the application registration to retrieve.

```yaml
Type: String
Parameter Sets: By App Name

Required: True
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

### -Filter
Specify the query to pass to Graph API in $filter.

```yaml
Type: String
Parameter Sets: Filter

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/serviceprincipal-get)