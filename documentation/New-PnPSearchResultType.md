---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPSearchResultType.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPSearchResultType
---

# New-PnPSearchResultType

## SYNOPSIS

**Required Permissions**

  * [Graph Connector Service (GCS) API](#prerequisites)
  * Site scope: Site Administrator
  * Organization scope: Search Administrator or Global Administrator

Creates a new Microsoft Search result type on the currently connected site or at the organization level.

## SYNTAX

### Default (Default)
```powershell
New-PnPSearchResultType -Name <String> [-Scope <SearchVerticalScope>] [-Rules <SearchResultTypeRule[]>] [-DisplayTemplate <String>] [-DisplayProperties <String[]>] [-ContentSource <Object>] [-Validate] [-Verbose] [-Connection <PnPConnection>]
```

### Payload
```powershell
New-PnPSearchResultType [-Scope <SearchVerticalScope>] -Payload <SearchResultTypePayload> [-Validate] [-Verbose] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet creates a new Microsoft Search result type on the site you are currently connected to, or at the organization (tenant) level when using `-Scope Organization`. Result types define how search results matching certain rules are displayed using Adaptive Card templates. By default it creates a SharePoint content result type with the default display template. Use `-ContentSource` for external connector content, `-Rules` for matching conditions, and `-DisplayTemplate` for custom Adaptive Card layouts. For full control, use the `-Payload` parameter. It uses the Graph Connector Service (GCS) API at gcs.office.com.

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
New-PnPSearchResultType -Name "PDF Documents"
```

Creates a new result type for SharePoint content with the default Adaptive Card template.

### EXAMPLE 2
```powershell
$rule = New-PnPSearchResultTypeRule -PropertyName "FileType" -Operator Equals -Values "pdf"
New-PnPSearchResultType -Name "PDF Documents" -Rules $rule
```

Creates a result type that matches PDF files using an Equals rule.

### EXAMPLE 3
```powershell
$rules = @(
    New-PnPSearchResultTypeRule -PropertyName "FileType" -Operator Equals -Values "docx","xlsx","pptx"
    New-PnPSearchResultTypeRule -PropertyName "IsListItem" -Operator Equals -Values "false"
)
New-PnPSearchResultType -Name "Office Documents (not list items)" -Rules $rules
```

Creates a result type with multiple rules. All rules must match for a result to use this result type.

### EXAMPLE 4
```powershell
New-PnPSearchResultType -Name "TechCrunch Articles" -ContentSource "techcrunch"
```

Creates a result type for an external connector content source. The connection ID is resolved automatically to retrieve the required system details.

### EXAMPLE 5
```powershell
$connection = Get-PnPSearchSiteConnection -Identity "techcrunch"
New-PnPSearchResultType -Name "TechCrunch Articles" -ContentSource $connection
```

Creates a result type by passing a site connection object directly.

### EXAMPLE 6
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
New-PnPSearchResultType -Name "Simple Layout" -DisplayTemplate $template
```

Creates a result type with a custom Adaptive Card display template. Use a here-string (`@'...'@`) for readability. Template placeholders like `${title}` are replaced with search result properties at render time.

### EXAMPLE 7
```powershell
New-PnPSearchResultType -Name "Org Result Type" -Scope Organization
```

Creates a new organization-level result type.

### EXAMPLE 8
```powershell
# Clone an existing result type
$existing = Get-PnPSearchResultType -Identity "1770839639348_FYXB8XQI5"
$existing.Payload.Name = "Cloned Result Type"
New-PnPSearchResultType -Payload $existing.Payload
```

Creates a new result type by cloning the payload from an existing one.

### EXAMPLE 9
```powershell
$rule = New-PnPSearchResultTypeRule -PropertyName "IconUrl" -Operator StartsWith -Values "https://"
New-PnPSearchResultType -Name "Connector Articles" -ContentSource "techcrunch" -Rules $rule -Validate
```

Creates a result type for an external connector with property validation. The `-Validate` switch fetches the connector's schema and verifies that `IconUrl` exists as a property. If the property does not exist, the cmdlet throws an error listing the available connector properties. Invalid display properties produce a warning instead.

### EXAMPLE 10
```powershell
$rule = New-PnPSearchResultTypeRule -PropertyName "FyleType" -Operator Equals -Values "pdf"
New-PnPSearchResultType -Name "PDF Documents" -Rules $rule -Validate
```

Creates a SharePoint result type with property validation. The `-Validate` switch checks that `FyleType` is a known SharePoint managed property. Since it is misspelled (should be `FileType`), a warning is displayed. The result type is still created because custom managed properties may exist that are not in the default set.

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
Specifies whether to create a site-level or organization-level result type. Defaults to Site.

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
The display name for the new result type.

```yaml
Type: String
Parameter Sets: Default
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Rules
An array of SearchResultTypeRule objects defining the matching conditions. Use [New-PnPSearchResultTypeRule](New-PnPSearchResultTypeRule.md) to create rule objects. If not specified, no rules are applied.

```yaml
Type: SearchResultTypeRule[]
Parameter Sets: Default
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayTemplate
An Adaptive Card JSON string defining how matching results are displayed. If not specified, a default template is used.

```yaml
Type: String
Parameter Sets: Default
Required: False
Position: Named
Default value: Default Adaptive Card template
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayProperties
An array of property names used in the display template. If not specified, defaults to title, titleUrl, modifiedBy, modifiedTime, description.

```yaml
Type: String[]
Parameter Sets: Default
Required: False
Position: Named
Default value: title, titleUrl, modifiedBy, modifiedTime, description
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContentSource
The content source for the result type. Defaults to SharePoint when not specified. For external connectors, pass either a connection ID string (e.g., `"techcrunch"`) or a `SearchSiteConnection` object from `Get-PnPSearchSiteConnection`. When a string is provided, the connection is resolved automatically to retrieve the required system details.

```yaml
Type: Object
Parameter Sets: Default
Required: False
Position: Named
Default value: SharePoint
Accept pipeline input: False
Accept wildcard characters: False
```

### -Payload
A full SearchResultTypePayload object for complete control over the result type configuration.

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
Validates rule and display property names before creating the result type. For **external connector** content sources, the connector's schema is fetched from the API: invalid rule properties cause an error (they would never match), invalid display properties produce a warning. For **SharePoint** content sources, property names are checked against the list of known default managed properties: unrecognized properties produce a warning (not an error) because customers may have custom managed properties, aliases, or autogenerated properties not in the default set. Standard search display properties (title, titleUrl, modifiedBy, modifiedTime, description) are always considered valid. Additionally, the display template is validated to be valid JSON with Adaptive Card version 1.3, which is the version supported by Microsoft Search.

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
