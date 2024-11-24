---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteVersionPolicy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteVersionPolicy
---

# Get-PnPSiteVersionPolicy

## SYNOPSIS

Get version policy setting of the site.

**Required Permissions**

|        Type      |                    API/ Permission Name                    |                    Admin consent required                    |
| --------------- | --------------------------------------- | -------- |
| Delegated       | AllSites.FullControl | yes                               |

## SYNTAX

### Default (Default)

```
Get-PnPSiteVersionPolicy [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows retrieval of version policy setting on the site. When the new document libraries are created, they will be set as the version policy of the site.
If the version policy is not set on the site, the setting of the tenant will be used.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteVersionPolicy
```

Returns the version policy setting of the site.

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
