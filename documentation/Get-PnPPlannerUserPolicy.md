---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPPlannerUserPolicy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPPlannerUserPolicy
---

# Get-PnPPlannerUserPolicy

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Returns the Microsoft Planner user policy for a specific user

## SYNTAX

### Default (Default)

```
Get-PnPPlannerUserPolicy -Identity <string> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns the Microsoft Planner user policy for the provided user. If a Microsoft Planner user policy has never been set yet on a tenant, this cmdlet may return a '403 Forbidden: Access is denied' error. Set a policy once first to enable the background configuration to be done so this cmdlet can succeed from thereon.

## EXAMPLES

### Example 1

```powershell
Get-PnPPlannerUserPolicy -Identity "johndoe@contoso.onmicrosoft.com"
```
Returns the Microsoft Planner user policy for the provided user

## PARAMETERS

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

Azure Active Directory user identifier or user principal name of the user to retrieve the Microsoft Planner policy for

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
