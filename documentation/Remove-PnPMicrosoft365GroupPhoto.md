---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365GroupPhoto.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPMicrosoft365GroupPhoto
---

# Remove-PnPMicrosoft365GroupPhoto

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes the profile photo from a particular Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Remove-PnPMicrosoft365GroupPhoto -Identity <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove profile photo from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPMicrosoft365GroupPhoto -Identity "Project Team"
```

Removes profile photo from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group to remove profile photo from

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-owners)
