---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPFolder.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPFolder
---
  
# Add-PnPFolder

## SYNOPSIS
Creates a folder within a parent folder

## SYNTAX

```powershell
Add-PnPFolder -Name <String> -Folder <FolderPipeBind> [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to add a new folder.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPFolder -Name NewFolder -Folder _catalogs/masterpage
```

This will create the folder NewFolder in the masterpage catalog

### EXAMPLE 2
```powershell
Add-PnPFolder -Name NewFolder -Folder "Shared Documents"
```

This will create the folder NewFolder in the Documents library

### EXAMPLE 3
```powershell
Add-PnPFolder -Name NewFolder -Folder "Shared Documents/Folder"
```

This will create the folder NewFolder in Folder inside the Documents library

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
The parent folder in the site

```yaml
Type: FolderPipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The folder name

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


