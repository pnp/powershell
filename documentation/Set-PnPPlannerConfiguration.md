---
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPPlannerConfiguration.html
Module Name: PnP.PowerShell
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
  
# Set-PnPPlannerConfiguration

## SYNOPSIS

**Required Permissions**

* Azure: tasks.office.com

Allows the Microsoft Planner configuration of the tenant to be set

## SYNTAX

```
Set-PnPPlannerConfiguration [-IsPlannerAllowed <boolean>] [-AllowRosterCreation <boolean>] [-AllowTenantMoveWithDataLoss <boolean>] [-AllowPlannerMobilePushNotifications <boolean>] [-AllowCalendarSharing <boolean>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet allows the Microsoft Planner tenant configuration to be changed

## EXAMPLES

### Example 1
```powershell
Set-PnPPlannerConfiguration -AllowRosterCreation:$false -IsPlannerAllowed:$true
```
Configures Microsoft Planner to be enabled and disallow roster plans to be created

## PARAMETERS

### -IsPlannerAllowed
Allows configuring if Microsoft Planner is enabled on the tenant

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowRosterCreation
Allows configuring whether the creation of Roster containers (Planner plans without Microsoft 365 Groups) is allowed

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowTenantMoveWithDataLoss
Allows configuring whether a tenant move into a new region is currently authorized

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowPlannerMobilePushNotifications
Allows configuring whether the direct push notifications are enabled where contents of the push notification are being sent directly through Apple's or Google's services to get to the iOS or Android client

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllowCalendarSharing
Allows configuring whether Outlook calendar sync is enabled

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection
Optional connection to be used by the cmdlet.
Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)