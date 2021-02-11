---
Module Name: PnP.PowerShell
title: Get-PnPSearchConfiguration
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSearchConfiguration.html
---
 
# Get-PnPSearchConfiguration

## SYNOPSIS
Returns the search configuration

## SYNTAX

### Xml (Default)
```powershell
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-Path <String>] 
 [-Connection <PnPConnection>] [<CommonParameters>]
```

### OutputFormat
```powershell
Get-PnPSearchConfiguration [-Scope <SearchConfigurationScope>] [-OutputFormat <OutputFormat>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSearchConfiguration
```

Returns the search configuration for the current web

### EXAMPLE 2
```powershell
Get-PnPSearchConfiguration -Scope Site
```

Returns the search configuration for the current site collection

### EXAMPLE 3
```powershell
Get-PnPSearchConfiguration -Scope Subscription
```

Returns the search configuration for the current tenant

### EXAMPLE 4
```powershell
Get-PnPSearchConfiguration -Path searchconfig.xml -Scope Subscription
```

Returns the search configuration for the current tenant and saves it to the specified file

### EXAMPLE 5
```powershell
Get-PnPSearchConfiguration -Scope Site -OutputFormat ManagedPropertyMappings
```

Returns all custom managed properties and crawled property mapping at the current site collection

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

### -OutputFormat
Output format for of the configuration. Defaults to complete XML

```yaml
Type: OutputFormat
Parameter Sets: OutputFormat
Accepted values: CompleteXml, ManagedPropertyMappings

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Local path where the search configuration will be saved

```yaml
Type: String
Parameter Sets: Xml

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scope
Scope to use. Either Web, Site, or Subscription. Defaults to Web

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

