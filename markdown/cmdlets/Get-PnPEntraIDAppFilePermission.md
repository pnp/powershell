---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPEntraIDAppFilePermission.html
title: Get-PnPEntraIDAppFilePermission
Module Name: PnP.PowerShell
applicable: SharePoint Online
---
   
# Get-PnPEntraIDAppFilePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Files.ReadWrite.All or Sites.ReadWrite.All

Returns Entra ID App permissions for a file in a document library.

## SYNTAX

### All Permissions
```powershell
Get-PnPEntraIDAppFilePermission -List <String> [-Path <String>] [-FileId <String>] [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

### By Permission Id
```powershell
Get-PnPEntraIDAppFilePermission -PermissionId <String> -List <String> [-Path <String>] [-FileId <String>] [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

### By App Display Name or App Id
```powershell
Get-PnPEntraIDAppFilePermission -AppIdentity <String> -List <String> [-Path <String>] [-FileId <String>] [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet returns app permissions for a file in a document library. It is used in conjunction with the Entra ID SharePoint application permission `Files.SelectedOperations.Selected`.

The file can be identified by either:
- `-Path`: the path to the file relative to the document library root (e.g. `Folder/SubFolder/file.docx`)
- `-FileId`: the Graph drive item ID of the file

Exactly one of `-Path` or `-FileId` must be specified.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPEntraIDAppFilePermission -List "Documents" -Path "Contracts/2024/Agreement.docx"
```

Returns all app permissions set on the file at the given path in the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Get-PnPEntraIDAppFilePermission -List "Documents" -Path "Report.xlsx" -Site https://contoso.sharepoint.com/sites/finance
```

Returns all app permissions set on the file at the root of the Documents library on the specified site.

### EXAMPLE 3
```powershell
Get-PnPEntraIDAppFilePermission -List "Documents" -FileId "01ABC123DEF456GHI789"
```

Returns all app permissions set on the file with the specified drive item ID.

### EXAMPLE 4
```powershell
Get-PnPEntraIDAppFilePermission -List "Documents" -Path "Report.xlsx" -PermissionId aTowaS50fG1zLnNwLmV4dHxlMzhjZmIzMS00
```

Returns the specific permission details for the given permission id on the file.

### EXAMPLE 5
```powershell
Get-PnPEntraIDAppFilePermission -List "Documents" -Path "Report.xlsx" -AppIdentity "My App"
```

Returns the specific permission details for the app with the provided display name on the file.

### EXAMPLE 6
```powershell
Get-PnPEntraIDAppFilePermission -List "Documents" -Path "Report.xlsx" -AppIdentity "89ea5c94-7736-4e25-95ad-3fa95f62b66e"
```

Returns the specific permission details for the app with the provided app id on the file.

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

