---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchVertical.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSearchVertical
---

# Get-PnPSearchVertical

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Retrieves Microsoft Search verticals configured for the currently connected site or at the organization level.

## SYNTAX

```powershell
Get-PnPSearchVertical [-Identity <String>] [-Scope <SearchVerticalScope>] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet retrieves the Microsoft Search verticals configured for the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. It returns both built-in (out-of-box) verticals such as All, Files, Sites, News, and Images, as well as any custom verticals. It uses the Graph Connector Service (GCS) API at gcs.office.com.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchVertical
```

Returns all search verticals (both built-in and custom) for the currently connected site.

### EXAMPLE 2
```powershell
Get-PnPSearchVertical -Identity "SITEALL"
```

Returns the built-in "All" search vertical for the currently connected site.

### EXAMPLE 3
```powershell
Get-PnPSearchVertical -Identity "1610383262385_H0RPZO96M"
```

Returns the custom search vertical with the specified logical ID.

### EXAMPLE 4
```powershell
Get-PnPSearchVertical -Scope Organization
```

Returns all organization-level search verticals (configured in the Microsoft 365 admin center).

### EXAMPLE 5
```powershell
Get-PnPSearchVertical -Identity "ALL" -Scope Organization
```

Returns the built-in "All" organization-level search vertical.

### EXAMPLE 6
```powershell
Get-PnPSearchVertical | Where-Object { $_.Payload.VerticalType -eq 1 } | Set-PnPSearchVertical -Enabled $false
```

Disables all custom verticals on the current site. The `LogicalId` property from each piped object binds automatically to the `-Identity` parameter on `Set-PnPSearchVertical`.

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
The logical ID of the search vertical to retrieve. For site scope, built-in IDs include SITEALL, SITEFILES, SITESITES, SITENEWS, and SITEIMAGES. For organization scope, built-in IDs include ALL, FILES, SITES, NEWS, PEOPLE, IMAGES, MESSAGES, and MICROSOFTPOWERBI. Custom verticals have either a user-chosen ID or an auto-generated ID in the format `{timestamp}_{randomId}`. If not provided, all verticals will be returned.

```yaml
Type: String
Parameter Sets: (All)
Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue, ByPropertyName)
Accept wildcard characters: False
Aliases: LogicalId
```

### -Scope
Specifies whether to retrieve site-level or organization-level verticals. Defaults to Site.

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

## OUTPUT

Returns one or more `SearchVertical` objects with the following properties:

| Property | Type | Description |
|----------|------|-------------|
| Path | String | Site path in the format `:GUID:` |
| LogicalId | String | Unique identifier (e.g. `SITEALL`, `1610383262385_H0RPZO96M`) |
| LastModifiedDateTime | String | ISO 8601 timestamp of last modification |
| Payload | SearchVerticalPayload | The vertical configuration (see below) |

The `Payload` object contains:

| Property | Type | Description |
|----------|------|-------------|
| DisplayName | String | Display name of the vertical |
| State | Int | 0 = disabled, 1 = enabled |
| VerticalType | Int | 0 = built-in, 1 = custom |
| QueryTemplate | String | KQL query template |
| TemplateType | String | All, File, Sites, News, People, Images, Messages, Videos, Power BI, or Custom |
| Entities | List | Content source entities (EntityType + ContentSources) |
| Scope | Int | 0 = organization, 1 = site |
| IncludeConnectorResults | Boolean | Whether connector results are included |

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
