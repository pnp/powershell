---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchVertical.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPSearchVertical
---

# Set-PnPSearchVertical

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Updates a Microsoft Search vertical on the currently connected site or at the organization level.

## SYNTAX

### Properties (Default)
```powershell
Set-PnPSearchVertical -Identity <String> [-Scope <SearchVerticalScope>] [-DisplayName <String>] [-Enabled <Boolean>] [-QueryTemplate <String>] [-IncludeConnectorResults <Boolean>] [-Verbose] [-Connection <PnPConnection>]
```

### Payload
```powershell
Set-PnPSearchVertical -Identity <String> [-Scope <SearchVerticalScope>] -Payload <SearchVerticalPayload> [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet updates a Microsoft Search vertical on the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. When using individual parameters (Properties parameter set), the cmdlet retrieves the current vertical configuration, merges in your changes, and performs a full update. For complete control, use the `-Payload` parameter to provide the full payload object. It uses the Graph Connector Service (GCS) API at gcs.office.com.

> [!WARNING]
> This cmdlet uses the Graph Connector Service (GCS) API, which is an internal Microsoft API that is not publicly documented or officially supported. It may change without notice.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

> [!NOTE]
> This cmdlet requires a **delegated (interactive)** connection. App-only (certificate-based) connections are not supported by the GCS API and will result in a 403 Forbidden error.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSearchVertical -Identity "1610383262385_H0RPZO96M" -DisplayName "Updated Name"
```

Updates the display name of the specified custom search vertical.

### EXAMPLE 2
```powershell
Set-PnPSearchVertical -Identity "1610383262385_H0RPZO96M" -Enabled $false
```

Disables the specified search vertical.

### EXAMPLE 3
```powershell
$vertical = Get-PnPSearchVertical -Identity "1610383262385_H0RPZO96M"
$vertical.Payload.DisplayName = "New Name"
$vertical.Payload.State = 1
Set-PnPSearchVertical -Identity "1610383262385_H0RPZO96M" -Payload $vertical.Payload
```

Retrieves a vertical, modifies the payload directly, and sends the full payload update.

### EXAMPLE 4
```powershell
Set-PnPSearchVertical -Identity "1644258966832_MFDMSIXCG" -DisplayName "Updated Name" -Scope Organization
```

Updates the display name of an organization-level search vertical.

### EXAMPLE 5
```powershell
# Get the vertical and its payload
$vertical = Get-PnPSearchVertical -Identity "1610383262385_H0RPZO96M"
$payload = $vertical.Payload

# Create a FileType filter/refiner
$refiner = [PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch.SearchVerticalRefiner]@{
    Id          = "FileType"
    DisplayName = "File Type"
    State       = 1
    Category    = 0
    Layout      = [PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch.SearchVerticalRefinerLayout]@{
        FieldName          = "FileType"
        Type               = 0
        DisplayInterface   = 0
        ManualEntryEnabled = $false
        ShowCount          = $true
        Values             = @()
        MappedProperties   = @("FileType")
    }
}

# Add the refiner to the payload and link it to the first entity
$payload.Refiners.Add($refiner)
$payload.Entities[0].RefinerIds.Add("FileType")

# Push the update
Set-PnPSearchVertical -Identity "1610383262385_H0RPZO96M" -Payload $payload
```

Adds a "File Type" filter to a custom search vertical. Filters (refiners) allow users to narrow down search results within the vertical. The refiner must be added to both the `Refiners` list and to the entity's `RefinerIds` to be active. To discover the exact refiner format used in your environment, inspect an existing vertical that has filters: `Get-PnPSearchVertical -Identity "SITEFILES" | Select-Object -ExpandProperty Payload | Select-Object -ExpandProperty Refiners | ConvertTo-Json -Depth 5`.

### EXAMPLE 6
```powershell
Get-PnPSearchVertical -Identity "1610383262385_H0RPZO96M" | Set-PnPSearchVertical -DisplayName "Renamed Vertical"
```

Pipes a vertical object directly into `Set-PnPSearchVertical`. The `LogicalId` property binds automatically to `-Identity`.

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
The logical ID of the search vertical to update. For site scope, built-in IDs include SITEALL, SITEFILES, SITESITES, SITENEWS, and SITEIMAGES. For organization scope, built-in IDs include ALL, FILES, SITES, NEWS, PEOPLE, IMAGES, MESSAGES, and MICROSOFTPOWERBI. Custom verticals have either a user-chosen ID or an auto-generated ID in the format `{timestamp}_{randomId}`.

```yaml
Type: String
Parameter Sets: (All)
Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue, ByPropertyName)
Accept wildcard characters: False
Aliases: LogicalId
```

### -Scope
Specifies whether to update a site-level or organization-level vertical. Defaults to Site.

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
The new display name for the search vertical.

```yaml
Type: String
Parameter Sets: Properties
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enabled
Sets whether the search vertical is enabled (`$true`) or disabled (`$false`).

```yaml
Type: Boolean
Parameter Sets: Properties
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -QueryTemplate
A KQL query template to filter results in the vertical. The `{searchTerms}` placeholder is automatically prepended if not included. For example, `-QueryTemplate "IsDocument:1"` becomes `{searchTerms} IsDocument:1`.

```yaml
Type: String
Parameter Sets: Properties
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeConnectorResults
Whether to include connector results in the vertical. This setting can only be used on built-in verticals: `SITEALL` at site scope or `ALL` at organization scope. Using it on custom verticals will result in an error.

```yaml
Type: Boolean
Parameter Sets: Properties
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Payload
A full SearchVerticalPayload object to replace the entire vertical configuration. The payload is sent directly via PUT without merging with the existing configuration.

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
