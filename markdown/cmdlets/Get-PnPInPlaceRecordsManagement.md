---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPInPlaceRecordsManagement.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPInPlaceRecordsManagement
---
  
# Get-PnPInPlaceRecordsManagement

## SYNOPSIS
Returns if the place records management feature is enabled.

## SYNTAX

```powershell
Get-PnPInPlaceRecordsManagement [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPInPlaceRecordsManagement
```

Returns if $true if in place records management is active

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



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


