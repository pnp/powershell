---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPHubSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPHubSite
---

# Get-PnPHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieve all or a specific hubsite.

## SYNTAX

### Default (Default)

```
Get-PnPHubSite [[-Identity] <HubSitePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPHubSite
```

Returns all hubsite properties

### EXAMPLE 2
```powershell
Get-PnPHubSite -Identity "https://contoso.sharepoint.com/sites/myhubsite"
```

Returns the properties of the specified hubsite by using the hubsite url

### EXAMPLE 3
```powershell
Get-PnPHubSite -Identity "bc07d4b8-1c2f-4184-8cc2-a52dfd6fe0c4"
```

Returns the properties of the specified hubsite by using the hubsite site id

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPHubSite
```

Returns all hubsite properties

### EXAMPLE 2

```powershell
Get-PnPHubSite -Identity "https://contoso.sharepoint.com/sites/myhubsite"
```

Returns the properties of the specified hubsite by using the hubsite url

### EXAMPLE 3

```powershell
Get-PnPHubSite -Identity "bc07d4b8-1c2f-4184-8cc2-a52dfd6fe0c4"
```

Returns the properties of the specified hubsite by using the hubsite site id

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

Specify hub site url or site id

```yaml
Type: HubSitePipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
