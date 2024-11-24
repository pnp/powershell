---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Disable-PnPSharingForNonOwnersOfSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Disable-PnPSharingForNonOwnersOfSite
---

# Disable-PnPSharingForNonOwnersOfSite

## SYNOPSIS

Configures the site to only allow sharing of the site and items in the site by owners

## SYNTAX

### Default (Default)

```
Disable-PnPSharingForNonOwnersOfSite [-Identity <SitePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Configures the site to only allow sharing of the site and items in the site by owners. At this point there is no interface available yet to undo this action through script. You will have to do so through the user interface of SharePoint.

## EXAMPLES

### EXAMPLE 1

```powershell
Disable-PnPSharingForNonOwnersOfSite
```

Restricts sharing of the site and items in the site only to owners

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
