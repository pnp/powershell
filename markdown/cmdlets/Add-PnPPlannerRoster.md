---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPlannerRoster.html
tags: Available in the current Nightly Release only.
title: Add-PnPPlannerRoster
---
  
# Add-PnPPlannerRoster

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite (delegated) or Tasks.ReadWrite.All (application)

## SYNOPSIS

Creates a new Microsoft Planner Roster

## SYNTAX

```powershell
Add-PnPPlannerRoster [-Connection <PnPConnection>] 
```

## DESCRIPTION
Creates a new Microsoft Planner Roster

The Microsoft Planner Roster APIs this cmdlet uses are only available through the beta endpoint of Microsoft Graph. Microsoft can change the permissions they require without notice, so verify the permissions above against Microsoft Learn if a call is unexpectedly denied.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPlannerRoster
```

Creates a new Microsoft Planner Roster

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

