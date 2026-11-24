---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFileSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFileSharingLink
---
  
# Get-PnPFileSharingLink

## SYNOPSIS
Retrieves sharing links to associated with the file or list item.

## SYNTAX

```powershell
Get-PnPFileSharingLink -Identity <FilePipeBind> [-Verbose] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Retrieves sharing links for a file or list item.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFileSharingLink -Identity "/sites/demo/Shared Documents/Test.docx"
```

This will fetch sharing links for `Test.docx` file in the `Shared Documents` library based on the server relative url.

### EXAMPLE 2
```powershell
Get-PnPFileSharingLink -Identity eff4c8ca-7b92-4aa2-9744-855611c6ccf2
```

This will fetch sharing links for the file in the site with the provided unique identifier, regardless of where it is located.

### EXAMPLE 3
```powershell
Get-PnPListItem -List "Documents" | Get-PnPFileSharingLink
```

This will fetch sharing links for all files in the `Documents` library.

### EXAMPLE 4

```powershell
Get-PnPListItem -List "Documents" -Id 1 | Get-PnPFileSharingLink
```

This will fetch sharing links for the file in the `Documents` library with Id 1.

### EXAMPLE 5
```powershell
Get-PnPFile -Url "/sites/demo/Shared Documents/Test.docx" | Get-PnPFileSharingLink
```

This will fetch sharing links for the passed in file.

### EXAMPLE 6
```powershell
Get-PnPFileInFolder -Recurse -ExcludeSystemFolders -FolderSiteRelativeUrl "Shared Documents" | Get-PnPFileSharingLink
```

This will fetch sharing links for all files in the `Shared Documents` library, including the files in subfolders, excluding the ones in hidden internal system folders.

### EXAMPLE 7
```powershell
Get-PnPFileInFolder -Recurse -ExcludeSystemFolders -FolderSiteRelativeUrl "Shared Documents"  | Get-PnPFileSharingLink | ? ExpirationDateTime -eq $null
```

This will fetch sharing links for all files in the `Shared Documents` library, including the files in subfolders, excluding the ones in hidden internal system folders where no expiration has been set on the sharing of the file.

### EXAMPLE 8
```powershell	
Get-PnPFileSharingLink -Identity /sites/demo/Lists/Issue tracker/1_.000
```

This will fetch sharing links for the list item with id `1` from list `Issue Tracker`

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

### -Identity
The server relative path to the file, the unique identifier of the file, the listitem representing the file, or the file object itself to retrieve the sharing links for.

```yaml
Type: FilePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
