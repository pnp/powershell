---
Module Name: PnP.PowerShell
title: Get-LargeListOperationStatus
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-LargeListOperationStatus.html
---
 
# Get-LargeListOperationStatus

## SYNOPSIS
Get the status of a large list operation. Currently supports Large List Removal Operation.

## SYNTAX

```powershell
Get-LargeListOperationStatus [-ListId] <ListId> [-OperationId] <OperationId> [-Connection <PnPConnection>]
```

## DESCRIPTION

Allows to get the status of a large list operation.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-LargeListOperationStatus -ListId 9ea5d197-2227-4156-9ae1-725d74dc029d -OperationId 924e6a34-5c90-4d0d-8083-2efc6d1cf481
```