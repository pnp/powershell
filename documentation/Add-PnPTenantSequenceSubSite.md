---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantSequenceSubSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPTenantSequenceSubSite
---

# Add-PnPTenantSequenceSubSite

## SYNOPSIS

Adds a tenant sequence sub site object to a tenant sequence site object

## SYNTAX

### Default (Default)

```
Add-PnPTenantSequenceSubSite -SubSite <TeamNoGroupSubSite> -Site <SiteCollection>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add a tenant sequence sub site object to a tenant sequence site object.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTenantSequenceSubSite -Site $mysite -SubSite $mysubsite
```

Adds an existing subsite object to an existing sequence site object

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

The site to add the subsite to

```yaml
Type: SiteCollection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SubSite

The subsite to add

```yaml
Type: TeamNoGroupSubSite
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
