---
Module Name: PnP.PowerShell
title: Set-PnPMicrosoft365GroupSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMicrosoft365GroupSettings.html
---
 
# Set-PnPMicrosoft365GroupSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Directory.ReadWrite.All, Directory.AccessUser.All

Updates Microsoft 365 Group settings for the tenant or specified Group.

## SYNTAX

```powershell
Set-PnPMicrosoft365GroupSettings -Identity <string> [-Values <Hashtable>] [-Group <Microsoft365GroupPipeBind>]
 [<CommonParameters>]
```

## DESCRIPTION

Allows to modify Microsoft 365 Group settings for tenat or specified group.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMicrosoft365GroupSettings -Identity $groupSettingId -Values @{"AllowToAddGuests"="true"}
```

Sets the properties of the setting where $groupSettingId is a Group setting ID. You can get the Group setting using the `Get-PnPMicrosoft365GroupSettings` cmdlet.

### EXAMPLE 2
```powershell
Set-PnPMicrosoft365GroupSettings -Identity $groupSettingId -Values @{"AllowToAddGuests"="true"} -Group $groupId
```

Sets the properties of the Microsoft 365 group specific setting where $groupSettingId is a setting ID of that group. You can get the Group setting using the `Get-PnPMicrosoft365GroupSettings -Group` cmdlet.

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group setting

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
Hashtable of properties for the settings. Use the syntax @{AllowToAddGuests="false";GuestUsageGuidelinesUrl="https://google.com/privacy"}.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
The Identity of the Microsoft 365 Group for which you want to update setting.

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/groupsetting-update)