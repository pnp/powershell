---
Module Name: PnP.PowerShell
title: Request-PnPPersonalSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Request-PnPPersonalSite.html
---
 
# Request-PnPPersonalSite

## SYNOPSIS
Requests that one or more users be enqueued for a Personal Site to be created.

## SYNTAX

```powershell
Request-PnPPersonalSite -UserEmails <String[]> [-NoWait]
```

## DESCRIPTION

The Request-PnPPersonalSite cmdlet requests that the users specified be enqueued so that a Personal Site be created for each. The actual Personal site is created by a Timer Job later.

> [!NOTE]
> • You must specify a string array of user logins that contains one or more valid user email (logins) and cannot contain any empty fields. The command will stop if it encounters any empty strings in the array. A maximum of 200 users can be specified. <br/><br/>• The actor executing this cmdlet must be at least assigned the SharePoint Online administrator role and have been assigned a SharePoint Online license to be able to provision the OneDrive for Business sites. The users who the sites will be provisioned for must also have a SharePoint license assigned. <br/><br/>•  This cmdlet is NOT OneDrive Multi-Geo aware. If you need to request Personal Sites for Multi-Geo enabled tenants, you must run this cmdlet for users in the region their data is to be hosted in. To retrieve users with a specific PDL, use the following sample: `Get-MSOLUser | Where {$_.PreferredDataLocation -eq "EUR"}`.

## EXAMPLES

### EXAMPLE 1
```powershell
$emails = "user1@contoso.com", "user2@contoso.com"
Request-PnPPersonalSite -UserEmails $emails
```

This example requests that two users to be enqueued for the creation of a Personal Site.

### EXAMPLE 2
```powershell
Request-PnPPersonalSite -UserEmails $emails
```

This example requests that many users to be queued for the creation of a Personal Site. The users are previously defined using the variable $emails.

## PARAMETERS

### -UserEmails

Specifies one or more user logins to be enqueued for the creation of a Personal Site. The Personal site is created by a Timer Job later. You can specify between 1 and 200 users.
> [!NOTE]
> If you are Pre-Provisioning OneDrive for many users, it might take up to 24 hours for the OneDrive locations to be created. If a user's OneDrive isn't ready after 24 hours, please contact Support.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:
Applicable: SharePoint Online

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -NoWait

Continues without the status being polled. Polling the action can slow its progress if lots of user emails are specified.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:
Applicable: SharePoint Online

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

