---
Module Name: PnP.PowerShell
title: Set-PnPMessageCenterAnnouncementAsUnread
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMessageCenterAnnouncementAsUnread.html
---
 
# Set-PnPMessageCenterAnnouncementAsUnread

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessageViewpoint.Write (delegated)

Marks one or multiple message center announcements of the Office 365 Services as unread.

## SYNTAX

```powershell
Set-PnPMessageCenterAnnouncementAsUnread [-Identity <String[]>] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet marks one or multiple message center announcements of the Office 365 Services as unread for the current user.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMessageCenterAnnouncementAsUnread -Identity "MC123456"
```

Marks message center announcement MC123456 as unread for the current user.

### EXAMPLE 2
```powershell
Set-PnPMessageCenterAnnouncementAsUnread -Identity "MC123456", "MC234567"
```

Marks message center announcements MC123456 and MC234567 as unread for the current user.

### EXAMPLE 3
```powershell
Set-PnPMessageCenterAnnouncementAsUnread
```

Marks all message center announcements as unread for the current user.

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
Id(s) of the message center announcements to mark as unread.

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
