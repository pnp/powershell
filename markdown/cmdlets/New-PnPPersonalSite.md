---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPPersonalSite.html
tags: Available in the current Nightly Release only.
title: New-PnPPersonalSite
---
  
# New-PnPPersonalSite

## SYNOPSIS

**Required Permissions**

Access to SharePoint admin site

* SharePoint: AllSites.FullControl and User.ReadWrite.All when using delegated permissions
* SharePoint: Sites.FullControl.All and User.ReadWrite.All when using application permissions

## SYNTAX

```powershell
New-PnPPersonalSite [-Email] <String[]> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates a OneDrive For Business site for the provided user(s). The site is enqueued and actually created by a Timer Job later, so it will not exist yet when this cmdlet returns. If a user already has a OneDrive for Business site, the request for that user is silently ignored.

> [!NOTE]
> • The account or application running this cmdlet must be assigned at least the SharePoint Administrator role and must have a SharePoint Online license. The users the sites are provisioned for must also have a SharePoint license assigned.<br/><br/>• This only works for users who are allowed to sign in. Requests for users whose sign in is blocked do not result in a OneDrive for Business site being created.<br/><br/>• When pre-provisioning for a large number of users, it might take multiple days for the OneDrive locations to be created.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPPersonalSite -Email @('katiej@contoso.onmicrosoft.com','garth@contoso.onmicrosoft.com')
```

Creates a OneDrive For Business site for the provided two users

## PARAMETERS

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

### -Email
The UserPrincipalName (UPN) of the users

```yaml
Type: String[]
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


