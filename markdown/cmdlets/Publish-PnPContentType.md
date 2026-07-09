---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Publish-PnPContentType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Publish-PnPContentType
---
  
# Publish-PnPContentType

## SYNOPSIS

**Required Permissions**

  * Fullcontrol permission on the content type hub site.

Publishes or republishes a content type present on content type hub site.

## SYNTAX

```powershell
Publish-PnPContentType -ContentType <ContentTypePipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to publish a content type present on content type hub site. To check if a content type has been published already, you can use [Get-PnPContentTypePublishingStatus](Get-PnPContentTypePublishingStatus.md).

## EXAMPLES

### EXAMPLE 1
```powershell
 Publish-PnPContentType -ContentType 0x0101
```

This will publish the content type with the given id.
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
The content type object in the content type hub site which is to be published.

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
