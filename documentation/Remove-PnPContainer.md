---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPContainer
---
  
# Remove-PnPContainer

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

The Remove-PnPContainer cmdlet removes a container from the SharePoint tenant. The container to remove is specified by the Identity parameter, which accepts a ContainerPipeBind object.

## SYNTAX

```powershell
Remove-PnPContainer [-Identity] <ContainerPipeBind> [-SkipRecycleBin] [-Force] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

When admins delete a Container, it is moved into the Recycle Bin. A deleted Container can be restored from the Recycle Bin within 93 days. If a Container is deleted from the Recycle Bin, or it exceeds the 93-day retention period, it is permanently deleted. Deleting a Container deletes everything within it, including all documents and files. You can view all deleted Containers in the Recycle Bin with the Get-PnPDeletedContainer cmdlet.

Use `-SkipRecycleBin` to permanently delete the Container straight away. It then cannot be restored.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" 
```

Moves the specified container to the Recycle Bin by using the container id.

### EXAMPLE 2

```powershell
Remove-PnPContainer -Identity  "https://contoso.sharepoint.com/contentstorage/CSP_4bd71a68-8312-4275-99b1-a2b70e3ba0e8"
```

Moves the specified container to the Recycle Bin by using the container url.

### EXAMPLE 3

```powershell
Remove-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1" -SkipRecycleBin -Force
```

Permanently deletes the specified container without asking for confirmation.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
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

### -Force

Skips the confirmation asked for when using `-SkipRecycleBin`.

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

Specify container site url, container api url or container id.

```yaml
Type: ContainerPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -SkipRecycleBin

Permanently deletes the Container instead of moving it to the Recycle Bin. Asks for confirmation unless `-Force` is used.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf

Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
