---
Module Name: PnP.PowerShell
title: Remove-PnPSdnProvider
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSdnProvider.html
---
 
# Remove-PnPSdnProvider

## SYNOPSIS
Removes Software-Defined Networking (SDN) Support in your SharePoint Online tenant.

## SYNTAX

```powershell
Remove-PnPSdnProvider [-Confirm]
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

