---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteVersionPolicyStatus.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteVersionPolicyStatus
---

# Get-PnPSiteVersionPolicyStatus

## SYNOPSIS

Get the progress of setting version policy for existing document libraries on the site.

## SYNTAX

### Default (Default)

```
Get-PnPSiteVersionPolicyStatus [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of the progress of setting version policy for existing document libraries on the site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteVersionPolicyStatus
```

Returns the progress of setting version policy for existing document libraries on the site.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
- [Microsoft Docs documentation](https://learn.microsoft.com/sharepoint/dev/solution-guidance/modern-experience-site-classification#programmatically-read-the-classification-of-a-site)
