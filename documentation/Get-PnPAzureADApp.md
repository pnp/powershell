---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADApp.html
schema: 2.0.0
applicable: SharePoint Online
title: Get-PnPAzureADApp
---

# Get-PnPAzureADApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.Read.All

Returns Azure AD App registrations.

## SYNTAX

### Identity (Default)
```powershell
Get-PnPAzureADApp [-Identity <AzureADAppPipeBind>] [-Connection <PnPConnection>]
```

### Filter
```powershell
Get-PnPAzureADApp -Filter <string> [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlets returns all app registrations, a specific one or ones matching a provided filter.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAzureADApp
```

This returns all Azure AD App registrations.

### EXAMPLE 2
```powershell
Get-PnPAzureADApp -Identity MyApp
```

This returns the Azure AD App registration with the display name as 'MyApp'.

### EXAMPLE 3
```powershell
Get-PnPAzureADApp -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

This returns the Azure AD App registration with the app id specified or the id specified.

### EXAMPLE 4
```powershell
Get-PnPAzureADApp -Filter "startswith(description, 'contoso')"
```

This returns the Azure AD App registrations with the description starting with "contoso". This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/graph/aad-advanced-queries?tabs=http#group-properties)

## PARAMETERS

### -Identity
Specify the display name, id or app id.

```yaml
Type: AzureADAppPipeBind
Parameter Sets: Identity
Aliases:

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
