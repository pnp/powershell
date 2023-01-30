---
Module Name: PnP.PowerShell
title: Get-PnPSyntexModel
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSyntexModel.html
---
 
# Get-PnPSyntexModel

## SYNOPSIS
Returns Microsoft Syntex models from a Syntex Content Center.

This cmdlet only works when you've connected to a Syntex Content Center site.


## SYNTAX

```powershell
Get-PnPSyntexModel [-Identity] <SyntexModelPipeBind> [-Connection <PnPConnection>]
 [<CommonParameters>]
```

## DESCRIPTION
This command allows the retrieval of a Syntex document processing models defined in the connected Syntex Content Center site.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSyntexModel 
```

Lists all the document processing models in the connected Syntex Content Center site.

### EXAMPLE 2
```powershell
Get-PnPSyntexModel -Identity 1
```

Gets the document processing model with the id 1.

### EXAMPLE 3
```powershell
Get-PnPSyntexModel -Identity "Invoice model"
```

Gets the document processing model named "Invoice model".

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

