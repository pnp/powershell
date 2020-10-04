---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpuser
schema: 2.0.0
title: Get-PnPUser
---

# Get-PnPUser

## SYNOPSIS
Returns site users of current web

## SYNTAX

### Identity based request (Default)
```
Get-PnPUser [-Identity <UserPipeBind>] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### With rights assigned
```
Get-PnPUser [-WithRightsAssigned] [-Web <WebPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

### With rights assigned detailed
```
Get-PnPUser [-WithRightsAssignedDetailed] [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
This command will return all users that exist in the current site collection's User Information List, optionally identifying their current permissions to this site

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUser
```

Returns all users from the User Information List of the current site collection regardless if they currently have rights to access the current site

### EXAMPLE 2
```powershell
Get-PnPUser -Identity 23
```

Returns the user with Id 23 from the User Information List of the current site collection

### EXAMPLE 3
```powershell
Get-PnPUser -Identity "i:0#.f|membership|user@tenant.onmicrosoft.com"
```

Returns the user with LoginName i:0#.f|membership|user@tenant.onmicrosoft.com from the User Information List of the current site collection

### EXAMPLE 4
```powershell
Get-PnPUser | ? Email -eq "user@tenant.onmicrosoft.com"
```

Returns the user with e-mail address user@tenant.onmicrosoft.com from the User Information List of the current site collection

### EXAMPLE 5
```powershell
Get-PnPUser -WithRightsAssigned
```

Returns only those users from the User Information List of the current site collection who currently have any kind of access rights given either directly to the user or Active Directory Group or given to the user or Active Directory Group via membership of a SharePoint Group to the current site

### EXAMPLE 6
```powershell
Get-PnPUser -WithRightsAssigned -Web subsite1
```

Returns only those users from the User Information List of the current site collection who currently have any kind of access rights given either directly to the user or Active Directory Group or given to the user or Active Directory Group via membership of a SharePoint Group to subsite 'subsite1'

### EXAMPLE 7
```powershell
Get-PnPUser -WithRightsAssignedDetailed
```

Returns all users who have been granted explicit access to the current site, lists and listitems

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

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

### -Identity
User ID or login name

```yaml
Type: UserPipeBind
Parameter Sets: Identity based request
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithRightsAssigned
If provided, only users that currently have any kinds of access rights assigned to the current site collection will be returned. Otherwise all users, even those who previously had rights assigned, but not anymore at the moment, will be returned as the information is pulled from the User Information List. Only works if you don't provide an -Identity.

```yaml
Type: SwitchParameter
Parameter Sets: With rights assigned
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithRightsAssignedDetailed
If provided, only users that currently have any specific kind of access rights assigned to the current site, lists or listitems/documents will be returned. Otherwise all users, even those who previously had rights assigned, but not anymore at the moment, will be returned as the information is pulled from the User Information List. Only works if you don't provide an -Identity.

Only applicable to: SharePoint Online

```yaml
Type: SwitchParameter
Parameter Sets: With rights assigned detailed
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)