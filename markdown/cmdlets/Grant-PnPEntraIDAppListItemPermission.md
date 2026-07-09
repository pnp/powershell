---
Module Name: PnP.PowerShell
title: Grant-PnPEntraIDAppListItemPermission
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPEntraIDAppListItemPermission.html
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
---
   
# Grant-PnPEntraIDAppListItemPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Adds permissions for a given Entra ID application registration on a list item.

## SYNTAX

```powershell
Grant-PnPEntraIDAppListItemPermission -AppId <Guid> -DisplayName <String> -Permissions <Read|Write|Owner|FullControl> -List <String> -ListItem <Int32> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet adds permissions for a given Entra ID application registration on a list item. It is used in conjunction with the Entra ID SharePoint application permission `ListItems.SelectedOperations.Selected`.

The `-ListItem` parameter accepts the integer item ID. Use `Get-PnPListItem` to look up the ID if needed.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPEntraIDAppListItemPermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Read -List "Documents" -ListItem 5
```

Grants the Entra ID application registration Read access on list item 5 in the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Grant-PnPEntraIDAppListItemPermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Owner -List "Documents" -ListItem 5 -Site https://contoso.sharepoint.com/sites/projects
```

Grants Owner access on list item 5 in the Documents library of the specified site collection.

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
The list containing the item. Accepts a list GUID or display name.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListItem
The integer ID of the list item to grant permissions on. Use `Get-PnPListItem` to look up the ID if needed.

```yaml
Type: Int32
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

