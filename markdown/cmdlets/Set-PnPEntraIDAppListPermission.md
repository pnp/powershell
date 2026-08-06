---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPEntraIDAppListPermission.html
title: Set-PnPEntraIDAppListPermission
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
   
# Set-PnPEntraIDAppListPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Updates permissions for a given Entra ID application registration on a list.

## SYNTAX

```powershell
Set-PnPEntraIDAppListPermission -PermissionId <String> -Permissions <Read|Write|Owner|FullControl> -List <String> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet updates an existing permission for an Entra ID application registration on a list.

Use [Get-PnPEntraIDAppListPermission](Get-PnPEntraIDAppListPermission.md) to retrieve the `PermissionId` required by this cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPEntraIDAppListPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -Permissions Read -List "Documents"
```

Updates the permission to Read access on the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Set-PnPEntraIDAppListPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -Permissions Owner -List "Documents" -Site https://contoso.sharepoint.com/sites/projects
```

Updates the permission to Owner access on the Documents library of the specified site collection.

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
The list to update permissions on. Accepts a list GUID or display name.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PermissionId
The id of the permission to update. Use [Get-PnPEntraIDAppListPermission](Get-PnPEntraIDAppListPermission.md) to retrieve the id.

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

