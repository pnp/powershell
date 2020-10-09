---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnptenantsequencesite
schema: 2.0.0
title: Get-PnPTenantSequenceSite
---

# Get-PnPTenantSequenceSite

## SYNOPSIS
Returns one ore more sites from a tenant template

## SYNTAX

```powershell
Get-PnPTenantSequenceSite -Sequence <ProvisioningSequence> [-Identity <ProvisioningSitePipeBind>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantSequenceSite -Sequence $mysequence
```

Returns all sites from the specified sequence

### EXAMPLE 2
```powershell
Get-PnPTenantSequenceSite -Sequence $mysequence -Identity 8058ea99-af7b-4bb7-b12a-78f93398041e
```

Returns the specified site from the specified sequence

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
Optional Id of the site

Only applicable to: SharePoint Online

```yaml
Type: ProvisioningSitePipeBind
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Sequence
The sequence to retrieve the site from

Only applicable to: SharePoint Online

```yaml
Type: ProvisioningSequence
Parameter Sets: (All)
Aliases:

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