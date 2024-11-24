---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPMasterPage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPMasterPage
---

# Set-PnPMasterPage

## SYNOPSIS

Set the masterpage.

## SYNTAX

### Server Relative

```
Set-PnPMasterPage [-MasterPageServerRelativeUrl <String>]
 [-CustomMasterPageServerRelativeUrl <String>] [-Connection <PnPConnection>]
```

### Site Relative

```
Set-PnPMasterPage [-MasterPageSiteRelativeUrl <String>] [-CustomMasterPageSiteRelativeUrl <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets the default master page of the current web.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPMasterPage -MasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master
```

Sets the master page based on a server relative URL.

### EXAMPLE 2

```powershell
Set-PnPMasterPage -MasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master -CustomMasterPageServerRelativeUrl /sites/projects/_catalogs/masterpage/oslo.master
```

Sets the master page and custom master page based on a server relative URL.

### EXAMPLE 3

```powershell
Set-PnPMasterPage -MasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master
```

Sets the master page based on a site relative URL.

### EXAMPLE 4

```powershell
Set-PnPMasterPage -MasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master -CustomMasterPageSiteRelativeUrl _catalogs/masterpage/oslo.master
```

Sets the master page and custom master page based on a site relative URL.

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

### -CustomMasterPageServerRelativeUrl

Specifies the custom Master page URL based on the server relative URL.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- CustomMasterPageUrl
ParameterSets:
- Name: Server Relative
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CustomMasterPageSiteRelativeUrl

Specifies the custom Master page URL based on the site relative URL.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Site Relative
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MasterPageServerRelativeUrl

Specifies the Master page URL based on the server relative URL.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- MasterPageUrl
ParameterSets:
- Name: Server Relative
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MasterPageSiteRelativeUrl

Specifies the Master page URL based on the site relative URL.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Site Relative
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
