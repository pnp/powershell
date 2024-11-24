---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365Group.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPMicrosoft365Group
---

# Remove-PnPMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes one Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Remove-PnPMicrosoft365Group -Identity <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPMicrosoft365Group -Identity $groupId
```

Removes an Microsoft 365 Group based on its ID

### EXAMPLE 2

```powershell
Remove-PnPMicrosoft365Group -Identity $group
```

Removes the provided Microsoft 365 Group

### EXAMPLE 3

```powershell
Get-PnPMicrosoft365Group | ? Visibility -eq "Public" | Remove-PnPMicrosoft365Group
```

Removes all the public Microsoft 365 Groups

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
