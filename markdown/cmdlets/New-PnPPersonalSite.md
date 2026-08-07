---
Module Name: PnP.PowerShell
title: New-PnPPersonalSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPPersonalSite.html
---
 
# New-PnPPersonalSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

## SYNTAX

```powershell
New-PnPPersonalSite [-Email] <String[]> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Creates a OneDrive For Business site for the provided user(s)

If you want to use this cmdlet in an automated script not requiring manual authentication, you *must* assign the following permission to your application registration from either Azure Active Directory or done through https://tenant-admin.sharepoint.com/_layouts/appregnew.aspx with the following permission through https://tenant-admin.sharepoint.com/_layouts/appinv.aspx:

`
<AppPermissionRequests AllowAppOnlyPolicy="true">
  <AppPermissionRequest Scope="http://sharepoint/social/tenant" Right="FullControl" />
</AppPermissionRequests>
`

You then *must* connect using:

`
Connect-PnPOnline -Url https://tenant-admin.sharepoint.com -ClientId <clientid> -ClientSecret <clientsecret>
`

Authenticating using a certificate is *not* possible and will throw an unauthorized exception. It does not require assigning any permissions in Azure Active Directory.

If you want to run this cmdlet using an interactive login, you *must* connect using:

`
Connect-PnPOnline -Url https://tenant-admin.sharepoint.com -UseWebLogin
`

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

