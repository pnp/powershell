---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPPublishingImageRendition.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPPublishingImageRendition
---
  
# Add-PnPPublishingImageRendition

## SYNOPSIS
Adds an Image Rendition if the Name of the Image Rendition does not already exist. This prevents creating two Image Renditions that share the same name.

## SYNTAX

```powershell
Add-PnPPublishingImageRendition -Name <String> -Width <Int32> -Height <Int32> 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add an Image Rendition.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPPublishingImageRendition -Name "MyImageRendition" -Width 800 -Height 600
```

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

### -Height
The height of the Image Rendition.

```yaml
Type: Int32
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The display name of the Image Rendition.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



### -Width
The width of the Image Rendition.

```yaml
Type: Int32
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


