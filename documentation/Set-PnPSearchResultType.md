---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSearchResultType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPSearchResultType
---

# Set-PnPSearchResultType

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Updates a Microsoft Search result type on the currently connected site or at the organization level.

## SYNTAX

### Properties (Default)
```powershell
Set-PnPSearchResultType -Identity <String> [-Scope <SearchVerticalScope>] [-Name <String>] [-Priority <Int32>] [-Validate] [-Verbose] [-Connection <PnPConnection>]
```

### Payload
```powershell
Set-PnPSearchResultType -Identity <String> [-Scope <SearchVerticalScope>] -Payload <SearchResultTypePayload> [-Validate] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet updates a Microsoft Search result type on the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. When using individual parameters (Properties parameter set), the cmdlet retrieves the current configuration, merges in your changes, and performs a full update. For complete control over rules, content source, and display template, use the `-Payload` parameter. It uses the Graph Connector Service (GCS) API at gcs.office.com.

> [!WARNING]
> This cmdlet uses the Graph Connector Service (GCS) API, which is an internal Microsoft API that is not publicly documented or officially supported. It may change without notice.

### Prerequisites

Your Entra app registration must have the `ExternalConnection.ReadWrite.All` delegated permission from the Graph Connector Service (GCS) API. To add this permission using Azure CLI:

```bash
az ad app permission add --id <your-app-id> --api 56c1da01-2129-48f7-9355-af6d59d42766 --api-permissions d44774bd-e26c-43b1-996d-51bb90a9078e=Scope
az ad app permission admin-consent --id <your-app-id>
```

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Name "Updated Name"
```

Updates the name of the specified result type.

### EXAMPLE 2
```powershell
# Get the result type and modify its payload
$rt = Get-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5"
$payload = $rt.Payload

# Change the content source to an external connector
$connection = Get-PnPSearchSiteConnection -Identity "techcrunch"
$payload.ContentSourceId = [PnP.PowerShell.Commands.Model.Graph.MicrosoftSearch.SearchResultTypeContentSource]@{
    ContentSourceApplication = "Connectors"
    Identity                 = $connection.Id
    SystemId                 = $connection.SystemId
}
$payload.ContentSourceName = $connection.Name

# Update the rules using New-PnPSearchResultTypeRule
$payload.Rules = @(
    New-PnPSearchResultTypeRule -PropertyName "IconUrl" -Operator StartsWith -Values "https://"
)
$payload.RuleProperties = @("IconUrl")

Set-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Payload $payload
```

Changes a result type's content source from SharePoint to an external connector with new rules.

### EXAMPLE 4
```powershell
$rt = Get-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5"
$rt.Payload.Rules = @(
    New-PnPSearchResultTypeRule -PropertyName "FileType" -Operator Equals -Values "docx","xlsx","pptx"
    New-PnPSearchResultTypeRule -PropertyName "IsListItem" -Operator Equals -Values "false"
)
$rt.Payload.RuleProperties = @("FileType", "IsListItem")
Set-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Payload $rt.Payload
```

Updates the rules on an existing result type to match Office documents that are not list items.

### EXAMPLE 5
```powershell
$template = @'
{
  "type": "AdaptiveCard",
  "version": "1.3",
  "body": [
    {
      "type": "TextBlock",
      "text": "[${title}](${titleUrl})",
      "weight": "Bolder",
      "size": "Medium",
      "color": "Accent"
    },
    {
      "type": "TextBlock",
      "text": "${description}",
      "wrap": true,
      "maxLines": 3
    }
  ],
  "$schema": "http://adaptivecards.io/schemas/adaptive-card.json"
}
'@
$rt = Get-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5"
$rt.Payload.DisplayTemplate = $template
Set-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Payload $rt.Payload
```

Updates the display template of an existing result type with a custom Adaptive Card layout. Use a here-string (`@'...'@`) for readability.

### EXAMPLE 6
```powershell
Set-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Priority 1
```

Sets the result type to be first in display order.

### EXAMPLE 7
```powershell
Set-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Name "Updated Name" -Scope Organization
```

Updates the name of an organization-level result type.

### EXAMPLE 8
```powershell
$rt = Get-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5"
$rt.Payload.Rules = @(
    New-PnPSearchResultTypeRule -PropertyName "IconUrl" -Operator StartsWith -Values "https://"
)
$rt.Payload.RuleProperties = @("IconUrl")
Set-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5" -Payload $rt.Payload -Validate
```

Updates the rules on a connector-based result type with property validation. The `-Validate` switch checks that `IconUrl` exists in the connector's schema. If a rule property does not exist, the cmdlet throws an error. Invalid display properties produce a warning.

### EXAMPLE 9
```powershell
Get-PnPSearchResultType -Identity "My Result Type" | Set-PnPSearchResultType -Name "Updated Name"
```

Pipes a result type object directly into `Set-PnPSearchResultType`. The `LogicalId` property binds automatically to `-Identity`.

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
The logical ID or name of the search result type to update.

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
Specifies whether to update a site-level or organization-level result type. Defaults to Site.

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

### -Name
The new name for the result type.

```yaml
Type: String
Parameter Sets: Properties
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Priority
The priority (display order) for the result type. Priorities must be unique — when you set a result type to a priority that is already in use, the existing result type at that position shifts accordingly.

```yaml
Type: Int32
Parameter Sets: Properties
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Payload
A full SearchResultTypePayload object to replace the entire result type configuration. The payload is sent directly via PUT without merging with the existing configuration.

```yaml
Type: SearchResultTypePayload
Parameter Sets: Payload
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Validate
Validates rule and display property names before updating the result type. For **external connector** content sources, the connector's schema is fetched from the API: invalid rule properties cause an error (they would never match), invalid display properties produce a warning. For **SharePoint** content sources, property names are checked against the list of known default managed properties: unrecognized properties produce a warning (not an error) because customers may have custom managed properties, aliases, or autogenerated properties not in the default set. Standard search display properties (title, titleUrl, modifiedBy, modifiedTime, description) are always considered valid. Additionally, the display template is validated to be valid JSON with Adaptive Card version 1.3, which is the version supported by Microsoft Search.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
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
