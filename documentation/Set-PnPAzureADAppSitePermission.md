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

Updates permissions for a given Azure Active Directory application registration.

## SYNTAX

```powershell
Set-PnPAzureADAppSitePermission -PermissionId <String> -Permissions <Read|Write|Manage|FullControl> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet updates permissions for a given Azure Active Directory application registration in a site collection. It is used in conjunction with the Azure Active Directory SharePoint application permission Sites.Selected. Notice that this cmdlet allows for more permissions compared for when initially setting rights through [Grant-PnPAzureADAppSitePermission](Grant-PnPAzureADAppSitePermission.md).

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPAzureADAppSitePermission -PermissionId ABSDFefsdfef33fsdFSvsadf3e3fsdaffsa -Permissions Read
```

Updates the Azure Active Directory application registration with the specific permission id and sets the rights to 'Read' access for the currently connected site collection.

### EXAMPLE 2
```powershell
Set-PnPAzureADAppSitePermission -PermissionId ABSDFefsdfef33fsdFSvsadf3e3fsdaffsa -Permissions FullControl -Site https://contoso.microsoft.com/sites/projects
```

Updates the Azure Active Directory application registration with the specific permission id and sets the rights to 'FullControl' access for the site collection at the provided URL.

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

### -PermissionId
The permission with the specified id will be updated. Use [Get-PnPAzureADAppSitePermission](Get-PnPAzureADAppSitePermission.md) to discover currently set permissions which can be updated.

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
Specifies the permissions to set for the Azure Active Directory application registration which can either be Read, Write, Manage or FullControl.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Accepted values: Read, Write, Manage, FullControl
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Optional url of a site to set the permissions for. Defaults to the current site if not provided.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: Currently connected site
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
