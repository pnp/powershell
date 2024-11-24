---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPOrgNewsSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPOrgNewsSite
---

# Add-PnPOrgNewsSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds the site as an organization news source in your tenant

## SYNTAX

### Default (Default)

```
Add-PnPOrgNewsSite -OrgNewsSiteUrl <SitePipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add site as an organization news source in your tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPOrgNewsSite -OrgNewsSiteUrl "https://yourtenant.sharepoint.com/sites/news"
```

Adds the site as one of multiple possible tenant's organizational news sites

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

### -OrgNewsSiteUrl

The url of the site to be marked as one of organization's news sites

```yaml
Type: SitePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
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

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
