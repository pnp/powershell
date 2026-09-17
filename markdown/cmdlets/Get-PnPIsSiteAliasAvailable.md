---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPIsSiteAliasAvailable.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPIsSiteAliasAvailable
---
  
# Get-PnPIsSiteAliasAvailable

## SYNOPSIS
Validates if a certain alias is still available to be used to create a new site collection for. If it is not, it will propose an alternative alias and URL which is still available.

## SYNTAX

```powershell
Get-PnPIsSiteAliasAvailable [-Identity <String>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPIsSiteAliasAvailable -Identity "HR"
```

Validates if the alias "HR" is still available to be used

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
Alias you want to check for if it is still available to create a new site collection for

```yaml
Type: String
Parameter Sets: (All)
Aliases: Alias

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


