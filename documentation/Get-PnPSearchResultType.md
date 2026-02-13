---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchResultType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPSearchResultType
---

# Get-PnPSearchResultType

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Retrieves Microsoft Search result types configured for the currently connected site or at the organization level.

## SYNTAX

```powershell
Get-PnPSearchResultType [-Identity <String>] [-Scope <SearchVerticalScope>] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet retrieves the Microsoft Search result types configured for the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. Result types define how search results are displayed using Adaptive Card templates. They can be configured for SharePoint content or external connector content. It uses the Graph Connector Service (GCS) API at gcs.office.com.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchResultType
```

Returns all search result types for the currently connected site.

### EXAMPLE 2
```powershell
Get-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5"
```

Returns the search result type with the specified logical ID.

### EXAMPLE 3
```powershell
Get-PnPSearchResultType -Identity "My Result Type"
```

Returns the search result type with the specified name.

### EXAMPLE 4
```powershell
Get-PnPSearchResultType -Scope Organization
```

Returns all organization-level search result types.

### EXAMPLE 5
```powershell
Get-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" | Select-Object -ExpandProperty Payload | Select-Object -ExpandProperty Rules
```

Shows the rules configured for a specific result type.

### EXAMPLE 6
```powershell
Get-PnPSearchResultType | Where-Object { $_.Payload.IsActive -eq $false } | Remove-PnPSearchResultType
```

Pipes inactive result types directly to `Remove-PnPSearchResultType`. The `LogicalId` property binds automatically to `-Identity`.

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
The logical ID or name of the search result type to retrieve. Logical IDs are in the format `{timestamp}_{randomId}`. If a name is provided, the cmdlet searches all result types for a matching name. If not provided, all result types will be returned.

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
Specifies whether to retrieve site-level or organization-level result types. Defaults to Site.

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

Returns one or more `SearchResultType` objects with the following properties:

| Property | Type | Description |
|----------|------|-------------|
| Path | String | Site path in the format `:GUID:` |
| LogicalId | String | Unique identifier (e.g. `1770839639348_FYXB8XQI5`) |
| LastModifiedDateTime | String | Timestamp of last modification |
| Payload | SearchResultTypePayload | The result type configuration (see below) |

The `Payload` object contains:

| Property | Type | Description |
|----------|------|-------------|
| Name | String | Display name of the result type |
| IsActive | Boolean | Whether the result type is active |
| Priority | Int | Priority order (0 = highest) |
| ContentSourceId | Object | Content source with `ContentSourceApplication`, `Identity`, `SystemId` |
| ContentSourceName | String | Display name of the content source |
| Rules | List | Matching rules (see below) |
| DisplayTemplate | String | Adaptive Card JSON template |
| DisplayProperties | List | Properties used in the display template |

Each rule in `Rules` contains:

| Property | JSON Key | Type | Description |
|----------|----------|------|-------------|
| PropertyName | PN | String | Property to match (e.g. "FileType", "IsListItem") |
| Operator | PO | Object | Operator with `N` code and `JBO` flag |
| Values | PV | List | Values to compare against |

Operator `N` codes: 1=Equals, 2=NotEquals, 3=Contains, 4=DoesNotContain, 5=LessThan, 6=GreaterThan, 7=StartsWith. Use [New-PnPSearchResultTypeRule](New-PnPSearchResultTypeRule.md) to create rules with friendly operator names.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
