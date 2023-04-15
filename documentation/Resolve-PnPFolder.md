---
Module Name: PnP.PowerShell
title: Resolve-PnPFolder
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Resolve-PnPFolder.html
---
 
# Resolve-PnPFolder

## SYNOPSIS
Returns a folder from a given site relative path, and will create it if it does not exist.

## SYNTAX

```powershell
Resolve-PnPFolder [-SiteRelativePath] <String> [-Connection <PnPConnection>]
 [-Includes <String[]>] 
```

## DESCRIPTION
Returns a folder from a given site relative path, and will create it if it does not exist. If you do not want the folder to be created, for instance just to test if a folder exists, use Get-PnPFolder

## EXAMPLES

### EXAMPLE 1
```powershell
Resolve-PnPFolder -SiteRelativePath "demofolder/subfolder"
```

Creates a folder called subfolder in a folder called demofolder located in the root folder of the site. If the folder hierarchy does not exist, it will be created.

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

### -SiteRelativePath
Site Relative Folder Path

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
[Get-PnPFolder](https://github.com/OfficeDev/PnP-PowerShell/blob/master/Documentation/GetPnPFolder.md)