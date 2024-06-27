---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantSequenceSite.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPTenantSequenceSite
---
  
# Add-PnPTenantSequenceSite

## SYNOPSIS
Adds an existing tenant sequence site object to a tenant template

## SYNTAX

```powershell
Add-PnPTenantSequenceSite -Site <ProvisioningSitePipeBind> -Sequence <ProvisioningSequence> 
  
```

## DESCRIPTION

Allows to add an existing tenant sequence site object to a tenant template.

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPTenantSequenceSite -Site $myteamsite -Sequence $mysequence
```

Adds an existing site object to an existing template sequence

## PARAMETERS

### -Sequence
The sequence to add the site to

```yaml
Type: ProvisioningSequence
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Site

```yaml
Type: ProvisioningSitePipeBind
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)