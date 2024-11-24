---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Clear-PnPMicrosoft365GroupOwner.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Clear-PnPMicrosoft365GroupOwner
---

# Clear-PnPMicrosoft365GroupOwner

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Removes all current owners from a particular Microsoft 365 Group (aka Unified Group)

## SYNTAX

### Default (Default)

```
Clear-PnPMicrosoft365GroupOwner -Identity <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove all current owners from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Clear-PnPMicrosoft365GroupOwner -Identity "Project Team"
```

Removes all the current owners from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group to remove all owners from

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
