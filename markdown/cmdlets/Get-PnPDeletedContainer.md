---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPDeletedContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPDeletedContainer
---
  
# Get-PnPDeletedContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

The Get-PnPDeletedContainer cmdlet returns a list of all deleted Containers in the Recycle Bin. There is no Identity parameter needed. The list includes the ContainerId, ContainerName, DeletedOn, and CreatedDate. Deleted Containers in the Recycle Bin are permanently deleted after 93 days. Use cmdlet Restore-PnPDeletedContainer to restore a deleted container.

## SYNTAX

```powershell
Get-PnPDeletedContainer [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPDeletedContainer
```

Returns a list of the ContainerId, ContainerName, and CreatedDate of all deleted Containers in the Recycle Bin.

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
