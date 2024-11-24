---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365GroupMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPMicrosoft365GroupMember
---

# Remove-PnPMicrosoft365GroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All, GroupMember.ReadWrite.All

Removes members from a particular Microsoft 365 Group

## SYNTAX

### Default (Default)

```
Remove-PnPMicrosoft365GroupMember -Identity <Microsoft365GroupPipeBind> -Users <String[]>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove members from a specified Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPMicrosoft365GroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Removes the provided two users as members from the Microsoft 365 Group named "Project Team"

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group to remove members from

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

### -Users

The UPN(s) of the user(s) to remove as members from the Microsoft 365 Group

```yaml
Type: String[]
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-members)
