---
Module Name: PnP.PowerShell
title: Get-PnPTemporarilyDisableAppBar
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTemporarilyDisableAppBar.html
---
 
# Get-PnPTemporarilyDisableAppBar

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the disabled state of the SharePoint Online App Bar. It may take some time after changing this setting for the change to be reflected in SharePoint Online and for this cmdlet to return the updated value. Support for this may be dropped after March 31st, 2023 after which the SharePoint Online App Bar will become visible anyway. See the [Message Center Announcement](https://admin.microsoft.com/Adminportal/Home#/MessageCenter/:/messages/MC428505) for more information.

## SYNTAX

```powershell
Get-PnPTemporarilyDisableAppBar 
```

## DESCRIPTION

Allows to retrieve disabled state of the SharePoint Online App Bar.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTemporarilyDisableAppBar
```

Returns True if the the SharePoint Online App Bar is hidden or False if it is not.

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

