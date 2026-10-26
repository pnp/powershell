---
Module Name: PnP.PowerShell
title: Get-PnPSyntexModelPublication
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSyntexModelPublication.html
---
 
# Get-PnPSyntexModelPublication

> [!WARNING]
> This cmdlet is obsolete and no longer does anything, as SharePoint Syntex is no longer supported. It only writes a warning and will be removed in a future version.

## SYNOPSIS
Returns the libraries to which a Microsoft Syntex model was published.

This cmdlet only works when you've connected to a Syntex Content Center site.

## SYNTAX

```powershell
Get-PnPSyntexModelPublications -Model <SyntexModelPipeBind> [-Connection <PnPConnection>]
 
```

## DESCRIPTION
This command returns the libraries to which a Syntex document processing model defined in the connected Syntex Content Center site was published.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSyntexModelPublication -Identity "Invoice model"
```

Gets the libraries to which the document processing model named "Invoice model" was published.

## PARAMETERS

### -Connection
Ignored, as this cmdlet no longer performs any request. Kept for backwards compatibility with existing scripts.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Model
The name or id of the Syntex model.

```yaml
Type: SyntexModelPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

