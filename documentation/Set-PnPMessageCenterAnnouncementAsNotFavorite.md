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
Set-PnPMessageCenterAnnouncementAsNotFavorite [-Identity <Ids>] 
```

## DESCRIPTION

Allows to mark message center announcements as not favorite.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMessageCenterAnnouncementAsNotFavorite -Identity "MC123456"
```

Marks message center announcement MC123456 as not favorite for the current user

### EXAMPLE 2
```powershell
Set-PnPMessageCenterAnnouncementAsNotFavorite -Identity "MC123456", "MC234567"
```

Marks message center announcements MC123456 and MC234567 as not favorite for the current user

### EXAMPLE 3
```powershell
Set-PnPMessageCenterAnnouncementAsNotFavorite
```

Marks all message center announcements as not favorite for the current user

## PARAMETERS

### -Identity
Marks the message center announcement or announcements with the provided Ids as not favorite
```yaml
Type: String[]
Parameter Sets: None

Required: false
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)