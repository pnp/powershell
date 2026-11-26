---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPEntraIDGroupOwner.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPEntraIDGroupOwner
---
  
# Add-PnPEntraIDGroupOwner

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: All of Group.ReadWrite.All, User.ReadWrite.All

Adds users to the owners of an Entra ID group. This can be a security or Microsoft 365 group. Distribution lists are not currently supported by Graph API.

## SYNTAX

```powershell
Add-PnPEntraIDGroupOwner -Identity <EntraIDGroupPipeBind> -Users <String[]> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to add users to owners of an Entra ID Group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPEntraIDGroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Adds the provided two users as additional owners to the Entra ID group named "Project Team".

### EXAMPLE 2
```powershell
Add-PnPEntraIDGroupOwner -Identity "Project Team" -Users "125eaa87-7b54-41fd-b30f-2adfa68c4afe"
```

Sets the provided security group as owner of the Entra ID group name "Project Team".

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
The Identity of the Entra ID group to add owners to.

```yaml
Type: EntraIDGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Users
The UPN(s) of the user(s) to add to the Entra ID group as a member.

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