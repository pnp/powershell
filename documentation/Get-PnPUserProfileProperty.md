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
Retrieves


**Required Permissions**
To use the `-Account` or `-FromTenantAdminSite` parameters:
* SharePoint: Access to the SharePoint Tenant Administration site

You must connect to the tenant admin website (https://:<tenant>-admin.sharepoint.com) with Connect-PnPOnline in order to use this cmdlet. 

To use without `-Account` or `-FromTenantAdminSite`:
* SharePoint: Access to a site collection
* SharePoint: (App only) `http://sharepoint/social/tenant`, Read

## SYNTAX

```powershell
Get-PnPUserProfileProperty [-FromTenantAdminSite] [-Connection <PnPConnection>] [<CommonParameters>]
Get-PnPUserProfileProperty [-Account] <String[]>] [-Connection <PnPConnection>] [<CommonParameters>]
Get-PnPUserProfileProperty -Claims <String> [-FromTenantAdminSite] [-Connection <PnPConnection>] [<CommonParameters>]
Get-PnPUserProfileProperty -User <UserPipeBind>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Requires a connection to a SharePoint Tenant Admin site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUserProfileProperty
```

Returns the profile properties for the current user.

### EXAMPLE 2
```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com'
```

Returns the profile properties for the specified user

### EXAMPLE 3
```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com','user2@domain.com'
```

Returns the profile properties for the specified users

### EXAMPLE 4
```powershell
Get-PnPUserProfileProperty -Claims 'i:0#.f|membership|user@domain.com'
```

Returns the profile properties for the specified users, in the claims format

### EXAMPLE 2
```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com','user2@domain.com'
```

Returns the profile properties for the specified users

### EXAMPLE 2
```powershell
Get-PnPUserProfileProperty -Account 'user@domain.com','user2@domain.com'
```

Returns the profile properties for the specified users

## PARAMETERS

### -Account
The account of the user, formatted as a login name e.g. user@domain.com

```yaml
Type: String[]
Parameter Sets: By Account (Tenant Admin)

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Claims
The account of the user, formatted as a claims identity, e.g. i:0#.f|membership|user@domain.com

```yaml
Type: String[]
Parameter Sets: By Claims

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -User
A user in the current site, formatted either as a User ID, a display name, or CSOM `User` object retrieved such as returned by `Get-PnPUser`.

```yaml
Type: UserPipeBind
Parameter Sets: By User

Required: False
Position: Named
Accept pipeline input: True
Accept wildcard characters: False
```

### -FromTenantAdminSite
If provided, cmdlet will attempt to connect to and retrieve user profile from tenant admin site.

```yaml
Type: Switch
Parameter Sets: By Claims, Current User

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

## Output

[PersonProperties](https://docs.microsoft.com/en-us/previous-versions/office/sharepoint-csom/jj163873%28v=office.15%29#properties)


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

