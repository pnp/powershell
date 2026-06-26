---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPEntraIDAppSitePermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Grant-PnPEntraIDAppSitePermission
---
  
# Grant-PnPEntraIDAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All (Delegated)

Adds permissions for a given Entra ID application registration.

## SYNTAX

```powershell
Grant-PnPEntraIDAppSitePermission -AppId <Guid> -DisplayName <String> -Permissions <Read|Write|Manage|FullControl> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet adds permissions for a given Entra ID application registration in a site collection. It is used in conjunction with the Entra ID SharePoint application permission Sites.Selected.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPEntraIDAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Read
```

Adds permissions for the Entra ID application registration with the specific application id and sets the rights to 'Read' access for the currently connected site collection.

### EXAMPLE 2
```powershell
Grant-PnPEntraIDAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions FullControl -Site https://contoso.sharepoint.com/sites/projects
```

Adds permissions for the Entra ID application registration with the specific application id and sets the rights to 'FullControl' access for the site collection at the provided URL.

## PARAMETERS

### -AppId
Specify the AppId of the Entra ID application registration to grant permission for.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
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

### -DisplayName
The display name to set for the application permission you're adding. Only for visual reference purposes, does not need to match the name of the application in Entra ID.

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
Specifies the permissions to set for the Entra ID application registration which can either be Read, Write, Manage or FullControl.

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

Required: True
Position: Named
Default value: Currently connected site
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
