---
tags: Available in the current Nightly Release only.
title: Set-PnPEntraIDAppFilePermission
schema: 2.0.0
Module Name: PnP.PowerShell
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPEntraIDAppFilePermission.html
external help file: PnP.PowerShell.dll-Help.xml
applicable: SharePoint Online
---
   
# Set-PnPEntraIDAppFilePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Files.ReadWrite.All or Sites.ReadWrite.All

Updates permissions for a given Entra ID application registration on a file in a document library.

## SYNTAX

```powershell
Set-PnPEntraIDAppFilePermission -PermissionId <String> -Permissions <Read|Write|Owner|FullControl> -List <String> [-Path <String>] [-FileId <String>] [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet updates an existing permission for an Entra ID application registration on a file in a document library. It is used in conjunction with the Entra ID SharePoint application permission `Files.SelectedOperations.Selected`.

Use [Get-PnPEntraIDAppFilePermission](Get-PnPEntraIDAppFilePermission.md) to retrieve the `PermissionId` required by this cmdlet.

The file can be identified by either:
- `-Path`: the path to the file relative to the document library root (e.g. `Folder/SubFolder/file.docx`)
- `-FileId`: the Graph drive item ID of the file

Exactly one of `-Path` or `-FileId` must be specified.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPEntraIDAppFilePermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -Permissions Read -List "Documents" -Path "Contracts/Agreement.docx"
```

Updates the permission to Read access on the file at the specified path in the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Set-PnPEntraIDAppFilePermission -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00 -Permissions Write -List "Documents" -FileId "01ABC123DEF456GHI789" -Site https://contoso.sharepoint.com/sites/finance
```

Updates the permission to Write access on the file with the specified drive item ID in the given site collection.

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
The id of the permission to update. Use [Get-PnPEntraIDAppFilePermission](Get-PnPEntraIDAppFilePermission.md) to retrieve the id.

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

