---
Module Name: PnP.PowerShell
title: Set-PnPMessageCenterAnnouncementAsArchived
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMessageCenterAnnouncementAsArchived.html
---
 
# Set-PnPMessageCenterAnnouncementAsArchived

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessageViewpoint.Write (delegated)

Marks one or multiple message center announcements of the Office 365 Services as archived

## SYNTAX

```powershell
Set-PnPMessageCenterAnnouncementAsArchived [-Identity <Ids>] 
```

## DESCRIPTION

Allows to mark message center announcements as archived.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPMessageCenterAnnouncementAsArchived -Identity "MC123456"
```

Marks message center announcement MC123456 as archived for the current user

### EXAMPLE 2
```powershell
Set-PnPMessageCenterAnnouncementAsArchived -Identity "MC123456", "MC234567"
```

Marks message center announcements MC123456 and MC234567 as archived for the current user

### EXAMPLE 3
```powershell
Set-PnPMessageCenterAnnouncementAsArchived
```

Marks all message center announcements as archived for the current user

## PARAMETERS

### -Identity
Marks the message center announcement or announcements with the provided Ids as archived

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