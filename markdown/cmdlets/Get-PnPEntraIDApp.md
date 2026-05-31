---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEntraIDApp.html
schema: 2.0.0
applicable: SharePoint Online
title: Get-PnPEntraIDApp
---

# Get-PnPEntraIDApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.Read.All

Returns Entra ID App registrations.

## SYNTAX

### Identity (Default)
```powershell
Get-PnPEntraIDApp [-Identity <EntraIDAppPipeBind>] [-Connection <PnPConnection>]
```

### Filter
```powershell
Get-PnPEntraIDApp -Filter <string> [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlets returns all app registrations, a specific one or ones matching a provided filter.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEntraIDApp
```

This returns all Entra ID App registrations.

### EXAMPLE 2
```powershell
Get-PnPEntraIDApp -Identity MyApp
```

This returns the Entra ID App registration with the display name as 'MyApp'.

### EXAMPLE 3
```powershell
Get-PnPEntraIDApp -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

This returns the Entra ID App registration with the app id specified or the id specified.

### EXAMPLE 4
```powershell
Get-PnPEntraIDApp -Filter "startswith(description, 'contoso')"
```

This returns the Entra ID App registrations with the description starting with "contoso". This example demonstrates using Advanced Query capabilities (see: https://learn.microsoft.com/graph/aad-advanced-queries?tabs=http#group-properties)

## PARAMETERS

### -Identity
Specify the display name, id or app id.

```yaml
Type: EntraIDAppPipeBind
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
