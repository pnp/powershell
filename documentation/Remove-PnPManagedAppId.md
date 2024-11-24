---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPManagedAppId.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPManagedAppId
---

# Remove-PnPManagedAppId

## SYNOPSIS

Removes an App Id from the Credential Manager

## SYNTAX

### Default (Default)

```
Remove-PnPManagedAppId -Url <String> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes an App Id from the Credential Manager

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPManagedAppId -Url "https://tenant.sharepoint.com"
```

Removes the specified App Id from the Credential Manager

## PARAMETERS

### -Force

If specified you will not be asked for confirmation

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

### -Url

The Url for which to remove the App Id

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
