---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPAzureADGroupMember.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPAzureADGroupMember
---
  
# Add-PnPAzureADGroupMember

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: All of Group.ReadWrite.All, User.ReadWrite.All

Adds members to a particular Azure Active Directory Group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

```powershell
Add-PnPAzureADGroupMember -Identity <AzureADGroupPipeBind> -Users <String[]> [-RemoveExisting] [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to add users to Azure Active Directory Group. This can be a security, distribution or Microsoft 365 group. By specifying `-RemoveExisting` option it is possible to first clear the group of all existing members.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPAzureADGroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Adds the provided two users as additional members to the Azure Active Directory Group named "Project Team".

### EXAMPLE 2
```powershell
Add-PnPAzureADGroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com" -RemoveExisting
```

Sets the provided two users as the only members of the Azure Active Directory group named "Project Team" by removing any current existing members first.

### EXAMPLE 3
```powershell
Add-PnPAzureADGroupMember -Identity "Project Team" -Users "125eaa87-7b54-41fd-b30f-2adfa68c4afe"
```

Sets the provided security group as a member of the Azure Active Directory group name "Project Team".

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
The Identity of the Azure Active Directory group to add members to.

```yaml
Type: AzureADGroupPipeBind
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
The UPN(s) of the user(s) to add to the Azure Active Directory group as a member.

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