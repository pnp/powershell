---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPGroupPermissions.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPGroupPermissions
---

# Get-PnPGroupPermissions

## SYNOPSIS

Returns the permissions for a specific SharePoint group

## SYNTAX

### Default (Default)

```
Get-PnPGroupPermissions [-Identity] <GroupPipeBind> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command will return the permissions of a specific SharePoint group

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPGroupPermissions -Identity 'My Site Members'
```

Returns the permissions for the SharePoint group with the name 'My Site Members'

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

SharePoint group name, id or instance to return the permissions for

```yaml
Type: GroupPipeBind
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
