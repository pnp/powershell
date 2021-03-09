---
Module Name: PnP.PowerShell
title: New-PnPAadGroup
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPAadGroup.html
---
 
# New-PnPAadGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.Create, Group.ReadWrite.All

Creates a new Azure Active Directory group. This can be a security or distribution group.

## SYNTAX

```powershell
New-PnPAadGroup -DisplayName <String> -Description <String> -MailNickname <String> [-Owners <String[]>] [-Members <String[]>] [-IsSecurityEnabled <SwitchParameter>] [-IsMailEnabled <SwitchParameter>] [-Force] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPAadGroup -DisplayName $displayName -Description $description -MailNickname $nickname
```

Creates an Azure Active Directory group with all the required properties

### EXAMPLE 2
```powershell
New-PnPAadGroup -DisplayName $displayName -Description $description -MailNickname $nickname -Owners $arrayOfOwners -Members $arrayOfMembers
```

Creates a new Azure Active Directory group with all the required properties, and with a custom list of Owners and a custom list of Members

### EXAMPLE 3
```powershell
New-PnPAadGroup -DisplayName $displayName -Description $description -MailNickname $nickname -IsSecurityEnabled -IsMailEnabled
```

Creates a new Azure Active Directory group which is mail and security enabled

## PARAMETERS

### -Description
The Description of the Azure Active Directory group

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The Display Name of the Azure Active Directory group

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsSecurityEnabled
Creates an Azure Active Directory group which can be used to set permissions

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsMailEnabled
Creates an Azure Active Directory group which can be used to send e-mail to

```yaml
Type: SwitchParameter
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

### -MailNickname
The Mail Nickname of the Azure Active Directory group. Cannot contain spaces.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Members
The array UPN values of the group's members

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
The array UPN values of the group's owners

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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)[Documentation](https://docs.microsoft.com/graph/api/group-post-groups)