---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMicrosoft365GroupSettingTemplates.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPMicrosoft365GroupSettingTemplates
---
  
# Get-PnPMicrosoft365GroupSettingTemplates

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Directory.Read.All

Gets the available system wide template of settings for Microsoft 365 Groups.

## SYNTAX

```powershell
Get-PnPMicrosoft365GroupSettingTemplates [-Identity <string>] [<CommonParameters>]
```

## DESCRIPTION

Allows to retrieve available system wide template of settings for Microsoft 365 Groups.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMicrosoft365GroupSettingTemplates
```

Retrieves all the available Microsoft 365 Group setting templates from the Tenant

### EXAMPLE 2
```powershell
Get-PnPMicrosoft365GroupSettingTemplates -Identity "08d542b9-071f-4e16-94b0-74abb372e3d9"
```

Retrieves a specific Microsoft 365 Group setting template. In the above example, it retrieves the `Group.Unified.Guest` setting template.

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group setting template

```yaml
Type: string
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/groupsettingtemplate-list)


