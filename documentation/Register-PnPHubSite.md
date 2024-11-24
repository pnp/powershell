---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Register-PnPHubSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Register-PnPHubSite
---

# Register-PnPHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Registers a site as a hub site.

## SYNTAX

### Default (Default)

```
Register-PnPHubSite -Site <SitePipeBind> [-Principals <String[][]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Registers a site as a hub site.

## EXAMPLES

### EXAMPLE 1

```powershell
Register-PnPHubSite -Site "https://tenant.sharepoint.com/sites/myhubsite"
```

This example registers the specified site as a hub site.

### EXAMPLE 2

```powershell
Register-PnPHubSite -Site "https://tenant.sharepoint.com/sites/myhubsite" -Principals "user@contoso.com"
```

This example registers the specified site as a hub site and specifies that 'user@contoso.com' be granted rights to the hub site.

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

### -Principals

Specifies one or more principals (user or group) to be granted rights to the specified hub site. Can be used to filter who can associate sites to this hub site.

```yaml
Type: String[]
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

### -Site

The site to register as a hub site.

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
