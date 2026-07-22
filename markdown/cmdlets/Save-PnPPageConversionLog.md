---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Save-PnPPageConversionLog.html
external help file: PnP.PowerShell.dll-Help.xml
title: Save-PnPPageConversionLog
---
 
# Save-PnPPageConversionLog

## SYNOPSIS
Persists the current client side page conversion log data to the loggers linked to the last used page transformation run. Needs to be used in conjunction with the -LogSkipFlush flag on the ConvertTo-PnPPage cmdlet.

## SYNTAX 

```powershell
Save-PnPPageConversionLog  [-Connection <PnPConnection>]
```

## EXAMPLES

### EXAMPLE 1
```powershell
Save-PnPPageConversionLog
```

Persists the current client side page conversion log data to the loggers linked to the last used page transformation run. Needs to be used in conjunction with the -LogSkipFlush flag on the ConvertTo-PnPPage cmdlet.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Accept pipeline input: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

