---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerRosterPlan.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPlannerRosterPlan
---

# Get-PnPPlannerRosterPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite
  * Microsoft Graph API: Tasks.Read

Returns Microsoft Planner roster plans for a specific Microsoft Planner Roster or a specific user

## SYNTAX

### Default (Default)

```
Get-PnPPlannerRosterPlan [-Identity <PlannerRosterPipeBind>] [-User <string>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns Microsoft Planner roster plans for a specific Microsoft Planner Roster or a specific user

## EXAMPLES

### Example 1

```powershell
Get-PnPPlannerRosterPlan -Identity "abcdefgh"
```

Returns all plans inside the roster with the provided identifier

### Example 2

```powershell
Get-PnPPlannerRosterPlan -User "johndoe@contoso.onmicrosoft.com"
```

Returns all roster plans for the provided user

## PARAMETERS

### -Identity

A Microsoft Planner Roster Id or instance. Documentation: https://learn.microsoft.com/graph/api/plannerroster-list-plans?view=graph-rest-beta&tabs=http

```yaml
Type: PlannerGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: BYROSTER
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -User

The user principal name to query for. Documentation: https://learn.microsoft.com/graph/api/planneruser-list-rosterplans?view=graph-rest-beta&tabs=http

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: BYUSER
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### CommonParameters

This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable,
-InformationAction, -InformationVariable, -OutBuffer, -OutVariable, -PipelineVariable,
-ProgressAction, -Verbose, -WarningAction, and -WarningVariable. For more information, see
[about_CommonParameters](https://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
