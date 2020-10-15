---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/remove-pnpstoredcredential
schema: 2.0.0
title: Remove-PnPStoredCredential
---

# Remove-PnPStoredCredential

## SYNOPSIS
Removes a credential

## SYNTAX

```powershell
Remove-PnPStoredCredential -Name <String> [-Force] [<CommonParameters>]
```

## DESCRIPTION
Removes a stored credential from the Windows Credential Manager

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPStoredCredential -Name https://tenant.sharepoint.com
```

Removes the specified credential from the Windows Credential Manager

## PARAMETERS

### -Force
If specified you will not be asked for confirmation

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The credential to remove

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)