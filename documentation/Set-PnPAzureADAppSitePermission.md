---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPAzureADAppSitePermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPAzureADAppSitePermission
---
  
# Set-PnPAzureADAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All

Updates permissions for a given app.

## SYNTAX

```powershell
Set-PnPAzureADAppSitePermission -PermissionId <String> -Permissions <"Read"|"Write"|"Manage"|"FullControl"> [-Site <SitePipeBind>]
```

## DESCRIPTION

This cmdlets updates permissions for a given app in a site.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAzureADAppSitePermission -PermissionId ABSDFefsdfef33fsdFSvsadf3e3fsdaffsa -Permissions Read
```

Updates the app with the specific permission id and sets the rights to 'Read' access.

## PARAMETERS

### -PermissionId
If specified the permission with that id specified will be retrieved

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Permissions
Specifies the permissions to set for the app. 

```yaml
Type: String
Parameter Sets: (All)

Required: True
Accepted values: Write, Read, Manage, FullControl
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Optional url to to a site to set the permissions for. Defaults to the current site.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
