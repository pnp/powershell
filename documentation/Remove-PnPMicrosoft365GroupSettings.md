---
Module Name: PnP.PowerShell
title: Remove-PnPMicrosoft365GroupSettings
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365GroupSettings.html
---
 
# Remove-PnPMicrosoft365GroupSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Directory.ReadWrite.All , Directory.AccessAsUser.All

Removes Microsoft 365 Group settings from the tenant or the specified Microsoft 365 Group.

## SYNTAX

```powershell
Remove-PnPMicrosoft365GroupSettings -Identity <string> -Group <Microsoft365GroupPipeBind>  [<CommonParameters>]
```

## DESCRIPTION

Allows to remove Microsoft 365 Group settings from the tenant or the specified group.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPMicrosoft365GroupSettings -Identity "10f686b9-9deb-4ad8-ba8c-1f9b7a00a22b"
```

Removes a tenant wide Microsoft 365 Group setting based on its ID. You can get the ID of the setting using `Get-PnPMicrosoft365GroupSettings` cmdlet.

### EXAMPLE 2
```powershell
Remove-PnPMicrosoft365GroupSettings -Identity "10f686b9-9deb-4ad8-ba8c-1f9b7a00a22b" -Group $groupId
```

Removes the Microsoft 365 Group setting with Id from the specified group. You can get the ID of the setting using `Get-PnPMicrosoft365GroupSettings -Group` cmdlet.

## PARAMETERS

### -Identity
The Identity of the Microsoft 365 Group setting

```yaml
Type: string
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Group
The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Microsoft Graph documentation](https://learn.microsoft.com/graph/api/groupsetting-delete)

