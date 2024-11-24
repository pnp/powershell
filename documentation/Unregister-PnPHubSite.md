---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Unregister-PnPHubSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Unregister-PnPHubSite
---

# Unregister-PnPHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Unregister a site as a hubsite.

## SYNTAX

### Default (Default)

```
Unregister-PnPHubSite -Site <SitePipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to unregister a site as a hubsite.

## EXAMPLES

### EXAMPLE 1

```powershell
Unregister-PnPHubSite -Site "https://tenant.sharepoint.com/sites/myhubsite"
```

This example unregisters the specified site as a hubsite.

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

### -Site

The site to unregister as a hubsite.

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
