---
Module Name: PnP.PowerShell
title: Get-PnPUserOneDriveQuota
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUserOneDriveQuota.html
---
 
# Get-PnPUserOneDriveQuota

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Retrieves the current quota set on the OneDrive for Business site for a specific user in bytes.

## SYNTAX

```powershell
Get-PnPUserOneDriveQuota [-Account] <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command allows you to request the quota set on the OneDrive for Business site of a specific user.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUserOneDriveQuota -Account 'user@domain.com'
```

Returns the quota set on the OneDrive for Business site for the specified user in bytes

### EXAMPLE 2
```powershell
(Get-PnPUserOneDriveQuota -Account 'user@domain.com') / 1gb
```

Returns the quota set on the OneDrive for Business site for the specified user in gigabytes

## PARAMETERS

### -Account
The account of the user, formatted either as a login name, or as a claims identity, e.g. i:0#.f|membership|user@domain.com

```yaml
Type: String
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