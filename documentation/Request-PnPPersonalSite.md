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

The Request-PnPPersonalSite cmdlet requests that the users specified be enqueued so that a Personal Site be created for each. The actual OneDrive for Business site is created by a Timer Job later.


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
> If you are Pre-Provisioning OneDrive for many users, it might take up to 24 hours for the OneDrive locations to be created. If a user's OneDrive isn't ready after 24 hours, please contact Support.

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