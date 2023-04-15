---
Module Name: PnP.PowerShell
title: Test-PnPTenantTemplate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Test-PnPTenantTemplate.html
---
 
# Test-PnPTenantTemplate

## SYNOPSIS
Tests a tenant template for invalid references

## SYNTAX

```powershell
Test-PnPTenantTemplate -Template <ProvisioningHierarchy> [-Connection <PnPConnection>]  
 
```

## DESCRIPTION

Allows to check if the tenant template has invalid references.

## EXAMPLES

### EXAMPLE 1
```powershell
Test-PnPTenantTemplate -Template $myTemplate
```

Checks for valid template references

## PARAMETERS

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

### -Template
The in-memory template to test

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

