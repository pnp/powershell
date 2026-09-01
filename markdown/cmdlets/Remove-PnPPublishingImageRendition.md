---
Module Name: PnP.PowerShell
title: Remove-PnPPublishingImageRendition
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPPublishingImageRendition.html
---
 
# Remove-PnPPublishingImageRendition

## SYNOPSIS
Removes an existing image rendition

## SYNTAX

```powershell
Remove-PnPPublishingImageRendition [-Identity] <ImageRenditionPipeBind> [-Force] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to remove an existing image rendition.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPPublishingImageRendition -Name "MyImageRendition" -Width 800 -Height 600
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

### -Force
If provided, no confirmation will be asked to remove the Image Rendition.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The display name or id of the Image Rendition.

```yaml
Type: ImageRenditionPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

