---
Module Name: PnP.PowerShell
title: Reset-PnPUserOneDriveQuotaToDefault
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Reset-PnPUserOneDriveQuotaToDefault.html
---
 
# Reset-PnPUserOneDriveQuotaToDefault

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Resets the current quota set on the OneDrive for Business site for a specific user to the tenant default

## SYNTAX

```powershell
Reset-PnPUserOneDriveQuotaToDefault [-Account] <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command allows you to reset the quota set on the OneDrive for Business site of a specific user to the default as set on the tenant. You must connect to the tenant admin website (https://:<tenant>-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Reset-PnPUserOneDriveQuotaToDefault -Account 'user@domain.com'
```

Resets the quota set on the OneDrive for Business site for the specified user to the tenant default

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

