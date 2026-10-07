---
Module Name: PnP.PowerShell
title: Get-PnPSitePolicy
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSitePolicy.html
---
 
# Get-PnPSitePolicy

## SYNOPSIS
Retrieves all or a specific site policy

## SYNTAX

```powershell
Get-PnPSitePolicy [-AllAvailable] [-Name <String>] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to retrieve site policies.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSitePolicy
```

Retrieves the current applied site policy.

### EXAMPLE 2
```powershell
Get-PnPSitePolicy -AllAvailable
```

Retrieves all available site policies.

### EXAMPLE 3
```powershell
Get-PnPSitePolicy -Name "Contoso HBI"
```

Retrieves an available site policy with the name "Contoso HBI".

## PARAMETERS

### -AllAvailable
Retrieve all available site policies

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
Retrieves a site policy with a specific name

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

