---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantDeletedSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantDeletedSite
---

# Get-PnPTenantDeletedSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Fetches the site collections from the tenant recycle bin.

## SYNTAX

### Default (Default)

```
Get-PnPTenantDeletedSite [-Identity] <String> [-Limit] [-IncludePersonalSite]
 [-IncludeOnlyPersonalSite] [-Detailed] [-Verbose] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Fetches the site collections which are listed in your tenant's recycle bin.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantDeletedSite
```

This will fetch basic information on site collections located in the recycle bin.

### EXAMPLE 2

```powershell
Get-PnPTenantDeletedSite -Detailed
```

This will fetch detailed information on site collections located in the recycle bin.

### EXAMPLE 3

```powershell
Get-PnPTenantDeletedSite -Identity "https://tenant.sharepoint.com/sites/contoso"
```

This will fetch basic information on the site collection with the url 'https://tenant.sharepoint.com/sites/contoso' from the recycle bin.

### EXAMPLE 4

```powershell
Get-PnPTenantDeletedSite -IncludePersonalSite
```

This will fetch the site collections from the recycle bin including the personal sites and display its properties.

### EXAMPLE 5

```powershell
Get-PnPTenantDeletedSite -IncludeOnlyPersonalSite
```

This will fetch the site collections from the recycle bin which are personal sites and display its properties.

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

When specified, detailed information will be returned on the site collections. This will take longer to execute.

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

### -Identity

Specifies the full URL of the site collection that needs to be restored.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludeOnlyPersonalSite

If specified the task will only retrieve the personal sites from the recycle bin.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (ParameterSetPersonalSitesOnly)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -IncludePersonalSite

If specified the task will also retrieve the personal sites from the recycle bin.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (ParameterSetAllSites)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Limit

Limit of the number of site collections to be retrieved from the recycle bin. Default is 200.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
