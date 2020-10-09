---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpdocumentsettemplate
schema: 2.0.0
title: Get-PnPDocumentSetTemplate
---

# Get-PnPDocumentSetTemplate

## SYNOPSIS
Retrieves a document set template

## SYNTAX

```powershell
Get-PnPDocumentSetTemplate [-Identity] <DocumentSetPipeBind> [-Web <WebPipeBind>] [-Connection <PnPConnection>]
 [-Includes <String[]>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPDocumentSetTemplate -Identity "Test Document Set"
```

This will get the document set template with the name "Test Document Set"

### EXAMPLE 2
```powershell
Get-PnPDocumentSetTemplate -Identity "0x0120D520005DB65D094035A241BAC9AF083F825F3B"
```

This will get the document set template with the id "0x0120D520005DB65D094035A241BAC9AF083F825F3B"

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
Either specify a name, an id, a document set template object or a content type object

```yaml
Type: DocumentSetPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Web
The web to apply the command to. Omit this parameter to use the current web.

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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)