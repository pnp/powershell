---
Module Name: PnP.PowerShell
title: Get-PnPTenantTheme
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantTheme.html
---
 
# Get-PnPTenantTheme

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns all or a specific theme

## SYNTAX

```powershell
Get-PnPTenantTheme [[-Name] <String>] [-AsJson] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns all or a specific tenant theme.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantTheme
```

Returns all themes.

### EXAMPLE 2
```powershell
Get-PnPTenantTheme -Name "MyCompanyTheme"
```

Returns the specified theme.

### EXAMPLE 3
```powershell
Get-PnPTenantTheme -Name "MyCompanyTheme" -AsJson
```

Returns the specified theme as json.

## PARAMETERS

### -AsJson
The theme is returned in json format.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: 1
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

### -Name
The name of the theme to retrieve.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

