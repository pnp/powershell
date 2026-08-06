---
tags: Available in the current Nightly Release only.
external help file: PnP.PowerShell.dll-Help.xml
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFolderSharingLink.html
title: Get-PnPFolderSharingLink
Module Name: PnP.PowerShell
applicable: SharePoint Online
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

