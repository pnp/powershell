---
Module Name: PnP.PowerShell
title: New-PnPTenantSequence
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTenantSequence.html
---
 
# New-PnPTenantSequence

## SYNOPSIS
Creates a new tenant sequence object

## SYNTAX

```powershell
New-PnPTenantSequence [-Id <String>]   
```

## DESCRIPTION

Allows to create a new tenant sequence object.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

