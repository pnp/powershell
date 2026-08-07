---
Module Name: PnP.PowerShell
title: Set-PnPMessageCenterAnnouncementAsFavorite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMessageCenterAnnouncementAsFavorite.html
---
 
# Set-PnPMessageCenterAnnouncementAsFavorite

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessageViewpoint.Write (delegated)

Marks one or multiple message center announcements of the Office 365 Services as favorite$.

## SYNTAX

```powershell
Set-PnPMessageCenterAnnouncementAsFavorite [-Identity <Ids>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to mark message center announcements as favorite.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMessageCenterAnnouncementAsFavorite -Identity "MC123456"
```

Marks message center announcement MC123456 as favorite for the current user.

### EXAMPLE 2
```powershell
Set-PnPMessageCenterAnnouncementAsFavorite -Identity "MC123456", "MC234567"
```

Marks message center announcements MC123456 and MC234567 as favorite for the current user.

### EXAMPLE 3
```powershell
Set-PnPMessageCenterAnnouncementAsFavorite
```

Marks all message center announcements as favorite for the current user.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
Marks a particular message center announcement or announcements with the provided Ids as favorite.
```yaml
Type: String[]
Parameter Sets: None

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)