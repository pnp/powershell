---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnppublishingimagerendition
schema: 2.0.0
title: Get-PnPPublishingImageRendition
---

# Get-PnPPublishingImageRendition

## SYNOPSIS
Returns all image renditions or if Identity is specified a specific one

## SYNTAX

```powershell
Get-PnPPublishingImageRendition [[-Identity] <ImageRenditionPipeBind>] [-Web <WebPipeBind>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

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

### -Web
This parameter allows you to optionally apply the cmdlet action to a subweb within the current web. In most situations this parameter is not required and you can connect to the subweb using Connect-PnPOnline instead. Specify the GUID, server relative url (i.e. /sites/team1) or web instance of the web to apply the command to. Omit this parameter to use the current web.

```yaml
Type: WebPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)