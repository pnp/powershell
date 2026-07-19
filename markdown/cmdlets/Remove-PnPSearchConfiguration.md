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
Removes the search configuration.

## SYNTAX

### Config
```powershell
Remove-PnPSearchConfiguration -Configuration <String> [-Scope <SearchConfigurationScope>] 
 [-Connection <PnPConnection>]
```

### Path
```powershell
Remove-PnPSearchConfiguration -Path <String> [-Scope <SearchConfigurationScope>] 
 [-Connection <PnPConnection>]
```

## DESCRIPTION
This cmdlet removes the search configuration from a single web, site collection or a tenant, using path or a configuration string.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPSearchConfiguration -Configuration $config
```

Removes the search configuration for the current web (does not remove managed property mappings).

### EXAMPLE 2
```powershell
Remove-PnPSearchConfiguration -Configuration $config -Scope Site
```

Removes the search configuration for the current site collection (does not remove managed property mappings).

### EXAMPLE 3
```powershell
Remove-PnPSearchConfiguration -Configuration $config -Scope Subscription
```

Removes the search configuration for the current tenant (does not remove managed property mappings).

### EXAMPLE 4
```powershell
Remove-PnPSearchConfiguration -Path searchconfig.xml -Scope Subscription
```

Reads the search configuration from the specified XML file and removes it for the current tenant (does not remove managed property mappings).

## PARAMETERS

### -Configuration
Search configuration string.

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
Path to the search configuration.

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
Scope to remove the configuration from. The default is Web.

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
