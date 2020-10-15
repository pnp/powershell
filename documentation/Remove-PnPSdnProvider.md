---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/rempve-pnpsdnprovider
schema: 2.0.0
title: Remove-PnPSdnProvider
---

# Remove-PnPSdnProvider

## SYNOPSIS
Removes Software-Defined Networking (SDN) Support in your SharePoint Online tenant.

## SYNTAX

```powershell
Remove-PnPSdnProviderr [-Confirm] [-WhatIf]
```

## DESCRIPTION
Removes SDN Support in your SharePoint Online tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSdnProvider -Confirm:false
```

This command removes the SDN support for your Online Tenant without confirmation.

## PARAMETERS

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)