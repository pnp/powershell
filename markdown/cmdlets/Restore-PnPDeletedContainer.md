---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Restore-PnPDeletedContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Restore-PnPDeletedContainer
---
  
# Restore-PnPDeletedContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

The Restore-PnPDeletedContainer recovers a deleted Container from the Recycle Bin.

## SYNTAX

```powershell
Restore-PnPDeletedContainer -Identity <string> [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Restore-PnPDeletedContainer -Identity "b!jKRbiovfMEWUWKabObEnjC5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Restores the Container with ContainerId "b!jKRbiovfMEWUWKabObEnjC5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" from the Recycle Bin.

## PARAMETERS

### -Identity

The ContainerId of the deleted container to be restored.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Restore-PnPConnection.

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
