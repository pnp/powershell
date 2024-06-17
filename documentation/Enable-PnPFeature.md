---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPFeature.html
external help file: PnP.PowerShell.dll-Help.xml
title: Enable-PnPFeature
---
  
# Enable-PnPFeature

## SYNOPSIS
Enables a feature

## SYNTAX

```powershell
Enable-PnPFeature [-Identity] <Guid> [-Force] [-Scope <FeatureScope>] [-Sandboxed] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to enable to feature.

## EXAMPLES

### EXAMPLE 1
```powershell
Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will enable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"

### EXAMPLE 2
```powershell
Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Force
```

This will enable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with force.

### EXAMPLE 3
```powershell
Enable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Web
```

This will enable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with the web scope.

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

### -Force
Specifies whether to overwrite an existing feature with the same feature identifier. This parameter is ignored if there are no errors.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Identity
The id of the feature to enable.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Sandboxed
Specify this parameter if the feature you're trying to activate is part of a sandboxed solution.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Specify the scope of the feature to activate, either Web or Site. Defaults to Web.

```yaml
Type: FeatureScope
Parameter Sets: (All)
Accepted values: Web, Site

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


