---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFolderSharingLink.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFolderSharingLink
---
  
# Get-PnPFolderSharingLink

## SYNOPSIS
Retrieves sharing links to associated with the folder.

## SYNTAX

```powershell
Get-PnPFolderSharingLink -Folder <FolderPipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Retrieves sharing links for a folder.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFolderSharingLink -Folder "/sites/demo/Shared Documents/Test"
```

This will fetch sharing links for `Test` folder in the `Shared Documents` library.

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

### -Folder
The folder in the site

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
