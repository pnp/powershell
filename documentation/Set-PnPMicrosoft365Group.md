---
Module Name: PnP.PowerShell
title: Set-PnPMicrosoft365Group
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMicrosoft365Group.html
---
 
# Set-PnPMicrosoft365Group

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Sets Microsoft 365 Group properties

## SYNTAX

```powershell
Set-PnPMicrosoft365Group -Identity <Microsoft365GroupPipeBind> [-DisplayName <String>] [-Description <String>]
 [-Owners <String[]>] [-Members <String[]>] [-IsPrivate] [-LogoPath <String>] [-CreateTeam]
 [-HideFromAddressLists <Boolean>] [-HideFromOutlookClients <Boolean>] [-SensitivityLabels <GUID[]>]
 [<CommonParameters>]
```

## DESCRIPTION

Allows to modify Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMicrosoft365Group -Identity $group -DisplayName "My Displayname"
```

Sets the display name of the group where $group is a Group entity

### EXAMPLE 2
```powershell
Set-PnPMicrosoft365Group -Identity $groupId -Descriptions "My Description" -DisplayName "My DisplayName"
```

Sets the display name and description of a group based upon its ID

### EXAMPLE 3
```powershell
Set-PnPMicrosoft365Group -Identity $group -GroupLogoPath ".\MyLogo.png"
```

Sets a specific Microsoft 365 Group logo

### EXAMPLE 4
```powershell
Set-PnPMicrosoft365Group -Identity $group -IsPrivate:$false
```

Sets a group to be Public if previously Private

### EXAMPLE 5
```powershell
Set-PnPMicrosoft365Group -Identity $group -Owners demo@contoso.com
```

Sets demo@contoso.com as owner of the group

### EXAMPLE 6
```powershell
Set-PnPMicrosoft365Group -Identity $group -SensitivityLabels "bc98af29-59eb-4869-baaa-9a8dff631aa4"
```

Sets the sensitivity label of the group

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
The Description of the group to set

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
The DisplayName of the group to set

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogoPath
The path to the logo file of to set. Logo must be at least 48 pixels wide and may be at most 4 MB in size. Requires Site.ReadWrite.All permissions.

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
The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-update)