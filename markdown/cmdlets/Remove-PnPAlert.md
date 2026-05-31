---
Module Name: PnP.PowerShell
title: Remove-PnPAlert
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPAlert.html
---
 
# Remove-PnPAlert

## SYNOPSIS
Removes an alert for a user.

## SYNTAX

```powershell
Remove-PnPAlert [-User <UserPipeBind>] -Identity <AlertPipeBind> [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to remove an alert for a user.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPAlert -Identity 641ac67f-0ce0-4837-874a-743c8f8572a7
```

Removes the alert with the specified ID for the current user.

### EXAMPLE 2
```powershell
Remove-PnPAlert -Identity 641ac67f-0ce0-4837-874a-743c8f8572a7 -User "i:0#.f|membership|Alice@contoso.onmicrosoft.com"
```

Removes the alert with the specified ID for the specified user.

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

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The alert id, or the actual alert object to remove.

```yaml
Type: AlertPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
User to remove the alert for. Accepts User ID, login name or actual User object. Skip this parameter to use the current user. Note: Only site owners can remove alerts for other users.

```yaml
Type: UserPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

