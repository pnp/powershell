---
schema: 2.0.0
external help file: PnP.PowerShell.dll-Help.xml
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
title: Set-PnPMultiGeoCompanyAllowedDataLocation
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPMultiGeoCompanyAllowedDataLocation.html
---
  
# Set-PnPMultiGeoCompanyAllowedDataLocation

## SYNOPSIS
Starts setting up a multi-geo data location for the SharePoint Online tenant.

## SYNTAX

```powershell
Set-PnPMultiGeoCompanyAllowedDataLocation [-Location] <String> [-InitialDomain] <String> [-Connection <PnPConnection>] [-WhatIf] [-Confirm]
```

## DESCRIPTION
Starts setting up an allowed SharePoint Online multi-geo data location for the tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPMultiGeoCompanyAllowedDataLocation -Location EUR -InitialDomain contoso.onmicrosoft.com
```

Starts setting up the EUR multi-geo data location for the tenant with the initial domain `contoso.onmicrosoft.com`.

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

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InitialDomain
Specifies the initial SharePoint Online domain for the data location.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Location
Specifies the multi-geo location code to set up.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

## OUTPUTS

### System.String
Returns a message indicating that setting up the new location has started.

## RELATED LINKS

[Get-PnPMultiGeoCompanyAllowedDataLocation](Get-PnPMultiGeoCompanyAllowedDataLocation.md)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

