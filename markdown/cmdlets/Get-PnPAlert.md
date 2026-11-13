---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAlert.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAlert
---
  
# Get-PnPAlert

## SYNOPSIS
Returns registered alerts for a user.

## SYNTAX

```powershell
Get-PnPAlert [[-List] <ListPipeBind>] [-User <UserPipeBind>] [-Title <String>] [-AllUsers]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve all registered alerts for given users. Using `AllUsers` option will allow to retrieve all alerts in the current site, regardless of the user or list it belongs to.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAlert
```

Returns all registered alerts for the current user.

### EXAMPLE 2
```powershell
Get-PnPAlert -List "Demo List"
```

Returns all alerts registered on the given list for the current user.

### EXAMPLE 3
```powershell
Get-PnPAlert -List "Demo List" -User "i:0#.f|membership|Alice@contoso.onmicrosoft.com"
```

Returns all alerts registered on the given list for the specified user.

### EXAMPLE 4
```powershell
Get-PnPAlert -Title "Demo Alert"
```

Returns all alerts with the given title for the current user. Title comparison is case sensitive.

### EXAMPLE 5
```powershell
Get-PnPAlert -AllUsers
```

Returns all alerts that exist in the current site, regardless of the user or list it belongs to.

### EXAMPLE 6
```powershell
Get-PnPAlert -List "Demo List" -AllUsers
```

Returns all alerts that exist in the current site for the list "Demo List", regardless of the user it belongs to.

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

### -List
The ID, Title or Url of the list.

```yaml
Type: ListPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Title
Retrieve alerts with this title. Title comparison is case sensitive.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
User to retrieve the alerts for (User ID, login name or actual User object). Skip this parameter to retrieve the alerts for the current user. Note: Only site owners can retrieve alerts for other users.

```yaml
Type: UserPipeBind
Parameter Sets: Alerts for a specific user

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AllUsers
Retrieves alerts for all users in the current site

```yaml
Type: SwitchParameter
Parameter Sets: Alerts for all users

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


