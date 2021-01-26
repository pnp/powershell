---
Module Name: PnP.PowerShell
title: Get-PnPUserProfileProperty
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUserProfileProperty.html
---
 
# Get-PnPUserProfileProperty

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

You must connect to the tenant admin website (https://:<tenant>-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet. 

## SYNTAX

```powershell
Get-PnPUserProfileProperty [-Account] <String[]> [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Requires a connection to a SharePoint Tenant Admin site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com'
```

Returns the profile properties for the specified user

### EXAMPLE 2
```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com','user2@domain.com'
```

Returns the profile properties for the specified users

## PARAMETERS

### -Account
The account of the user, formatted either as a login name, or as a claims identity, e.g. i:0#.f|membership|user@domain.com

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

