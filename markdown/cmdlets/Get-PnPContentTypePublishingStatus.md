---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPContentTypePublishingStatus.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPContentTypePublishingStatus
---
  
# Get-PnPContentTypePublishingStatus

## SYNOPSIS

**Required Permissions**

  * Fullcontrol permission on the content type hub site.

Returns the publishing status of a content type present on content type hub site.

## SYNTAX

```powershell
Get-PnPContentTypePublishingStatus -ContentType <ContentTypePipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve the publishing status of a content type present on content type hub site.

## EXAMPLES

### EXAMPLE 1
```powershell
 Get-PnPContentTypePublishingStatus -ContentType 0x0101
```

This will return `True` if content type is published in the content type hub site otherwise it will return `False`.
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

### -ContentType
The content type object in the content type hub site for which the publishing status needs to be fetched.

```yaml
Type: ContentType
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)