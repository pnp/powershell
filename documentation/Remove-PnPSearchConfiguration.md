---
Module Name: PnP.PowerShell
title: Remove-PnPSearchConfiguration
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPSearchConfiguration.html
---
 
# Remove-PnPSearchConfiguration

## SYNOPSIS
Remove the search configuration

## SYNTAX

### Config
```powershell
Remove-PnPSearchConfiguration -Configuration <String> [-Scope <SearchConfigurationScope>] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### Path
```powershell
Remove-PnPSearchConfiguration -Path <String> [-Scope <SearchConfigurationScope>] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSearchConfiguration -Configuration $config
```

Remove the search configuration for the current web (does not remove managed property mappings)

### EXAMPLE 2
```powershell
Remove-PnPSearchConfiguration -Configuration $config -Scope Site
```

Remove the search configuration for the current site collection (does not remove managed property mappings)

### EXAMPLE 3
```powershell
Remove-PnPSearchConfiguration -Configuration $config -Scope Subscription
```

Remove the search configuration for the current tenant (does not remove managed property mappings)

### EXAMPLE 4
```powershell
Remove-PnPSearchConfiguration -Path searchconfig.xml -Scope Subscription
```

Reads the search configuration from the specified XML file and remove it for the current tenant (does not remove managed property mappings)

## PARAMETERS

### -Configuration
Search configuration string

```yaml
Type: String
Parameter Sets: Config

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Path
Path to a search configuration

```yaml
Type: String
Parameter Sets: Path

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope

```yaml
Type: SearchConfigurationScope
Parameter Sets: (All)
Accepted values: Web, Site, Subscription

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

