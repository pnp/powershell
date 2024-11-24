---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantSite
---

# Get-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves site collection information

## SYNTAX

### By Site

```
Get-PnPTenantSite [-Identity] <string> [-Detailed] [-DisableSharingForNonOwnersStatus]
 [-Connection <PnPConnection>]
```

### All Sites

```
Get-PnPTenantSite [-Template <string>] [-Detailed] [-IncludeOneDriveSites]
 [-GroupIdDefined <Boolean>] [-Filter <string>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows for retrieval of site collection information through the SharePoint Online tenant site. It requires having SharePoint Online administrator access.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantSite
```

Returns all site collections except the OneDrive for Business sites with basic information on them

### EXAMPLE 2

```powershell
Get-PnPTenantSite -Detailed
```

Returns all site collections except the OneDrive for Business sites with the full details on them

### EXAMPLE 3

```powershell
Get-PnPTenantSite -IncludeOneDriveSites
```

Returns all site collections including all OneDrive for Business sites

### EXAMPLE 4

```powershell
Get-PnPTenantSite -IncludeOneDriveSites -Filter "Url -like '-my.sharepoint.com/personal/'"
```

Returns only OneDrive for Business site collections

### EXAMPLE 5

```powershell
Get-PnPTenantSite -Identity "http://tenant.sharepoint.com/sites/projects"
```

Returns information of the site collection with the provided url

### EXAMPLE 6

```powershell
Get-PnPTenantSite -Identity 7e8a6f56-92fe-4b22-9364-41799e579e8a
```

Returns information of the site collection with the provided Id

### EXAMPLE 7

```powershell
Get-PnPTenantSite -Template SITEPAGEPUBLISHING#0
```

Returns all Communication site collections

### EXAMPLE 8

```powershell
Get-PnPTenantSite -Filter "Url -like 'sales'"
```

Returns all site collections having 'sales' in their url

### EXAMPLE 9

```powershell
Get-PnPTenantSite -GroupIdDefined $true
```

Returns all site collections which have an underlying Microsoft 365 Group

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

### -Detailed

By default, not all returned attributes are populated. This switch populates all attributes. It can take several seconds to run. Without this, some attributes will show default values that may not be correct.

```yaml
Type: SwitchParameter
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

### -DisableSharingForNonOwnersStatus

This parameter will include the status for non owners sharing on the returned object. By default the value for this property is null.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Site
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Filter

Specifies the script block of the server-side filter to apply. See https://learn.microsoft.com/powershell/module/sharepoint-online/get-sposite?view=sharepoint-ps#:~:text=SharePoint%20Online-,%2DFilter,-Specifies%20the%20script.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All sites
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -GroupIdDefined

If specified allows you to filter on sites that have an underlying Microsoft 365 group defined.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All sites
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The URL or Id of the site collection. Requesting a site collection by its Id only works for modern SharePoint Online site collections.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Url
ParameterSets:
- Name: By Site
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeOneDriveSites

By default, the OneDrives are not returned. This switch includes all OneDrives.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All Sites
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Template

By default, all sites will be returned. Specify a template value alike "STS#0" here to filter on the template

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: All Sites
  Position: Named
  IsRequired: false
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

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
