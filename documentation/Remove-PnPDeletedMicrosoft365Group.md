---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPDeletedMicrosoft365Group.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPDeletedMicrosoft365Group
---

# Remove-PnPDeletedMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Permanently removes one deleted Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Remove-PnPDeletedMicrosoft365Group -Identity <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to permanently remove a deleted Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
```

Permanently removes a deleted Microsoft 365 Group based on its ID

### EXAMPLE 2

```powershell
$group = Get-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
Remove-PnPDeletedMicrosoft365Group -Identity $group
```

Permanently removes the provided deleted Microsoft 365 Group

## PARAMETERS

### -Identity

The identity of the deleted Microsoft 365 Group to be deleted

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
