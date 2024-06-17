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
Requests that one or more users be enqueued for a OneDrive for Business site to be created for them.

## SYNTAX

```powershell
Request-PnPPersonalSite -UserEmails <String[]> [-NoWait] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION

The Request-PnPPersonalSite cmdlet requests that the users specified be enqueued so that a Personal Site be created for each. The actual OneDrive for Business site is created by a Timer Job later.

> [!NOTE]
> • You must specify a string array of user logins that contains one or more valid user email (logins) and cannot contain any empty fields. The command will stop if it encounters any empty strings in the array. A maximum of 200 users can be specified. <br/><br/>• The actor executing this cmdlet must be at least assigned the SharePoint Online administrator role and have been assigned a SharePoint Online license to be able to provision the OneDrive for Business sites. The users who the sites will be provisioned for must also have a SharePoint license assigned. <br/><br/>•  This cmdlet is NOT OneDrive Multi-Geo aware. If you need to request Personal Sites for Multi-Geo enabled tenants, you must run this cmdlet for users in the region their data is to be hosted in. To retrieve users with a specific PDL, use the following sample: `Get-MSOLUser | Where {$_.PreferredDataLocation -eq "EUR"}`<br/><br/>•  If you want to use this cmdlet in an automated script not requiring manual authentication, you *must* assign the following permission to your application registration from either Azure Active Directory or done through https://tenant-admin.sharepoint.com/_layouts/appregnew.aspx with the following permission through https://tenant-admin.sharepoint.com/_layouts/appinv.aspx:<br/><br/> `
<AppPermissionRequests AllowAppOnlyPolicy="true">
    <AppPermissionRequest Scope="http://sharepoint/social/tenant" Right="FullControl" />
  </AppPermissionRequests>`<br/><br/>You then *must* connect using<br/> `Connect-PnPOnline -Url https://tenant-admin.sharepoint.com -ClientId <clientid> -ClientSecret <clientsecret>`<br/>Authenticating using a certificate is *not* possible and will throw an unauthorized exception. It does not require assigning any permissions in Azure Active Directory.<br/><br/>If you want to run this cmdlet using an interactive login, you *must* connect using:<br/>`Connect-PnPOnline -Url https://tenant-admin.sharepoint.com -UseWebLogin`

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