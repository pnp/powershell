---
Module Name: PnP.PowerShell
title: Set-PnPInPlaceRecordsManagement
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPInPlaceRecordsManagement.html
---
 
# Set-PnPInPlaceRecordsManagement

## SYNOPSIS
Activates or deactivates in place records management feature.

## SYNTAX

```powershell
Set-PnPInPlaceRecordsManagement -Enabled <Boolean> [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Activates or deactivates in place records management feature in the site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPInPlaceRecordsManagement -Enabled $true
```

Activates In Place Records Management.

### EXAMPLE 2
```powershell
Set-PnPInPlaceRecordsManagement -Enabled $false
```

Deactivates In Place Records Management.

## PARAMETERS

### -Enabled
Specifies whether in place records management should be enabled.
```yaml
Type: Boolean
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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
