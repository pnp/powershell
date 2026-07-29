---
tags: Available in the current Nightly Release only.
title: Get-PnPEntraIDAppListPermission
Module Name: PnP.PowerShell
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEntraIDAppListPermission.html
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
---
   
# Get-PnPEntraIDAppListPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.ReadWrite.All

Returns Entra ID App permissions for a list.

## SYNTAX

### All Permissions
```powershell
Get-PnPEntraIDAppListPermission -List <String> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

### By Permission Id
```powershell
Get-PnPEntraIDAppListPermission -PermissionId <String> -List <String> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

### By App Display Name or App Id
```powershell
Get-PnPEntraIDAppListPermission -AppIdentity <String> -List <String> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet returns app permissions for a list in either the current or a given site.

The list can be identified by its GUID or display name.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEntraIDAppListPermission -List "Documents"
```

Returns all app permissions set on the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Get-PnPEntraIDAppListPermission -List "Documents" -Site https://contoso.sharepoint.com/sites/projects
```

Returns all app permissions set on the Documents library of the specified site collection.

### EXAMPLE 3
```powershell
Get-PnPEntraIDAppListPermission -List "12345678-1234-1234-1234-123456789012"
```

Returns all app permissions set on the list identified by its GUID.

### EXAMPLE 4
```powershell
Get-PnPEntraIDAppListPermission -List "Documents" -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00
```

Returns the specific permission details for the given permission id on the Documents library.

### EXAMPLE 5
```powershell
Get-PnPEntraIDAppListPermission -List "Documents" -AppIdentity "My App"
```

Returns the specific permission details for the app with the provided display name on the Documents library.

### EXAMPLE 6
```powershell
Get-PnPEntraIDAppListPermission -List "Documents" -AppIdentity "89ea5c94-7736-4e25-95ad-3fa95f62b66e"
```

Returns the specific permission details for the app with the provided app id on the Documents library.

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
The list to retrieve permissions for. Accepts a list GUID or display name.

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

