---
Module Name: PnP.PowerShell
title: New-PnPTenantSequenceCommunicationSite
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTenantSequenceCommunicationSite.html
---
 
# New-PnPTenantSequenceCommunicationSite

## SYNOPSIS
Creates a communication site object

## SYNTAX

```powershell
New-PnPTenantSequenceCommunicationSite -Url <String> -Title <String> [-Language <UInt32>] [-Owner <String>]
 [-Description <String>] [-Classification <String>] [-SiteDesignId <String>] [-HubSite]
 [-AllowFileSharingForGuestUsers] [-TemplateIds <String[]>]   
```

## DESCRIPTION

Allows to create a new communication site object.

## EXAMPLES

### EXAMPLE 1
```powershell
$site = New-PnPTenantSequenceCommunicationSite -Url "/sites/mycommunicationsite" -Title "My Team Site"
```

Creates a new communication site object with the specified variables

## PARAMETERS

### -AllowFileSharingForGuestUsers

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Classification

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -HubSite

```yaml
Type: SwitchParameter
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

### -Owner

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SiteDesignId

```yaml
Type: String
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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

