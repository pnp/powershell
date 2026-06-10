---
title: Revoke-PnPEntraIDAppListPermission
external help file: PnP.PowerShell.dll-Help.xml
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Revoke-PnPEntraIDAppListPermission.html
applicable: SharePoint Online
schema: 2.0.0
---
   
# Revoke-PnPEntraIDAppListPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Revokes permissions for a given Entra ID application registration on a list.

## SYNTAX

```powershell
Revoke-PnPEntraIDAppListPermission -PermissionId <String> -List <String> [-Site <SitePipeBind>] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet revokes an existing permission for an Entra ID application registration on a list.

Use [Get-PnPEntraIDAppListPermission](Get-PnPEntraIDAppListPermission.md) to retrieve the `PermissionId` required by this cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Revoke-PnPEntraIDAppListPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents"
```

Revokes the permission with the specified id on the Documents library of the currently connected site. A confirmation prompt will be shown before the permission is removed.

### EXAMPLE 2
```powershell
Revoke-PnPEntraIDAppListPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents" -Force
```

Revokes the permission on the Documents library without prompting for confirmation.

### EXAMPLE 3
```powershell
Revoke-PnPEntraIDAppListPermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents" -Site https://contoso.sharepoint.com/sites/projects -Force
```

Revokes the permission on the Documents library of the specified site collection without prompting for confirmation.

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
The list to revoke permissions on. Accepts a list GUID or display name.

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
The id of the permission to revoke. Use [Get-PnPEntraIDAppListPermission](Get-PnPEntraIDAppListPermission.md) to retrieve the id.

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

