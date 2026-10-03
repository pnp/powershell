---
Module Name: PnP.PowerShell
title: Test-PnPMicrosoft365GroupAliasIsUsed
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Test-PnPMicrosoft365GroupAliasIsUsed.html
---
 
# Test-PnPMicrosoft365GroupAliasIsUsed

## SYNOPSIS
Tests if a given alias is already used.

## SYNTAX

```powershell
Test-PnPMicrosoft365GroupAliasIsUsed -Alias <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION
This command allows you to test if a provided alias is used or free, helps decide if it can be used as part of connecting an Microsoft 365 group to an existing classic site collection.

## EXAMPLES

### EXAMPLE 1
```powershell
Test-PnPMicrosoft365GroupAliasIsUsed -Alias "MyGroup"
```

This will test if the alias MyGroup is already used

## PARAMETERS

### -Alias
Specifies the alias of the group. Cannot contain spaces.

```yaml
Type: String
Parameter Sets: (All)

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

