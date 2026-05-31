---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPConnection.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPConnection
---
  
# Get-PnPConnection

## SYNOPSIS
Returns the current connection

## SYNTAX

```powershell
Get-PnPConnection [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns a PnP PowerShell Connection for use with the -Connection parameter on other cmdlets.

## EXAMPLES

### EXAMPLE 1
```powershell
$ctx = Get-PnPConnection
```

This will put the current connection for use with the -Connection parameter on other cmdlets.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying -ReturnConnection on Connect-PnPOnline. If not provided, the connection will be retrieved from the current context.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)