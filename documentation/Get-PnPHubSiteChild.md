---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPHubSiteChild.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPHubSiteChild
---

# Get-PnPHubSiteChild

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves all sites associated to a specific hub site

## SYNTAX

### Default (Default)

```
Get-PnPHubSiteChild [-Identity <HubSitePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Retrieves all sites associated to a specific hub site

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPHubSiteChild
```

Returns the sites which are associated to the currently connected to hub site

### EXAMPLE 2

```powershell
Get-PnPHubSiteChild -Identity "https://contoso.sharepoint.com/sites/myhubsite"
```

Returns the sites which are associated with the provided hub site as their hub site

### EXAMPLE 3

```powershell
Get-PnPHubSite | Get-PnPHubSiteChild
```

Returns all sites that are associated to a hub site

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

### -Identity

The URL, Id or instance of the hubsite for which to receive the sites referring to it. If not provided, the currently connected to site will be used.

```yaml
Type: HubSitePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
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
