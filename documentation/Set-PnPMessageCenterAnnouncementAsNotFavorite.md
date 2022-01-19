---
Module Name: PnP.PowerShell
title: Set-PnPMessageCenterAnnouncementAsNotFavorite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMessageCenterAnnouncementAsNotFavorite.html
---
 
# Set-PnPMessageCenterAnnouncementAsNotFavorite

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessageViewpoint.Write (delegated)

Marks one or multiple message center announcements of the Office 365 Services as not favorite

## SYNTAX

```powershell
Set-PnPMessageCenterAnnouncementAsNotFavorite [-Identity <Id>] [-Identities <Ids>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMessageCenterAnnouncementAsNotFavorite -Identity "MC123456"
```

Marks message center announcement MC123456 as not favorite for the current user

### EXAMPLE 2
```powershell
Set-PnPMessageCenterAnnouncementAsNotFavorite -Identities @("MC123456", "MC234567")
```

Marks message center announcements MC123456 and MC234567 as not favorite for the current user

### EXAMPLE 3
```powershell
Set-PnPMessageCenterAnnouncementAsNotFavorite
```

Marks all message center announcements as not favorite for the current user

## PARAMETERS

### -Identity
Marks a particular message center announcement with the provided Id as not favorite
```yaml
Type: String
Parameter Sets: Single

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identities
Marks the message center announcements with the provided Ids as not favorite
```yaml
Type: String[]
Parameter Sets: Multiple

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)