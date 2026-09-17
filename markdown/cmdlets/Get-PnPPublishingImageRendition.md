---
Module Name: PnP.PowerShell
title: Get-PnPPublishingImageRendition
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPPublishingImageRendition.html
---
 
# Get-PnPPublishingImageRendition

## SYNOPSIS
Returns all image renditions or if Identity is specified a specific one

## SYNTAX

```powershell
Get-PnPPublishingImageRendition [[-Identity] <ImageRenditionPipeBind>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve all image renditions or a specific one when `Identity` option is used.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPPublishingImageRendition
```

Returns all Image Renditions

### EXAMPLE 2
```powershell
Get-PnPPublishingImageRendition -Identity "Test"
```

Returns the image rendition named "Test"

### EXAMPLE 3
```powershell
Get-PnPPublishingImageRendition -Identity 2
```

Returns the image rendition where its id equals 2

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
Id or name of an existing image rendition

```yaml
Type: ImageRenditionPipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

