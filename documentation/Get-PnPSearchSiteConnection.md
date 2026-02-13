---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchSiteConnection.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSearchSiteConnection
---

# Get-PnPSearchSiteConnection

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site Administrator

Retrieves the external connector site connections available for the currently connected site.

## SYNTAX

```powershell
Get-PnPSearchSiteConnection [-Identity <String>] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet retrieves the external connector site connections configured for the site you are currently connected to. These connections represent external data sources (e.g., web crawlers, custom connectors) that can be used as content sources in search verticals. It uses the Graph Connector Service (GCS) API at gcs.office.com.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchSiteConnection
```

Returns all external connector site connections available for the currently connected site.

### EXAMPLE 2
```powershell
Get-PnPSearchSiteConnection -Identity "techcrunch"
```

Returns the site connection with the specified ID.

### EXAMPLE 3
```powershell
$connections = Get-PnPSearchSiteConnection
$sources = $connections | ForEach-Object {
    [PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch.SearchVerticalContentSource]@{ Id = $_.Id; Name = $_.Name }
}
New-PnPSearchVertical -DisplayName "External Content" -ContentSources $sources
```

Retrieves all site connections and uses them as content sources when creating a new search vertical.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The ID of the site connection to retrieve. If not provided, all connections will be returned.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

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

[Get-PnPSearchExternalConnection](Get-PnPSearchExternalConnection.md) - Retrieves external connections at the tenant level via Microsoft Graph. Returns basic connection info (id, name, state) for all connections including those in draft state. Use `Get-PnPSearchSiteConnection` instead when you need detailed connection configuration (schema, crawl settings, identity configuration) or want to list only published connections available for search verticals.

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
