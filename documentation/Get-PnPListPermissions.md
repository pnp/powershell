---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPListPermissions.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPListPermissions
---

# Get-PnPListPermissions

## SYNOPSIS

Returns the permissions for a specific SharePoint List given a user or group by id.

## SYNTAX

### Default (Default)

```
Get-PnPListPermissions [-Identity] <ListPipeBind> -PrincipalId <Int32>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet retrieves the list permissions (role definitions) for a specific user or group in a provided list.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPListPermissions -Identity DemoList -PrincipalId 60
```

Returns the permissions for the SharePoint group with id for the list DemoList.

### EXAMPLE 2

```powershell
Get-PnPListPermissions -Identity DemoList -PrincipalId (Get-PnPGroup -Identity DemoGroup).Id
```

Returns the permissions for the SharePoint group call DemoGroup for the list DemoList.

## PARAMETERS

### -Identity

The id, name or server relative url of the list to retrieve the permissions for.

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Name
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -PrincipalId

The id of a user or a SharePoint group. See Get-PnPUser and Get-PnPGroup.

```yaml
Type: Int32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Name
ParameterSets:
- Name: (All)
  Position: 0
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
