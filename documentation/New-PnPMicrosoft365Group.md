---
Module Name: PnP.PowerShell
title: New-PnPMicrosoft365Group
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPMicrosoft365Group.html
---
 
# New-PnPMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.Create, Group.ReadWrite.All

Creates a new Microsoft 365 Group

## SYNTAX

```powershell
New-PnPMicrosoft365Group -DisplayName <String> -Description <String> -MailNickname <String>
 [-Owners <String[]>] [-Members <String[]>] [-IsPrivate] [-LogoPath <String>] [-CreateTeam] [-HideFromAddressLists <Boolean>] [-HideFromOutlookClients <Boolean>] [-ResourceBehaviorOptions <TeamResourceBehaviorOptions>] [-Force] [-SensitivityLabels <GUID[]>]
  [<CommonParameters>]
```

## DESCRIPTION

Allows to create a new Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPMicrosoft365Group -DisplayName $displayName -Description $description -MailNickname $nickname
```

Creates a public Microsoft 365 Group with all the required properties

### EXAMPLE 2
```powershell
New-PnPMicrosoft365Group -DisplayName $displayName -Description $description -MailNickname $nickname -Owners "owner1@domain.com" -Members "member1@domain.com"
```

Creates a public Microsoft 365 Group with all the required properties, and with a custom list of Owners and a custom list of Members

### EXAMPLE 3
```powershell
New-PnPMicrosoft365Group -DisplayName $displayName -Description $description -MailNickname $nickname -IsPrivate
```

Creates a private Microsoft 365 Group with all the required properties

### EXAMPLE 4
```powershell
New-PnPMicrosoft365Group -DisplayName $displayName -Description $description -MailNickname $nickname -Owners $arrayOfOwners -Members $arrayOfMembers -IsPrivate
```

Creates a private Microsoft 365 Group with all the required properties, and with a custom list of Owners and a custom list of Members

### EXAMPLE 5
```powershell
New-PnPMicrosoft365Group -DisplayName "myPnPDemo1" -Description $description -MailNickname $nickname -Owners $arrayOfOwners -Members $arrayOfMembers -IsPrivate -ResourceBehaviorOptions WelcomeEmailDisabled, HideGroupInOutlook
```

This will create a new Microsoft 365 Group called "myPnPDemo1" and sets the privacy to Private. Welcome Email will not be sent when the Group is created. The M365 Group will also not be visible in Outlook.

### EXAMPLE 6
```powershell
New-PnPMicrosoft365Group -DisplayName $displayName -Description $description -MailNickname $nickname -IsPrivate -SensitivityLabels "bc98af29-59eb-4869-baaa-9a8dff631aa4"
```

Creates a private Microsoft 365 Group with all the required properties and applies the sensitivity label.

## PARAMETERS

### -CreateTeam
Creates a Microsoft Teams team associated with created group

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
The Description of the Microsoft 365 Group

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
The Display Name of the Microsoft 365 Group

```yaml
Type: String
Parameter Sets: (All)

Required: True
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

### -LogoPath
The path to the logo file of to set. Supported formats are .png, .gif and .jpg

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsPrivate
Makes the group private when selected

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
The Mail Nickname of the Microsoft 365 Group. Cannot contain spaces.

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
The array UserPrincipalName values of the group's members

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
The array UserPrincipalName values of the group's owners

```yaml
Type: String[]
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

### -ResourceBehaviorOptions

Allows providing ResourceBehaviorOptions which accepts multiple values that specify group behaviors for a Microsoft 365 Group.

```yaml
Type: TeamResourceBehaviorOptions
Parameter Sets: (All)
Accepted values: AllowOnlyMembersToPost, HideGroupInOutlook, SubscribeNewGroupMembers, WelcomeEmailDisabled

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SensitivityLabels
The Sensitivity label to be set to the Microsoft 365 Group. To retrieve the sensitivity label you need to use the Graph API mentioned [here](https://learn.microsoft.com/en-us/graph/api/informationprotectionlabel-get?view=graph-rest-beta&tabs=http).

```yaml
Type: GUID[]
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-post-groups)