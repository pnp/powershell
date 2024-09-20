---
Module Name: PnP.PowerShell
title: Remove-PnPManagedAppId
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPManagedAppId.html
---
 
# Remove-PnPManagedAppId

## SYNOPSIS
Removes an App Id from the Credential Manager

## SYNTAX

```powershell
Remove-PnPManagedAppId -Url <String> [-Force] 
```

## DESCRIPTION
Removes an App Id from the Credential Manager

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPManagedAppId -Url "https://tenant.sharepoint.com"
```

Removes the specified App Id from the Credential Manager

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

### -Url
The Url for which to remove the App Id

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

