---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADAppSitePermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPAzureADAppSitePermission
---
  
# Get-PnPAzureADAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All

Returns Azure AD App permissions for a site

## SYNTAX

### All Permissions
```powershell
Get-PnPAzureADAppSitePermission [-PermissionId <String>] [-Site <SitePipeBind>]
```

### By Permission Id
```powershell
Get-PnPAzureADAppSitePermission -PermissionId <String> [-Site <SitePipeBind>]
```

### By App Display Name
```powershell
Get-PnPAzureADAppSitePermission -AppIdentity <String> [-Site <SitePipeBind>]
```

## DESCRIPTION

This cmdlet returns app permissions for either the current or a given site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPAzureADAppSitePermission
```

Returns the apps that have permissions for the currently connected to site. Notice that if PermissionId is not specified the Roles property is not populated. This is a current API limitation. 

### EXAMPLE 2
```powershell
Get-PnPAzureADAppSitePermissions -Site https://contoso.sharepoint.com/sites/projects
```

Returns the apps that have permissions for the site specified. Notice that you require to have the SharePoint Administrator role in your tenant to be able to use this.

### EXAMPLE 3
```powershell
Get-PnPAzureADAppSitePermission -PermissionId TowaS50fG1zLnNwLmV4dHwxYxNmI0OTI1
```

Returns the specific app details given the permission id for the current site.

### EXAMPLE 4
```powershell
Get-PnPAzureADAppSitePermission -AppIdentity "Test App"
```

### EXAMPLE 5
```powershell
Get-PnPAzureADAppSitePermission -AppIdentity "14effc36-dc8b-4f68-8919-f6beb7d847b3"
```

Returns the specific app details given the id of the app specified

## PARAMETERS

### -PermissionId
If specified the permission with that id specified will be retrieved

```yaml
Type: String
Parameter Sets: By Permission Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AppIdentity
You can specify either the Display Name or the AppId to specifically retrieve the permission for.

```yaml
Type: String
Parameter Sets: By Permission Id

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Optional url to to a site to retrieve the permissions for. Defaults to the current site.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


