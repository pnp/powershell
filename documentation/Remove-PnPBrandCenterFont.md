---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPBrandCenterFont.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPBrandCenterFont
---
  
# Remove-PnPBrandCenterFont

## SYNOPSIS

**Required Permissions**

* SharePoint Administrator

Removes a font file from the tenant's Brand Center fonts.

## SYNTAX

```powershell
Remove-PnPBrandCenterFont [-FileName] <String> [-WhatIf] [-Confirm] [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Removes a font file from the tenant's brand fonts library (Tenant.RemoveBrandFontFile). It accepts the fonts returned by [Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md) through the pipeline, using their FileName.

Returns the results SharePoint reports, each with GeoLocation, Status and Message (on a multi-geo tenant, one per geo location).

It does not change the Office font library. To stop Office apps offering a font, remove it from Brand Center, then run [Sync-PnPBrandCenterOfficeFont](Sync-PnPBrandCenterOfficeFont.md) -RemoveMissing.

It asks for confirmation; use -Confirm:$false to skip it.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPBrandCenterFont -FileName "Contoso-Regular.ttf"
```

Removes Contoso-Regular.ttf from the Brand Center fonts and shows the result per geo location.

### EXAMPLE 2
```powershell
"Contoso-Regular.ttf", "Contoso-Bold.ttf" | Remove-PnPBrandCenterFont -Confirm:$false
```

Removes two font files, given through the pipeline, without asking for confirmation.

### EXAMPLE 3
```powershell
Get-PnPBrandCenterFont | Where-Object Name -eq "Contoso Old" | Remove-PnPBrandCenterFont
```

Removes every file of the "Contoso Old" font family, asking for confirmation for each one.

### EXAMPLE 4
```powershell
Get-PnPBrandCenterFont | Where-Object IsHidden | Remove-PnPBrandCenterFont -WhatIf
```

Shows which hidden fonts would be removed, without removing them.

## PARAMETERS

### -Confirm
Prompts you for confirmation before running the cmdlet. It is on by default.

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

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing [Get-PnPConnection](Get-PnPConnection.md).

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FileName
File name of the font to remove, for example "Contoso-Regular.ttf".

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue, ByPropertyName)
Accept wildcard characters: False
```

## RELATED LINKS

[Add-PnPBrandCenterFont](Add-PnPBrandCenterFont.md)

[Get-PnPBrandCenterFont](Get-PnPBrandCenterFont.md)

[Sync-PnPBrandCenterOfficeFont](Sync-PnPBrandCenterOfficeFont.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
