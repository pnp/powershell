---
Module Name: PnP.PowerShell
title: Set-PnPSubscribeSharePointNewsDigest
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSubscribeSharePointNewsDigest.html
---
 
# Set-PnPSubscribeSharePointNewsDigest

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

You must connect to the tenant admin website (https://tenant-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet.

Enables or disables the SharePoint News Digest mails for a particular user. Note that the disabling option is still experimental and may not work and may be removed again in the future.

Note: The implementation behind this in SharePoint Online has changed causing this cmdlet to no longer work. Unfortunately there's no alternative way to call into this functionality from PnP PowerShell. We therefore have to remove this cmdlet in a future version. At present it does not work anymore.

## SYNTAX

```powershell
Set-PnPSubscribeSharePointNewsDigest -Account <String> -Enabled <Boolean> [-Connection <PnPConnection>] 
```

## DESCRIPTION
Requires a connection to a SharePoint Tenant Admin site.

Enables or disables the SharePoint News Digest mails for a particular user.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSubscribeSharePointNewsDigest -Account 'user@domain.com' -Enabled:$true
```

Enables the user user@domain.com for receiving the SharePoint News Digest e-mails.

### EXAMPLE 2
```powershell
Set-PnPSubscribeSharePointNewsDigest -Account 'user@domain.com' -Enabled:$false
```

Stops the user user@domain.com from receiving the SharePoint News Digest e-mails.

## PARAMETERS

### -Account
The account of the user, formatted either as a login name, e.g. user@domain.com, or as a claims identity, e.g. i:0#.f|membership|user@domain.com

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enabled
Boolean indicating if the user should receive the SharePoint News Digest e-mails

```yaml
Type: Boolean
Parameter Sets: (All)

Required: True
Position: 1
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

