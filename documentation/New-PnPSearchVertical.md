---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSearchVertical.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPSearchVertical
---

# New-PnPSearchVertical

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Creates a new Microsoft Search vertical on the currently connected site or at the organization level.

## SYNTAX

### Default (Default)
```powershell
New-PnPSearchVertical -DisplayName <String> [-Identity <String>] [-Scope <SearchVerticalScope>] [-Enabled <Boolean>] [-QueryTemplate <String>] [-ContentSources <Object[]>] [-IncludeConnectorResults <Boolean>] [-Verbose] [-Connection <PnPConnection>]
```

### Payload
```powershell
New-PnPSearchVertical [-Identity <String>] [-Scope <SearchVerticalScope>] -Payload <SearchVerticalPayload> [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet creates a new custom Microsoft Search vertical on the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. By default it creates a SharePoint content vertical. Use `-ContentSources` to create verticals for external connector content — pass connection IDs (strings), `SearchSiteConnection` objects from `Get-PnPSearchSiteConnection`, or `"SharePoint"` for SharePoint content. The entity type is automatically inferred from the content sources (`File` for SharePoint, `External` for connectors). For full control over the vertical configuration, use the `-Payload` parameter. It uses the Graph Connector Service (GCS) API at gcs.office.com.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPSearchVertical -DisplayName "Contoso Tickets"
```

Creates a new disabled search vertical named "Contoso Tickets" with default SharePoint content.

### EXAMPLE 2
```powershell
New-PnPSearchVertical -DisplayName "Contoso Tickets" -Enabled $true
```

Creates a new enabled search vertical named "Contoso Tickets" with SharePoint content.

### EXAMPLE 3
```powershell
New-PnPSearchVertical -DisplayName "Contoso Tickets" -Identity "tickets" -Enabled $true
```

Creates a vertical with a custom logical ID. The logical ID becomes part of the search URL (e.g. `/search.aspx/tickets`), similar to how built-in verticals have readable URLs like `/search.aspx/All` or `/search.aspx/Files`. If not specified, a generated ID like `1610383262385_H0RPZO96M` is used instead.

### EXAMPLE 4
```powershell
New-PnPSearchVertical -DisplayName "External Content" -ContentSources "techcrunch"
```

Creates a new search vertical for a single external connector, specified by connection ID. The entity type is automatically set to External. Use `Get-PnPSearchSiteConnection` to discover available connection IDs.

### EXAMPLE 5
```powershell
$conn = Get-PnPSearchSiteConnection -Identity "techcrunch"
New-PnPSearchVertical -DisplayName "External Content" -ContentSources $conn -Enabled $true
```

Creates a new enabled search vertical by passing a `SearchSiteConnection` object from `Get-PnPSearchSiteConnection`.

### EXAMPLE 6
```powershell
New-PnPSearchVertical -DisplayName "External Content" -ContentSources "techcrunch", "contosowiki"
```

Creates a new search vertical that surfaces content from multiple external connectors.

### EXAMPLE 7
```powershell
New-PnPSearchVertical -DisplayName "SharePoint Docs" -ContentSources "SharePoint" -Enabled $true
```

Creates a new enabled SharePoint content vertical. Passing `"SharePoint"` explicitly is equivalent to omitting `-ContentSources` (which defaults to SharePoint).

### EXAMPLE 8
```powershell
$payload = Get-PnPSearchVertical -Identity "1610383262385_H0RPZO96M" | Select-Object -ExpandProperty Payload
$payload.DisplayName = "Cloned Vertical"
New-PnPSearchVertical -Payload $payload
```

Creates a new search vertical by cloning the payload from an existing vertical.

### EXAMPLE 9
```powershell
New-PnPSearchVertical -DisplayName "Org Vertical" -Enabled $true -Scope Organization
```

Creates a new enabled organization-level search vertical.

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

### -Scope
Specifies whether to create a site-level or organization-level vertical. Defaults to Site.

```yaml
Type: SearchVerticalScope
Parameter Sets: (All)
Accepted values: Site, Organization
Required: False
Position: Named
Default value: Site
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name for the new search vertical.

```yaml
Type: String
Parameter Sets: Default
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
A custom logical ID for the vertical. The logical ID becomes the URL segment in the search page (e.g. specifying `"tickets"` results in `/search.aspx/tickets`), similar to how built-in verticals have readable IDs like `SITEALL`, `SITEFILES`, etc. If not specified, a unique ID is generated automatically in the format `{timestamp}_{randomId}`.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enabled
Whether the vertical should be created in an enabled state. Defaults to `$false` (disabled).

```yaml
Type: Boolean
Parameter Sets: Default
Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -QueryTemplate
A KQL query template to filter results in the vertical. The `{searchTerms}` placeholder is automatically prepended if not included. For example, `-QueryTemplate "IsDocument:1"` becomes `{searchTerms} IsDocument:1`.

```yaml
Type: String
Parameter Sets: Default
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentSources
The content sources for the vertical. Accepts one or more of the following: `"SharePoint"` for SharePoint content, a connection ID string for an external connector, or a `SearchSiteConnection` object from [Get-PnPSearchSiteConnection](Get-PnPSearchSiteConnection.md). If not specified, defaults to SharePoint. The entity type is automatically inferred: `File` for SharePoint, `External` for connectors.

```yaml
Type: Object[]
Parameter Sets: Default
Required: False
Position: Named
Default value: SharePoint
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeConnectorResults
Whether to include connector results in the vertical. This parameter only applies to built-in verticals (SITEALL at site scope, ALL at organization scope). For custom verticals created with this cmdlet, a warning is shown and the value is ignored. Use `Set-PnPSearchVertical` to modify built-in verticals.

```yaml
Type: Boolean
Parameter Sets: Default
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Payload
A full SearchVerticalPayload object for complete control over the vertical configuration. Use this parameter set for advanced scenarios where the simple parameters are not sufficient.

```yaml
Type: SearchVerticalPayload
Parameter Sets: Payload
Required: True
Position: Named
Default value: None
Accept pipeline input: False
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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
