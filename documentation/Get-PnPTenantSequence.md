---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnptenantsequence
schema: 2.0.0
title: Get-PnPTenantSequence
---

# Get-PnPTenantSequence

## SYNOPSIS
Returns one ore more provisioning sequence object(s) from a tenant template

## SYNTAX

```powershell
Get-PnPTenantSequence -Template <ProvisioningHierarchy> [-Identity <ProvisioningSequencePipeBind>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantSequence -Template $myTemplateObject
```

Returns all sequences from the specified tenant template

### EXAMPLE 2
```powershell
Get-PnPTenantSequence -Template $myTemplateObject -Identity "mysequence"
```

Returns the specified sequence from the specified tenant template

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

### -Identity
Optional Id of the sequence

Only applicable to: SharePoint Online

```yaml
Type: ProvisioningSequencePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Template
The template to retrieve the sequence from

Only applicable to: SharePoint Online

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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)