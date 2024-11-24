---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPSiteGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPSiteGroup
---

# Remove-PnPSiteGroup

## SYNOPSIS

Removes a group from a web.

## SYNTAX

### Default (Default)

```
Remove-PnPSiteGroup -Identity <String> [-Site <SitePipeBind>] [-Confirm]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove a group from specified site.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPSiteGroup -Identity GroupToRemove -Site "https://contoso.sharepoint.com/sites/marketing"
```

This example removes a group named GroupToRemove from the site collection https://contoso.sharepoint.com/sites/marketing.

### EXAMPLE 2

```powershell
Remove-PnPSiteGroup -Identity GroupToRemove
```

This example removes a group named GroupToRemove from the current site collection that has been connected to with Connect-PnPOnline.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
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

Specifies the name of the group to remove.

```yaml
Type: String
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

Specifies the site collection to remove the group from.

```yaml
Type: SitePipeBind
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
