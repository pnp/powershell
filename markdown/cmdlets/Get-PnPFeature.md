---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFeature.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFeature
---
  
# Get-PnPFeature

## SYNOPSIS
Returns all activated or a specific activated feature

## SYNTAX

```powershell
Get-PnPFeature [[-Identity] <FeaturePipeBind>] [-Scope <FeatureScope>] 
 [-Connection <PnPConnection>] [-Includes <String[]>] 
```

## DESCRIPTION
This cmdlet returns all activated features or a specific activated feature.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPFeature
```

This will return all activated web scoped features

### EXAMPLE 2
```powershell
Get-PnPFeature -Scope Site
```

This will return all activated site scoped features

### EXAMPLE 3
```powershell
Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22
```

This will return a specific activated web scoped feature

### EXAMPLE 4
```powershell
Get-PnPFeature -Identity fb689d0e-eb99-4f13-beb3-86692fd39f22 -Scope Site
```

This will return a specific activated site scoped feature

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

### -Identity
The feature ID or name to query for, Querying by name is not supported in version 15 of the Client Side Object Model

```yaml
Type: FeaturePipeBind
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scope
The scope of the feature. Defaults to Web.

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

### -Includes
Optionally allows properties to be retrieved for the returned feature which are not included in the response by default

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


