---
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Request-PnPPersonalSite.html
title: Request-PnPPersonalSite
Module Name: PnP.PowerShell
schema: 2.0.0
---
  
# Request-PnPPersonalSite

## SYNOPSIS

**Required Permissions**

Access to SharePoint admin site

* SharePoint: AllSites.FullControl and User.ReadWrite.All when using delegated permissions
* SharePoint: Sites.FullControl.All and User.ReadWrite.All when using application permissions

Requests that one or more users be enqueued for a OneDrive for Business site to be created for them.

## SYNTAX

```powershell
Request-PnPPersonalSite -UserEmails <String[]> [-NoWait] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION

The Request-PnPPersonalSite cmdlet requests that the users specified be enqueued so that a Personal Site be created for each. The actual OneDrive for Business site is created by a Timer Job later. If a user already has a Personal Site, the request for that user is silently ignored.

> [!NOTE]
> • A maximum of 200 users can be specified and none of the entries may be empty. The cmdlet stops if it encounters an empty string in the array.<br/><br/>• The account or application running this cmdlet must be assigned at least the SharePoint Administrator role and must have a SharePoint Online license. The users the sites are provisioned for must also have a SharePoint license assigned.<br/><br/>• This only works for users who are allowed to sign in. Requests for users whose sign in is blocked do not result in a Personal Site being created.<br/><br/>• This cmdlet is NOT OneDrive Multi-Geo aware. On Multi-Geo enabled tenants you must run it for users in the region their data is to be hosted in. To retrieve users with a specific PDL, use: `Get-PnPEntraIDUser | Where {$_.PreferredDataLocation -eq "EUR"}`

## EXAMPLES

### EXAMPLE 1
```powershell
Request-PnPPersonalSite -UserEmails @("user1@contoso.com", "user2@contoso.com")
```

This example requests that two users to be enqueued for the creation of a OneDrive for Business Site

### EXAMPLE 2
```powershell
Request-PnPPersonalSite -UserEmails "user1@contoso.com"
```

This example requests that for the provided user a OneDrive for Business site will be created

## PARAMETERS

### -UserEmails

Specifies one or more user logins to be enqueued for the creation of a Personal Site. The Personal site is created by a Timer Job later. You can specify between 1 and 200 users.
> [!NOTE]
> If you are pre-provisioning OneDrive for a large number of users, it might take multiple days for the OneDrive locations to be created.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:
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
Required: False
Position: Named
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

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

