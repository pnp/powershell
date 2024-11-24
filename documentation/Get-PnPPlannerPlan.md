---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerPlan.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPlannerPlan
---

# Get-PnPPlannerPlan

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All
  * Microsoft Graph API: Group.Read.All

Returns all or a specific Planner plan for a Microsoft 365 Group.

## SYNTAX

### By Group

```
Get-PnPPlannerPlan -Group <PlannerGroupPipeBind> [-Identity <PlannerPlanPipeBind>]
 [-ResolveIdentities]
```

### By Plan Id

```
Get-PnPPlannerPlan -Id <String> [-ResolveIdentities]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns all or a specific Planner plan for a Microsoft 365 Group.

## EXAMPLES

### Example 1

```powershell
Get-PnPPlannerPlan -Group "Marketing"
```

Returns all plans for the Marketing group.

### Example 2

```powershell
Get-PnPPlannerPlan -Group "Marketing" -Identity "Conference Plan"
```

Returns the specified plan for the Marketing group.

### Example 3

```powershell
Get-PnPPlannerPlan -Id "gndWOTSK60GfPQfiDDj43JgACDCb" -ResolveIdentities
```

Rerturns the plan with specified ID with resolved identities.

## PARAMETERS

### -Group

Specify the group containing the plans

```yaml
Type: PlannerGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Group
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Id

If specified the plan with this ID will be returned.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Plan Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

If specified the plan with this ID or Name will be returned.

```yaml
Type: PlannerPlanPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Group
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ResolveIdentities

Show user display names instead of user IDs.

```yaml
Type: SwitchParameter
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
