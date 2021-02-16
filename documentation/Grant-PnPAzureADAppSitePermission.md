---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPAzureADAppSitePermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Grant-PnPAzureADAppSitePermission
---
  
# Grant-PnPAzureADAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All

Grants an Azure AD App registration, which has the "Sites.Selected" permission scope set, access to a site.

## SYNTAX

```powershell
Grant-PnPAzureADAppSitePermission -AppId <Guid> -DisplayName <String> -Permissions <Read|Write> [-Site <SitePipeBind>]
```

## DESCRIPTION

This cmdlet grants a specific Azure AD App registration access to a site. The app requires to have the "Sites.Selected" permission scope set.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPAzureADAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Write
```

Grants the app with the specified ID write access to the current site that has been connected to with Connect-PnPOnline.

### EXAMPLE 2
```powershell
Grant-PnPAzureADAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Write -Site https://contoso.sharepoint.com/sites/projects
```

Grants the app with the specified ID write access to specified site.

## PARAMETERS

### -AppId
If specified the permission with that id specified will be retrieved

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name to set for the app in the site.

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
Accepted values: Write, Read
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


