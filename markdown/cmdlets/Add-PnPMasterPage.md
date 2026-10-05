---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPMasterPage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPMasterPage
---
  
# Add-PnPMasterPage

## SYNOPSIS
Adds a Masterpage

## SYNTAX

```powershell
Add-PnPMasterPage -SourceFilePath <String> -Title <String> -Description <String>
 [-DestinationFolderHierarchy <String>] [-UIVersion <String>] [-DefaultCssFile <String>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add MasterPage.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPMasterPage -SourceFilePath "page.master" -Title "MasterPage" -Description "MasterPage for Web" -DestinationFolderHierarchy "SubFolder"
```

Adds a MasterPage from the local file "page.master" to the folder "SubFolder" in the MasterPage gallery.

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

### -DefaultCssFile
Default CSS file for the MasterPage, this Url is SiteRelative

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
Description for the MasterPage

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DestinationFolderHierarchy
Folder hierarchy where the MasterPage will be deployed

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SourceFilePath
Path to the file which will be uploaded

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
Title for the MasterPage

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UIVersion
UIVersion of the MasterPage. Default = 15

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


