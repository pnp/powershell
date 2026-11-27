---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPContainerType.html
external help file: PnP.PowerShell.dll-Help.xml
title: Set-PnPContainerType
---

# Set-PnPContainerType

## SYNOPSIS

**Required Permissions**

* SharePoint Embedded Administrator or Global Administrator role is required

Changes the name or the application redirect url of a Container Type in SharePoint Embedded.

## SYNTAX

```powershell
Set-PnPContainerType [-Identity] <Guid> [-ContainerTypeName <String>] [-ApplicationRedirectUrl <String>] [-WhatIf] [-Confirm] [-Connection <PnPConnection>]
```

## DESCRIPTION

Changes only the settings for the parameters that are passed in, and returns the Container Type. At least one setting has to be passed. The billing of a standard Container Type is not changed by this cmdlet.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPContainerType -Identity 4f0af585-8dcc-0000-223d-661eb2c604e4 -ContainerTypeName "Contoso Legal"
```

Renames the specified Container Type.

### EXAMPLE 2
```powershell
Set-PnPContainerType -Identity 4f0af585-8dcc-0000-223d-661eb2c604e4 -ApplicationRedirectUrl "https://contoso.com/legal"
```

Changes the url that files in Containers of the specified Container Type redirect to.

## PARAMETERS

### -ApplicationRedirectUrl

The url of the application that files in Containers of this Container Type redirect to.

```yaml
Type: String
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

### -ContainerTypeName

The new name of the Container Type.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity

The id of the Container Type.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases: ContainerTypeId

Required: True
Position: 0
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
[SharePoint Embedded Container Types](https://learn.microsoft.com/sharepoint/dev/embedded/concepts/app-concepts/containertypes)
