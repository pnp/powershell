---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnptenantsequence
schema: 2.0.0
title: New-PnPTenantSequence
---

# New-PnPTenantSequence

## SYNOPSIS
Creates a new tenant sequence object

## SYNTAX

```powershell
New-PnPTenantSequence [-Id <String>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
$sequence = New-PnPTenantSequence
```

Creates a new instance of a tenant sequence object.

### EXAMPLE 2
```powershell
$sequence = New-PnPTenantSequence -Id "MySequence"
```

Creates a new instance of a tenant sequence object and sets the Id to the value specified.

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

### -Id
Optional Id of the sequence

```yaml
Type: String
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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)