---
Module Name: PnP.PowerShell
schema: 2.0.0
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
title: Revoke-PnPEntraIDAppListItemPermission
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Revoke-PnPEntraIDAppListItemPermission.html
---
   
# Revoke-PnPEntraIDAppListItemPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Revokes permissions for a given Entra ID application registration on a list item.

## SYNTAX

```powershell
Revoke-PnPEntraIDAppListItemPermission -PermissionId <String> -List <String> -ListItem <Int32> [-Site <SitePipeBind>] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet revokes an existing permission for an Entra ID application registration on a list item. It is used in conjunction with the Entra ID SharePoint application permission `ListItems.SelectedOperations.Selected`.

Use [Get-PnPEntraIDAppListItemPermission](Get-PnPEntraIDAppListItemPermission.md) to retrieve the `PermissionId` required by this cmdlet.

The `-ListItem` parameter accepts the integer item ID. Use `Get-PnPListItem` to look up the ID if needed.

## EXAMPLES

### EXAMPLE 1
```powershell
Revoke-PnPEntraIDAppListItemPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents" -ListItem 5
```

Revokes the permission with the specified id on list item 5 in the Documents library of the currently connected site. A confirmation prompt will be shown before the permission is removed.

### EXAMPLE 2
```powershell
Revoke-PnPEntraIDAppListItemPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents" -ListItem 5 -Site https://contoso.sharepoint.com/sites/projects -Force
```

Revokes the permission on list item 5 in the Documents library of the specified site collection without prompting for confirmation.

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

### -Force
When specified, no confirmation prompt will be shown before revoking the permission.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: False
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
The integer ID of the list item from which to revoke the permission. Use `Get-PnPListItem` to look up the ID if needed.

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
The id of the permission to revoke. Use [Get-PnPEntraIDAppListItemPermission](Get-PnPEntraIDAppListItemPermission.md) to retrieve the id.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
Optional url of a site to revoke the permissions on. Defaults to the currently connected site.

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

