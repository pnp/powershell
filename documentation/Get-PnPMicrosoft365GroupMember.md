---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPMicrosoft365GroupMember
---

# Get-PnPMicrosoft365GroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : at least Group.Read.All
  * Microsoft Graph API : Directory.Read.All

Returns the members of a particular Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Get-PnPMicrosoft365GroupMember -Identity <Microsoft365GroupPipeBind> [-UserType <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns the members of a particular Microsoft 365 Group

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPMicrosoft365GroupMember -Identity $groupId
```

Retrieves all the members of a specific Microsoft 365 Group based on its ID

### EXAMPLE 2

```powershell
Get-PnPMicrosoft365GroupMember -Identity $group
```

Retrieves all the members of a specific Microsoft 365 Group based on the group's object instance

### EXAMPLE 3

```powershell
Get-PnPMicrosoft365GroupMember -Identity "Sales" | Where-Object UserType -eq Guest
```

Returns all the guest users of the Microsoft 365 Group named "Sales"

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
