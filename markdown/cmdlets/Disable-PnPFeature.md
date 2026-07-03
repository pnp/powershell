---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Disable-PnPFeature.html
external help file: PnP.PowerShell.dll-Help.xml
title: Disable-PnPFeature
---
  
# Disable-PnPFeature

## SYNOPSIS
Disables a feature

## SYNTAX

```powershell
Disable-PnPFeature [-Identity] <Guid> [-Force] [-Scope <FeatureScope>] 
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Deactivates a feature that was active on a site

## EXAMPLES

### EXAMPLE 1
```powershell
Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe"

### EXAMPLE 2
```powershell
Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Force
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with force.

### EXAMPLE 3
```powershell
Disable-PnPFeature -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe -Scope Web
```

This will disable the feature with the id "99a00f6e-fb81-4dc7-8eac-e09c6f9132fe" with the web scope.

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
Specifies whether to continue if an error occurs when deactivating the feature.

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
The id of the feature to disable.

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Specify the scope of the feature to deactivate, either Web or Site. Defaults to Web.

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


