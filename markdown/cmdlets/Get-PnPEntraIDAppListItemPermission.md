---
Module Name: PnP.PowerShell
title: Get-PnPEntraIDAppListItemPermission
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEntraIDAppListItemPermission.html
external help file: PnP.PowerShell.dll-Help.xml
applicable: SharePoint Online
tags: Available in the current Nightly Release only.
schema: 2.0.0
---
   
# Get-PnPEntraIDAppListItemPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Returns Entra ID App permissions for a list item.

## SYNTAX

### All Permissions
```powershell
Get-PnPEntraIDAppListItemPermission -List <String> -ListItem <Int32> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

### By Permission Id
```powershell
Get-PnPEntraIDAppListItemPermission -PermissionId <String> -List <String> -ListItem <Int32> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

### By App Display Name or App Id
```powershell
Get-PnPEntraIDAppListItemPermission -AppIdentity <String> -List <String> -ListItem <Int32> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet returns app permissions for a list item in either the current or a given site. It is used in conjunction with the Entra ID SharePoint application permission `ListItems.SelectedOperations.Selected`.

The `-ListItem` parameter accepts the integer item ID. Use `Get-PnPListItem` to look up the ID if needed.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEntraIDAppListItemPermission -List "Documents" -ListItem 5
```

Returns all app permissions set on the list item with integer id 5 in the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Get-PnPEntraIDAppListItemPermission -List "Documents" -ListItem 5 -Site https://contoso.sharepoint.com/sites/projects
```

Returns all app permissions set on list item 5 in the Documents library of the specified site collection.

### EXAMPLE 4
```powershell
Get-PnPEntraIDAppListItemPermission -List "Documents" -ListItem 5 -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00
```

Returns the specific permission details for the given permission id on the list item.

### EXAMPLE 5
```powershell
Get-PnPEntraIDAppListItemPermission -List "Documents" -ListItem 5 -AppIdentity "My App"
```

Returns the specific permission details for the app with the provided display name on the list item.

### EXAMPLE 6
```powershell
Get-PnPEntraIDAppListItemPermission -List "Documents" -ListItem 5 -AppIdentity "89ea5c94-7736-4e25-95ad-3fa95f62b66e"
```

Returns the specific permission details for the app with the provided app id on the list item.

## PARAMETERS

### -AppIdentity
Specify either the display name or the app id (client id) to filter the returned permissions to a specific app.

```yaml
Type: String
Parameter Sets: By App Display Name or App Id

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
The integer ID of the list item to retrieve permissions for. Use `Get-PnPListItem` to look up the ID if needed.

```yaml
Type: Int32
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PermissionId
If specified, the permission with that id will be retrieved.

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
Optional url of a site to retrieve the permissions for. Defaults to the currently connected site.

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

