---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPMessageCenterAnnouncement.html
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPMessageCenterAnnouncement
Module Name: PnP.PowerShell
---
  
# Get-PnPMessageCenterAnnouncement

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceMessage.Read.All

Gets message center announcements of the Office 365 Services from the Microsoft Graph API

## SYNTAX

```powershell
Get-PnPMessageCenterAnnouncement [-Identity <Id>] 
```

## DESCRIPTION

Allows to retrieve the available message center announcements.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPMessageCenterAnnouncement
```

Retrieves all the available message center announcements

### EXAMPLE 2
```powershell
Get-PnPMessageCenterAnnouncement -Identity "MC123456"
```

Retrieves the details of the message center announcement with the Id MC123456

## PARAMETERS

### -Identity
Allows retrieval of a particular message center announcement with the provided Id
```yaml
Type: Identity
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

