---
external help file: PnP.PowerShell.dll-Help.xml
title: Revoke-PnPEntraIDAppFilePermission
online version: https://pnp.github.io/powershell/cmdlets/Revoke-PnPEntraIDAppFilePermission.html
applicable: SharePoint Online
schema: 2.0.0
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
---
   
# Revoke-PnPEntraIDAppFilePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Files.ReadWrite.All or Sites.ReadWrite.All

Revokes permissions for a given Entra ID application registration on a file in a document library.

## SYNTAX

```powershell
Revoke-PnPEntraIDAppFilePermission -PermissionId <String> -List <String> [-Path <String>] [-FileId <String>] [-Site <SitePipeBind>] [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet revokes an existing permission for an Entra ID application registration on a file in a document library. It is used in conjunction with the Entra ID SharePoint application permission `Files.SelectedOperations.Selected`.

Use [Get-PnPEntraIDAppFilePermission](Get-PnPEntraIDAppFilePermission.md) to retrieve the `PermissionId` required by this cmdlet.

The file can be identified by either:
- `-Path`: the path to the file relative to the document library root (e.g. `Folder/SubFolder/file.docx`)
- `-FileId`: the Graph drive item ID of the file

Exactly one of `-Path` or `-FileId` must be specified.

## EXAMPLES

### EXAMPLE 1
```powershell
Revoke-PnPEntraIDAppFilePermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents" -Path "Contracts/Agreement.docx"
```

Revokes the permission with the specified id on the file at the given path in the Documents library of the currently connected site. A confirmation prompt will be shown before the permission is removed.

### EXAMPLE 2
```powershell
Revoke-PnPEntraIDAppFilePermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents" -FileId "01ABC123DEF456GHI789" -Force
```

Revokes the permission on the file with the specified drive item ID without prompting for confirmation.

### EXAMPLE 3
```powershell
Revoke-PnPEntraIDAppFilePermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -List "Documents" -Path "Report.xlsx" -Site https://contoso.sharepoint.com/sites/finance -Force
```

Revokes the permission on the specified file in the given site collection without prompting for confirmation.

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

### -FileId
The Graph drive item ID of the file. Use this as an alternative to `-Path` when you already know the drive item ID. Mutually exclusive with `-Path`.

```yaml
Type: String
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
The document library containing the file. Accepts a list GUID or display name.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
The path to the file relative to the document library root (e.g. `Folder/SubFolder/file.docx` or just `file.docx` for a file at the root). Mutually exclusive with `-FileId`.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PermissionId
The id of the permission to revoke. Use [Get-PnPEntraIDAppFilePermission](Get-PnPEntraIDAppFilePermission.md) to retrieve the id.

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

