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
Restore-PnPDeletedContainer [-Identity] <String> [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1

```powershell
Restore-PnPDeletedContainer -Identity "b!jKRbiovfMEWUWKabObEnjC5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Restores the Container with ContainerId "b!jKRbiovfMEWUWKabObEnjC5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" from the Recycle Bin.

### EXAMPLE 2

```powershell
Restore-PnPDeletedContainer -Identity "https://contoso-admin.sharepoint.com/_api/v2.1/storageContainers/b!jKRbiovfMEWUWKabObEnjC5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -Force
```

Restores the Container with the specified api url from the Recycle Bin without asking for confirmation.

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

### -Force

Restores the Container without asking for confirmation.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The id or the api url of the deleted Container to be restored. A deleted Container cannot be restored by its site url.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
