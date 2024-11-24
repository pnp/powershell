---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPRoleDefinition.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPRoleDefinition
---

# Get-PnPRoleDefinition

## SYNOPSIS

Retrieves a Role Definitions of a site

## SYNTAX

### Default (Default)

```
Get-PnPRoleDefinition [[-Identity] <RoleDefinitionPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve Role Definitions of a site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPRoleDefinition
```

Retrieves the Role Definitions (Permission Levels) settings of the current site

### EXAMPLE 2

```powershell
Get-PnPRoleDefinition -Identity Read
```

Retrieves the specified Role Definition (Permission Level) settings of the current site

### EXAMPLE 3

```powershell
Get-PnPRoleDefinition | Where-Object { $_.RoleTypeKind -eq "Administrator" }
```

Retrieves the Role Definition (Permission Level) settings with the Administrator type, regardless of its name

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

The name of a role definition to retrieve.

```yaml
Type: RoleDefinitionPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
