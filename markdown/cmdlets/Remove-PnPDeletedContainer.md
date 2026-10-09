---
Module Name: PnP.PowerShell
tags: Available in the current Nightly Release only.
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPDeletedContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPDeletedContainer
---
 
# Remove-PnPDeletedContainer

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Permanently deletes a Container from the Recycle Bin in SharePoint Embedded.

## SYNTAX

```powershell
Remove-PnPDeletedContainer [-Identity] <String> [-Force] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Permanently deletes a Container that has been moved to the Recycle Bin, together with everything in it. It then cannot be restored. Asks for confirmation unless `-Force` is used. Use Get-PnPDeletedContainer to see the Containers in the Recycle Bin.

Only Containers that are in the Recycle Bin are deleted. Any other id, such as that of an active Container, is reported as an error and left alone, so the Containers returned by other cmdlets cannot be deleted by accident when piped in.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPDeletedContainer -Identity "b!jKRbiovfMEWUWKabObEnjC5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Permanently deletes the specified Container from the Recycle Bin, after asking for confirmation.

### EXAMPLE 2
```powershell
Get-PnPDeletedContainer | Where-Object ContainerName -like "Test*" | Remove-PnPDeletedContainer -Force
```

Permanently deletes every Container in the Recycle Bin whose name starts with Test, without asking for confirmation.

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

Permanently deletes the Container without asking for confirmation.

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

The id or the api url of the deleted Container, as returned by Get-PnPDeletedContainer. A deleted Container cannot be permanently deleted by its site url.

```yaml
Type: String
Parameter Sets: (All)
Aliases: ContainerId

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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

