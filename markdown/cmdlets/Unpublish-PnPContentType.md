---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Unpublish-PnPContentType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Unpublish-PnPContentType
---
  
# Unpublish-PnPContentType

## SYNOPSIS

**Required Permissions**

  * Fullcontrol permission on the content type hub site.

Unpublishes a content type present on content type hub site.

## SYNTAX

```powershell
Unpublish-PnPContentType -ContentType <ContentTypePipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to unpublish a content type present on content type hub site.

## EXAMPLES

### EXAMPLE 1
```powershell
 Unpublish-PnPContentType -ContentType 0x0101
```

This will unpublish the content type with the given id.
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
The content type object in the content type hub site which is to be unpublished.

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