---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPlannerRosterMember.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPPlannerRosterMember
Module Name: PnP.PowerShell
---
  
# Remove-PnPPlannerRosterMember

**Required Permissions**

  * Microsoft Graph API: Tasks.ReadWrite (delegated) or Tasks.ReadWrite.All (application)

## SYNOPSIS

Removes a member from a Microsoft Planner Roster.

## SYNTAX

```powershell
Remove-PnPPlannerRosterMember -Identity <PlannerRosterPipeBind> [-Connection <PnPConnection>]
```

## DESCRIPTION
Removes a member from a Microsoft Planner Roster.

The Microsoft Planner Roster APIs this cmdlet uses are only available through the beta endpoint of Microsoft Graph. Microsoft can change the permissions they require without notice, so verify the permissions above against Microsoft Learn if a call is unexpectedly denied.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPPlannerRosterMember -Identity "6519868f-868f-6519-8f86-19658f861965" -User "johndoe@contoso.onmicrosoft.com"
```

Removes the provided user from the Microsoft Planner Roster with the provided identifier.

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

### -Identity
Identity of the Microsoft Planner Roster to remove the member from.

```yaml
Type: PlannerRosterPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
User principal name of the user to remove from being a member.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

