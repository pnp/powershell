---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupSettings.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPMicrosoft365GroupSettings
---
  
# Get-PnPMicrosoft365GroupSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Directory.Read.All

Gets a settings of a specific Microsoft 365 Group or a tenant wide Microsoft 365 Group Settings.

## SYNTAX

```powershell
Get-PnPMicrosoft365GroupSettings [-Identity <Microsoft365GroupPipeBind>] [-GroupSetting <Microsoft365GroupSettingsPipeBind>] 
```

## DESCRIPTION

Allows to retrieve tenant wide settings of Microsoft 365 Groups or by using `Identity` option you may specify the exact Microsoft 365 Group.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365GroupSettings
```

Retrieves all the Microsoft 365 Group settings from the Tenant

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365GroupSettings -Identity $groupId
```

Retrieves a specific Microsoft 365 Group setting


### EXAMPLE 3
```powershell
Get-PnPMicrosoft365GroupSettings -GroupSetting $groupSettingId
```

Retrieves a tenant-wide specific Microsoft 365 Group setting.


### EXAMPLE 4
```powershell
Get-PnPMicrosoft365GroupSettings -Identity $groupId -GroupSetting $groupSettingId 
```

Retrieves a group-specific Microsoft 365 Group setting

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -GroupSetting
The Identity of the Microsoft 365 Group Setting

```yaml
Type: Microsoft365GroupSettingsPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation get settings](https://learn.microsoft.com/graph/api/groupsetting-get)
[Microsoft Graph documentation list settings](https://learn.microsoft.com/en-gb/graph/api/group-list-setting)


