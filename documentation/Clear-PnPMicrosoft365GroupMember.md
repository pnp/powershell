---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Clear-PnPMicrosoft365GroupMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Clear-PnPMicrosoft365GroupMember
---

# Clear-PnPMicrosoft365GroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All, GroupMember.ReadWrite.All

Removes all current members from a particular Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Clear-PnPMicrosoft365GroupMember -Identity <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove all current members from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Clear-PnPMicrosoft365GroupMember -Identity "Project Team"
```

Removes all the current members from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group to remove all members from

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-members)
