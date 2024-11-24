---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPPlannerConfiguration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPPlannerConfiguration
---

# Set-PnPPlannerConfiguration

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Allows the Microsoft Planner configuration of the tenant to be set.

## SYNTAX

### Default (Default)

```
Set-PnPPlannerConfiguration [-IsPlannerAllowed <boolean>] [-AllowRosterCreation <boolean>]
 [-AllowTenantMoveWithDataLoss <boolean>] [-AllowTenantMoveWithDataMigration <boolean>]
 [-AllowPlannerMobilePushNotifications <boolean>] [-AllowCalendarSharing <boolean>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows the Microsoft Planner tenant configuration to be changed.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPPlannerConfiguration -AllowRosterCreation:$false -IsPlannerAllowed:$true
```
This example configures Microsoft Planner to be enabled and disallows Roster plans to be created.

### EXAMPLE 2

```powershell
Set-PnPPlannerConfiguration -AllowPlannerMobilePushNotifications $false
```
This example disallows direct push notifications.

## PARAMETERS

### -AllowCalendarSharing

Allows configuring whether Outlook calendar sync is enabled.

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

### -AllowPlannerMobilePushNotifications

Allows configuring whether the direct push notifications are enabled where contents of the push notification are being sent directly through Apple's or Google's services to get to the iOS or Android client.

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

### -AllowRosterCreation

Allows configuring whether the creation of Roster containers (Planner plans without Microsoft 365 Groups) is allowed.

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

### -AllowTenantMoveWithDataLoss

Allows configuring whether a tenant move into a new region is currently authorized.

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

### -AllowTenantMoveWithDataMigration

Allows configuring whether a tenant move with data migration is authorized.

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

### -IsPlannerAllowed

Allows configuring if Microsoft Planner is enabled on the tenant.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
