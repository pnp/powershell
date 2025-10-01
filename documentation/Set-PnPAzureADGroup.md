---
Module Name: PnP.PowerShell
title: Set-PnPAzureADGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPAzureADGroup.html
---
 
# Set-PnPAzureADGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Sets the properties of a specific Azure Active Directory group.

## SYNTAX

```powershell
Set-PnPAzureADGroup -Identity <AzureADGroupPipeBind> [-DisplayName <String>] [-Description <String>]
 [-Owners <String[]>] [-Members <String[]>] [-SecurityEnabled] [-MailEnabled] 
 [-HideFromAddressLists <Boolean>] [-HideFromOutlookClients <Boolean>] 
 
```

## DESCRIPTION
This cmdlet sets the properties of a specific Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAzureADGroup -Identity $group -DisplayName "My DisplayName"
```

Sets the display name of the group where $group is a Group entity.

### EXAMPLE 2
```powershell
Set-PnPAzureADGroup -Identity $groupId -Description "My Description" -DisplayName "My DisplayName"
```

Sets the display name and description of a group based upon its ID.

### EXAMPLE 3
```powershell
Set-PnPAzureADGroup -Identity $group -Owners demo@contoso.com
```

Sets demo@contoso.com as the owner of the group.

## PARAMETERS

### -SecurityEnabled
Sets the Azure Active Directory group to be allowed to be used for setting permissions.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MailEnabled
Sets the Azure Active Directory group to be allowed to be used for receiving e-mail.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description of the group to set.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name of the group to set.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideFromAddressLists
Controls whether the group is hidden or shown in the Global Address List (GAL).

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideFromOutlookClients
Controls whether the group shows in the Outlook left-hand navigation.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The identity of the Azure Active Directory group.

```yaml
Type: AzureADGroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Members
The array UPN values of members to set to the group. Note: Will replace members.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Owners
The array UPN values of owners to set to the group. Note: Will replace owners.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-update)
