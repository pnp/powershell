---
external help file: PnP.PowerShell.dll-Help.xml
applicable: SharePoint Online
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPEntraIDAppListItemPermission.html
schema: 2.0.0
tags: Available in the current Nightly Release only.
title: Set-PnPEntraIDAppListItemPermission
---
   
# Set-PnPEntraIDAppListItemPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Updates permissions for a given Entra ID application registration on a list item.

## SYNTAX

```powershell
Set-PnPEntraIDAppListItemPermission -PermissionId <String> -Permissions <Read|Write|Owner|FullControl> -List <String> -ListItem <Int32> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet updates an existing permission for an Entra ID application registration on a list item. It is used in conjunction with the Entra ID SharePoint application permission `ListItems.SelectedOperations.Selected`.

Use [Get-PnPEntraIDAppListItemPermission](Get-PnPEntraIDAppListItemPermission.md) to retrieve the `PermissionId` required by this cmdlet.

The `-ListItem` parameter accepts the integer item ID. Use `Get-PnPListItem` to look up the ID if needed.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPEntraIDAppListItemPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -Permissions Read -List "Documents" -ListItem 5
```

Updates the permission to Read access on list item 5 in the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Set-PnPEntraIDAppListItemPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -Permissions Write -List "Documents" -ListItem 5 -Site https://contoso.sharepoint.com/sites/projects
```

Updates the permission to Write access on list item 5 in the Documents library of the specified site collection.

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
The integer ID of the list item whose permission should be updated. Use `Get-PnPListItem` to look up the ID if needed.

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
The id of the permission to update. Use [Get-PnPEntraIDAppListItemPermission](Get-PnPEntraIDAppListItemPermission.md) to retrieve the id.

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
The updated permissions for the Entra ID application registration. Can be Read, Write, Owner, or FullControl.

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
Optional url of a site to update the permissions on. Defaults to the currently connected site.

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

