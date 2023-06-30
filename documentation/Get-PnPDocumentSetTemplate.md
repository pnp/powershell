---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPDocumentSetTemplate.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPDocumentSetTemplate
---
  
# Get-PnPDocumentSetTemplate

## SYNOPSIS
Retrieves a document set template

## SYNTAX

```powershell
Get-PnPDocumentSetTemplate [-Identity] <DocumentSetPipeBind> [-Connection <PnPConnection>]
 [-Includes <String[]>] 
```

## DESCRIPTION

Allows to retrieve a document set template.

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



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


