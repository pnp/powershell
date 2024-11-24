---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPWebPermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPWebPermission
---

# Get-PnPWebPermission

## SYNOPSIS

Returns the explicit permissions for a specific SharePoint Web given a user or group by id.

## SYNTAX

### Default (Default)

```
Get-PnPWebPermission [-Identity] <WebPipeBind> -PrincipalId <Int32>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet retrieves the web permissions (role definitions) for a specific user or group in a provided web.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPWebPermission -Identity (Get-PnPWeb) -PrincipalId 60
```

Returns the permissions for the SharePoint group with id for the current Web.

### EXAMPLE 2

```powershell
Get-PnPWebPermission -Identity "subsite" -PrincipalId (Get-PnPGroup -Identity DemoGroup).Id
```

Returns the permissions for the SharePoint group called DemoGroup for a given subsite path.

## PARAMETERS

### -Identity

The id, name or server relative url of the Web to retrieve the permissions for.

```yaml
Type: WebPipeBand
DefaultValue: (CurrentWeb)
SupportsWildcards: false
ParameterValue: []
Aliases:
- Name
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
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
