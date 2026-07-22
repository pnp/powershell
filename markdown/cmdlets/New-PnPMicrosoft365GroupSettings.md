---
Module Name: PnP.PowerShell
title: New-PnPMicrosoft365GroupSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPMicrosoft365GroupSettings.html
---
 
# New-PnPMicrosoft365GroupSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Directory.AccessAsUser.All

Creates a new Microsoft 365 Group setting for a specific group or the tenant

## SYNTAX

```powershell
New-PnPMicrosoft365GroupSettings -Identity <Microsoft365GroupPipeBind> -DisplayName <String> -TemplateId <String> -Values <Hashtable> 
```

## DESCRIPTION

Allows to create a new Microsoft 365 Group setting for a specific group or the tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPMicrosoft365GroupSettings -DisplayName "Group.Unified" -TemplateId "62375ab9-6b52-47ed-826b-58e47e0e304b" -Values @{"GuestUsageGuidelinesUrl"="https://privacy.contoso.com/privacystatement";"EnableMSStandardBlockedWords"="true"}
```

Creates a tenant-wide Microsoft 365 Group setting

### EXAMPLE 2
```powershell
New-PnPMicrosoft365GroupSettings -Identity $groupId -DisplayName "Group.Unified.Guest" -TemplateId "08d542b9-071f-4e16-94b0-74abb372e3d9" -Values @{"AllowToAddGuests"="false"}
```

Creates a Microsoft 365 Group specific setting with all the required properties

## PARAMETERS

### -DisplayName
The Display Name of the Microsoft 365 Group setting. You can get that by using `Get-PnPMicrosoft365GroupSettingTemplates` cmdlet

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -TemplateId
The unique identifier for the template used to create this group of settings. To fetch the values of available templates, use `Get-PnPMicrosoft365GroupSettingTemplates`.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
Hashtable of properties for the settings defined in the templated. Use the syntax @{AllowToAddGuests="false";GuestUsageGuidelinesUrl="https://google.com/privacy"}.

```yaml
Type: Hashtable
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The Identity of the Microsoft 365 Group for which you want to create setting. These settings will override the tenant wide settings.

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
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/groupsetting-post-groupsettings)