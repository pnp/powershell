---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
title: Grant-PnPEntraIDAppListPermission
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPEntraIDAppListPermission.html
Module Name: PnP.PowerShell
---
   
# Grant-PnPEntraIDAppListPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Adds permissions for a given Entra ID application registration on a list.

## SYNTAX

```powershell
Grant-PnPEntraIDAppListPermission -AppId <Guid> -DisplayName <String> -Permissions <Read|Write|Owner|FullControl> -List <String> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet adds permissions for a given Entra ID application registration on a list.

The list can be identified by its GUID or display name.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPEntraIDAppListPermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Read -List "Documents"
```

Grants the Entra ID application registration Read access on the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Grant-PnPEntraIDAppListPermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Write -List "12345678-1234-1234-1234-123456789012"
```

Grants Write access on the list identified by its GUID in the currently connected site.

### EXAMPLE 3
```powershell
Grant-PnPEntraIDAppListPermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Owner -List "Documents" -Site https://contoso.sharepoint.com/sites/projects
```

Grants Owner access on the Documents library of the specified site collection.

## PARAMETERS

### -AppId
The app id (client id) of the Entra ID application registration to grant permission for.

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
The display name to associate with the permission. Used for visual reference only; does not need to match the application name in Entra ID.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -List
The list to grant permissions on. Accepts a list GUID or display name.

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
The permissions to grant for the Entra ID application registration. Can be Read, Write, Owner, or FullControl.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Accepted values: Read, Write, Owner, FullControl
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Optional url of a site to grant the permissions on. Defaults to the currently connected site.

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

