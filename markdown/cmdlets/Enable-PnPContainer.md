---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: Enable-PnPContainer
---

# Enable-PnPContainer

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : FileStorageContainer.Selected, plus permission on the Container Type for the application you connect with

Activates a Container in SharePoint Embedded.

## SYNTAX

```powershell
Enable-PnPContainer [-Identity] <ContainerPipeBind> [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

A Container is created inactive and is deleted automatically if it is not activated within 24 hours after its creation. This cmdlet activates it through Microsoft Graph.

## EXAMPLES

### EXAMPLE 1
```powershell
Enable-PnPContainer -Identity "b!aBrXSxKDdUKZsaK3Djug6C5rF4MG3pRBomypnjOHiSrjkM_EBk_1S57U3gD7oW-1"
```

Activates the specified Container.

### EXAMPLE 2
```powershell
New-PnPContainer -Name "Contoso Legal" -ContainerTypeId 4f0af585-8dcc-0000-223d-661eb2c604e4 | Enable-PnPContainer
```

Creates a Container and activates it.

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

### -Identity

The id or the api url of the Container. Microsoft Graph cannot look a Container up by its site url.

```yaml
Type: ContainerPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
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
[Activate fileStorageContainer](https://learn.microsoft.com/graph/api/filestoragecontainer-activate)
