---
Module Name: PnP.PowerShell
title: New-PnPTenantSequenceTeamNoGroupSubSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTenantSequenceTeamNoGroupSubSite.html
---
 
# New-PnPTenantSequenceTeamNoGroupSubSite

## SYNOPSIS
Creates a team site subsite with no Microsoft 365 group object

## SYNTAX

```powershell
New-PnPTenantSequenceTeamNoGroupSubSite -Url <String> -Title <String> -TimeZoneId <UInt32> [-Language <UInt32>]
 [-Description <String>] [-TemplateIds <String[]>] [-QuickLaunchDisabled]
 [-UseDifferentPermissionsFromParentSite]   
```

## DESCRIPTION

Allows to create a new team site subsite with no Microsoft 365 group object.

## EXAMPLES

### EXAMPLE 1
```powershell
$site = New-PnPTenantSequenceTeamNoGroupSubSite -Url "MyTeamSubsite" -Title "My Team Site" -TimeZoneId 4
```

Creates a new team site subsite object with the specified variables

## PARAMETERS

### -Description

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Language

```yaml
Type: UInt32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -QuickLaunchDisabled

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TemplateIds

```yaml
Type: String[]
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TimeZoneId

```yaml
Type: UInt32
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseDifferentPermissionsFromParentSite

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

