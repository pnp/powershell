---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSharingForNonOwnersOfSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSharingForNonOwnersOfSite
---

# Get-PnPSharingForNonOwnersOfSite

## SYNOPSIS

Returns $false if sharing of the site and items in the site is restricted only to owners or $true if members and owners are allowed to share

## SYNTAX

### Default (Default)

```
Get-PnPSharingForNonOwnersOfSite [-Identity <SitePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns $false if sharing of the site and items in the site is restricted only to owners or $true if members and owners are allowed to share. You can disable sharing by non owners by using Disable-PnPSharingForNonOwnersOfSite. At this point there is no interface available yet to enable sharing by owners and members again through script. You will have to do so through the user interface of SharePoint.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSharingForNonOwnersOfSite
```

Returns $false if sharing of the site and items in the site is restricted only to owners or $true if members and owners are allowed to share

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



```yaml
Type: SitePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Url
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
