---
Module Name: PnP.PowerShell
title: Get-PnPTenantSequence
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantSequence.html
---
 
# Get-PnPTenantSequence

## SYNOPSIS
Returns one ore more provisioning sequence object(s) from a tenant template

## SYNTAX

```powershell
Get-PnPTenantSequence -Template <ProvisioningHierarchy> [-Identity <ProvisioningSequencePipeBind>] 
  
```

## DESCRIPTION

Allows to retrieve provisioning sequence objects from a tenant template. By using `Identity` option it is possible to retrieve a specific provisioning sequence object.

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

### -Identity
Optional Id of the sequence

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

```yaml
Type: ProvisioningHierarchy
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

