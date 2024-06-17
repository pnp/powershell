---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantSequence.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTenantSequence
---
  
# Add-PnPTenantSequence

## SYNOPSIS
Adds a tenant sequence object to a tenant template

## SYNTAX

```powershell
Add-PnPTenantSequence -Template <ProvisioningHierarchy> -Sequence <ProvisioningSequence>  
 
```

## DESCRIPTION

Allows to add a tenant sequence object to a tenant template.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTenantSequence -Template $mytemplate -Sequence $mysequence
```

Adds an existing sequence object to an existing template object

### EXAMPLE 2
```powershell
New-PnPTenantSequence -Id "MySequence" | Add-PnPTenantSequence -Template $template
```

Creates a new instance of a provisioning sequence object and sets the Id to the value specified, then the sequence is added to an existing template object

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

### -Sequence
Optional Id of the sequence

```yaml
Type: ProvisioningSequence
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Template
The template to add the sequence to

```yaml
Type: ProvisioningHierarchy
Parameter Sets: (All)

Required: True
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


