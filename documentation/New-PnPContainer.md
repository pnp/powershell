---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/New-PnPContainer.html
external help file: PnP.PowerShell.dll-Help.xml
title: New-PnPContainer
---

# New-PnPContainer

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : FileStorageContainer.Selected, plus permission on the Container Type for the application you connect with

Creates a Container in SharePoint Embedded.

## SYNTAX

```powershell
New-PnPContainer [-Name] <String> -ContainerTypeId <Guid> [-Description <String>] [-OcrEnabled <Boolean>] [-ItemVersioningEnabled <Boolean>] [-ItemMajorVersionLimit <Int32>] [-Activate] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Creates a Container of the specified Container Type through Microsoft Graph and returns it. The Container Type must be registered in the tenant, and the application you connect with must have been granted permission on it, for example with Set-PnPContainerApplicationPermission. When connected on behalf of a user, that user becomes the owner of the Container.

A Container is created inactive and is deleted automatically if it is not activated within 24 hours. Use `-Activate` to activate it straight away, or Enable-PnPContainer later. Adding content to the Container or changing it also activates it. When the Container is created but cannot be activated, it is still returned, together with an error, so it can be activated later rather than created a second time.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPContainer -Name "Contoso Legal" -ContainerTypeId 4f0af585-8dcc-0000-223d-661eb2c604e4 -Activate
```

Creates and activates a Container of the specified Container Type.

### EXAMPLE 2
```powershell
New-PnPContainer -Name "Contoso Legal" -Description "Legal documents" -ContainerTypeId 4f0af585-8dcc-0000-223d-661eb2c604e4 -OcrEnabled $true -ItemMajorVersionLimit 100
```

Creates an inactive Container with optical character recognition enabled, keeping at most 100 major versions of each file.

## PARAMETERS

### -Activate

Activates the Container after creating it. Without it, the Container is deleted automatically if it is not activated within 24 hours.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -ContainerTypeId

The id of the Container Type of the Container.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description

The description of the Container.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemMajorVersionLimit

The maximum number of major versions kept of each file in the Container.

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ItemVersioningEnabled

Whether versioning is enabled for the files in the Container.

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name

The display name of the Container.

```yaml
Type: String
Parameter Sets: (All)
Aliases: DisplayName

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OcrEnabled

Whether optical character recognition is performed on new and updated documents in the Container, so their text can be searched.

```yaml
Type: Boolean
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
[Create fileStorageContainer](https://learn.microsoft.com/graph/api/filestoragecontainer-post)
