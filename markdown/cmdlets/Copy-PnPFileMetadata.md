---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Copy-PnPFileMetadata.html
external help file: PnP.PowerShell.dll-Help.xml
title: Copy-PnPFileMetadata
---
  
# Copy-PnPFileMetadata

## SYNOPSIS

Synchronizes metadata between files and folders in SharePoint

## SYNTAX

```powershell
Copy-PnPFileMetadata [-SourceUrl] <String> [-TargetUrl] <String> [-Fields <String[]>] [-Recursive] [-Force] [-Connection <PnPConnection>] [-SourceConnection <PnPConnection>] [-TargetConnection <PnPConnection>]
```

## DESCRIPTION

Synchronizes metadata (Created, Modified, Author, Editor) from source files and folders to their corresponding targets without copying the actual content. This cmdlet is useful for restoring lost metadata after migrations where system fields may have been reset.

When updating items, the cmdlet uses `UpdateOverwriteVersion()` to allow setting system fields while avoiding new user-facing versions.

For folders, the cmdlet batches updates per folder to reduce round-trips and improve performance on large libraries. With `-Verbose`, it logs progress for each folder and file processed, including periodic batch flush messages.

Both `-SourceUrl` and `-TargetUrl` can be provided as absolute URLs, server-relative (starting with `/`), or web-relative paths. URLs are normalized against their respective connections: the source URL is normalized using `-SourceConnection` (or the current connection), and the target URL is normalized using `-TargetConnection` (or the current connection). Targets must already exist; if a corresponding target file or folder is not found, it is skipped.

## EXAMPLES

### EXAMPLE 1 (same site, folder, recursive)

```powershell
Copy-PnPFileMetadata -SourceUrl "Shared Documents/MyProject" -TargetUrl "Shared Documents/MyProject"
```

Synchronizes metadata for the MyProject folder and all its contents recursively from the source to the target location, preserving original creation dates, modification dates, and author information.

### EXAMPLE 2 (same site, single file)

```powershell
Copy-PnPFileMetadata -SourceUrl "Shared Documents/report.docx" -TargetUrl "Shared Documents/report.docx"
```

Synchronizes metadata for a single file from the source to the target, restoring the original system fields.

### EXAMPLE 3 (same site, limited fields)

```powershell
Copy-PnPFileMetadata -SourceUrl "Shared Documents/Projects" -TargetUrl "Shared Documents/Projects" -Fields @("Created", "Modified") -Force
```

Synchronizes only the Created and Modified dates for the Projects folder and its contents, without prompting for confirmation.

### EXAMPLE 4 (same site, non-recursive)

```powershell
Copy-PnPFileMetadata -SourceUrl "Shared Documents/Archives" -TargetUrl "Shared Documents/Archives" -Recursive:$false
```

Synchronizes metadata only for the Archives folder itself, without processing its subfolders and files.

### EXAMPLE 5 (cross site, two connections)

```powershell
$src = Connect-PnPOnline -Url https://contoso.sharepoint.com/sites/archives -ReturnConnection
$dst = Connect-PnPOnline -Url https://contoso.sharepoint.com/sites/projects -ReturnConnection
Copy-PnPFileMetadata -SourceUrl "Shared Documents/MyProject" -TargetUrl "Shared Documents/MyProject" -SourceConnection $src -TargetConnection $dst -Verbose
```

Synchronizes metadata across two different site connections.

## PARAMETERS

### -Force

If provided, no confirmation will be requested and the action will be performed

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Fields

Specifies which metadata fields to synchronize. Default fields are Created, Modified, Author, and Editor.

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: @("Author", "Editor", "Created", "Modified")
Accept pipeline input: False
Accept wildcard characters: False
```

### -Recursive

If provided, processes folders recursively including all subfolders and files. This is enabled by default.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: $true
Accept pipeline input: False
Accept wildcard characters: False
```



### -SourceUrl

Site or server relative URL specifying the file or folder to copy metadata from. Must include the file name if it is a file or the entire path to the folder if it is a folder.

```yaml
Type: String
Parameter Sets: (All)
Aliases: ServerRelativeUrl

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetUrl

Site or server relative URL specifying the file or folder to copy metadata to. Must include the file name if it is a file or the entire path to the folder if it is a folder.

```yaml
Type: String
Parameter Sets: (All)
Aliases: TargetServerRelativeUrl

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceConnection

Optional connection to be used for accessing the source file or folder. If not provided, the current connection is used.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetConnection

Optional connection to be used for accessing the target file or folder. If not provided, the current connection is used.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
