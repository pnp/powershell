---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
title: Grant-PnPEntraIDAppFilePermission
online version: https://pnp.github.io/powershell/cmdlets/Grant-PnPEntraIDAppFilePermission.html
Module Name: PnP.PowerShell
---
   
# Grant-PnPEntraIDAppFilePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Files.ReadWrite.All or Sites.ReadWrite.All

Adds permissions for a given Entra ID application registration on a file in a document library.

## SYNTAX

```powershell
Grant-PnPEntraIDAppFilePermission -AppId <Guid> -DisplayName <String> -Permissions <Read|Write|Owner|FullControl> -List <String> [-Path <String>] [-FileId <String>] [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## DESCRIPTION

This cmdlet adds permissions for a given Entra ID application registration on a file in a document library. It is used in conjunction with the Entra ID SharePoint application permission `Files.SelectedOperations.Selected`.

The file can be identified by either:
- `-Path`: the path to the file relative to the document library root (e.g. `Folder/SubFolder/file.docx`)
- `-FileId`: the Graph drive item ID of the file

Exactly one of `-Path` or `-FileId` must be specified.

## EXAMPLES

### EXAMPLE 1
```powershell
Grant-PnPEntraIDAppFilePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Read -List "Documents" -Path "Contracts/Agreement.docx"
```

Grants the Entra ID application registration Read access on the file at the specified path in the Documents library of the currently connected site.

### EXAMPLE 2
```powershell
Grant-PnPEntraIDAppFilePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Write -List "Documents" -FileId "01ABC123DEF456GHI789"
```

Grants Write access on the file with the specified drive item ID in the Documents library.

### EXAMPLE 3
```powershell
Grant-PnPEntraIDAppFilePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Owner -List "Documents" -Path "Report.xlsx" -Site https://contoso.sharepoint.com/sites/finance
```

Grants Owner access on the specified file in the Documents library of the given site collection.

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

