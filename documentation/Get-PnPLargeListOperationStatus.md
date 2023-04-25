---
Module Name: PnP.PowerShell
title: Get-PnPLargeListOperationStatus
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPLargeListOperationStatus.html
---
 
# Get-PnPLargeListOperationStatus

## SYNOPSIS
Get the status of a large list operation. Currently supports Large List Removal Operation.

## SYNTAX

```powershell
Get-PnPLargeListOperationStatus [-ListId] <ListId> [-OperationId] <OperationId> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to get the status of a large list operation.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPLargeListOperationStatus -ListId 9ea5d197-2227-4156-9ae1-725d74dc029d -OperationId 924e6a34-5c90-4d0d-8083-2efc6d1cf481
```

## PARAMETERS

### -ListId
ListId/Guid of the list. Retrieve the value for this parameter from the output of the Large List Operation Command.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -OperationId
OperationId/Guid of the Large List Operation. Retrieve the value for this parameter from the output of the Large List Operation Command.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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