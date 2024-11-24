---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPPlannerUserPolicy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPPlannerUserPolicy
---

# Set-PnPPlannerUserPolicy

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Allows a Microsoft Planner user policy to be set for a specific user.

## SYNTAX

### Default (Default)

```
Set-PnPPlannerUserPolicy -Identity <string> [-BlockDeleteTasksNotCreatedBySelf <boolean>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows a Microsoft Planner user policy to be set for the provided user.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPPlannerUserPolicy -Identity "johndoe@contoso.onmicrosoft.com"
```
Sets the Microsoft Planner user policy for the provided user.

## PARAMETERS

### -BlockDeleteTasksNotCreatedBySelf

Allows the user for which the policy gets created to be blocked from deleting tasks that have not been created by the user itself.

```yaml
Type: Boolean
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

### -Connection

Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

Azure Active Directory user identifier or user principal name of the user to create the Microsoft Planner policy for.

```yaml
Type: String
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
