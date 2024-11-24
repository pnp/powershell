---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Restore-PnPDeletedMicrosoft365Group.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Restore-PnPDeletedMicrosoft365Group
---

# Restore-PnPDeletedMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Restores one deleted Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Restore-PnPDeletedMicrosoft365Group -Identity <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Restore-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
```

Restores a deleted Microsoft 365 Group based on its ID

### EXAMPLE 2
```powershell
$group = Get-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
Restore-PnPDeletedMicrosoft365Group -Identity $group
```

Restores the provided deleted Microsoft 365 Group

## EXAMPLES

### EXAMPLE 1

```powershell
Restore-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
```

Restores a deleted Microsoft 365 Group based on its ID

### EXAMPLE 2

```powershell
$group = Get-PnPDeletedMicrosoft365Group -Identity 38b32e13-e900-4d95-b860-fb52bc07ca7f
Restore-PnPDeletedMicrosoft365Group -Identity $group
```

Restores the provided deleted Microsoft 365 Group

## PARAMETERS

### -Identity

The Identity of the deleted Microsoft 365 Group

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/directory-deleteditems-restore)
