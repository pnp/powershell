---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteGroup
---

# Get-PnPSiteGroup

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets all the groups in the current or specified site collection.

## SYNTAX

### Default (Default)

```
Get-PnPSiteGroup
 [-Group <String>] [-Site <SitePipeBind>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Use the Get-PnPSiteGroup cmdlet to get all the groups on the specified or currently connected site collection.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteGroup
```

Returns all SharePoint groups in the current connected to site

### EXAMPLE 2

```powershell
Get-PnPSiteGroup -Site "https://contoso.sharepoint.com/sites/siteA"
```
This will return all SharePoint groups in the specified site

### EXAMPLE 3

```powershell
Get-PnPSiteGroup -Group "SiteA Members"
```
This will return the specified group for the current connected to site

### EXAMPLE 4

```powershell
Get-PnPSiteGroup -Group "SiteA Members" -Site "https://contoso.sharepoint.com/sites/siteA"
```
This will return the specified group for the specified site.

## PARAMETERS

### -Group

Specifies the group name.

```yaml
Type: String
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

### -Site

Retrieve the associated member group.

```yaml
Type: String
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
