---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantRestrictedSearchAllowedList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTenantRestrictedSearchAllowedList
---

# Add-PnPTenantRestrictedSearchAllowedList

## SYNOPSIS

Adds site URLs to the allowed list when Restricted SharePoint Search is enabled. The URLs can be provided as a string array or read from a CSV file.

## SYNTAX

### Default (Default)

```
Add-PnPTenantRestrictedSearchAllowedList [-SitesListFileUrl <String>] [-SitesList <String[]>]
 [-ContainsHeaders <SwitchParameter>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Adds site URLs to the allowed list when Restricted SharePoint Search is enabled. The URLs can be provided directly as a string array or read from a CSV file. At present, a maximum of 100 sites can be added to the allowed list.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTenantRestrictedSearchAllowedList -SitesListFileUrl "C:\temp\sitelist.csv" -ContainsHeader
```

Adds site URLs to the allowed list from a CSV file. The first line, which is assumed to be a header, is skipped.

Sample CSV file content with Header

SiteUrl
https://contoso.sharepoint.com/sites/Company311
https://contoso.sharepoint.com/sites/contosoportal

### EXAMPLE 2

```powershell
Add-PnPTenantRestrictedSearchAllowedList -SitesListFileUrl "C:\temp\sitelist.csv"
```

Adds site URLs to the allowed list from a CSV file.

Sample CSV file content without Header

https://contoso.sharepoint.com/sites/Company311
https://contoso.sharepoint.com/sites/contosoportal

### EXAMPLE 3

```powershell
Add-PnPTenantRestrictedSearchAllowedList -SitesList @("https://contoso.sharepoint.com/sites/Company311","https://contoso.sharepoint.com/sites/contosoportal")
```

Adds the specified sites to the allowed list.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Containsheader

If specified, this switch skips the first line from the CSV file, which is assumed to be a header.

```yaml
Type: SwitchParamter
DefaultValue: False
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: File
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SitesList

Specifies a collection of sites to add to the allowed list.

```yaml
Type: String[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: SiteList
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SitesListFileUrl

Specifies the path of the CSV file that contains a list of site URLs to be added to the allowed list when the tenant is set to Restricted Tenant Search Mode.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: File
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [How does Restricted SharePoint Search work?](https://learn.microsoft.com/sharepoint/restricted-sharepoint-search)
- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
