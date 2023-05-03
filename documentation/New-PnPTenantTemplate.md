---
Module Name: PnP.PowerShell
title: New-PnPTenantTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTenantTemplate.html
---
 
# New-PnPTenantTemplate

## SYNOPSIS
Creates a new tenant template object

## SYNTAX

```powershell
New-PnPTenantTemplate [-Author <String>] [-Description <String>] [-DisplayName <String>] [-Generator <String>]
   
```

## DESCRIPTION

Allows to create a new tenant template object.

## EXAMPLES

### EXAMPLE 1
```powershell
$template = New-PnPTenantTemplate
```

Creates a new instance of a tenant template object.

## PARAMETERS

### -Author

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Generator

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

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

