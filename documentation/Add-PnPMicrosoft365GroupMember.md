---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPMicrosoft365GroupMember.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPMicrosoft365GroupMember
---
  
# Add-PnPMicrosoft365GroupMember

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: All of Group.ReadWrite.All, User.ReadWrite.All

Adds members to a particular Microsoft 365 Group.

## SYNTAX

```powershell
Add-PnPMicrosoft365GroupMember -Identity <Microsoft365GroupPipeBind> -Users <String[]> [-RemoveExisting] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to add multiple users to Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPMicrosoft365GroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Adds the provided two users as additional members to the Microsoft 365 Group named "Project Team".

### EXAMPLE 2
```powershell
Add-PnPMicrosoft365GroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com" -RemoveExisting
```

Sets the provided two users as the only members of the Microsoft 365 Group named "Project Team" by removing any current existing members first.

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
The Identity of the Microsoft 365 Group to add members to.

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -RemoveExisting
If provided, all existing members will be removed and only those provided through Users will become members.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Users
The UPN(s) of the user(s) to add to the Microsoft 365 Group as a member.

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-post-members)
