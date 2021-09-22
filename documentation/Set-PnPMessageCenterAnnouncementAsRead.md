---
Module Name: PnP.PowerShell
title: Set-PnPMessageCenterAnnouncementAsRead
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMessageCenterAnnouncementAsRead.html
---
 
# Set-PnPMessageCenterAnnouncementAsRead

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessageViewpoint.Write (delegated)

Marks one or multiple message center announcements of the Office 365 Services as read

## SYNTAX

```powershell
Set-PnPMessageCenterAnnouncementAsRead [-Identity <Id>] [-Identities <Ids>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMessageCenterAnnouncementAsRead -Identity "MC123456"
```

Marks message center announcement MC123456 as read for the current user

### EXAMPLE 2
```powershell
Set-PnPMessageCenterAnnouncementAsRead -Identities @("MC123456", "MC234567")
```

Marks message center announcements MC123456 and MC234567 as read for the current user

### EXAMPLE 3
```powershell
Set-PnPMessageCenterAnnouncementAsRead
```

Marks all message center announcements as read for the current user

## PARAMETERS

### -Identity
Marks a particular message center announcement with the provided Id as read
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
Marks the message center announcements with the provided Ids as read
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