---
Module Name: PnP.PowerShell
title: Sync-PnPAppToTeams
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Sync-PnPAppToTeams.html
---
 
# Sync-PnPAppToTeams

## SYNOPSIS
Synchronize an app from the tenant app catalog to the Microsoft Teams app catalog

## SYNTAX

```powershell
Sync-PnPAppToTeams [-Identity] <AppMetadataPipeBind> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to Synchronize an app from the tenant app catalog to the Microsoft Teams app catalog.

## EXAMPLES

### EXAMPLE 1
```powershell
Sync-PnPAppToTeams -Identity 99a00f6e-fb81-4dc7-8eac-e09c6f9132fe
```

This will synchronize the given app with the Microsoft Teams app catalog

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
Specifies the Id of the Add-In Instance

```yaml
Type: AppMetadataPipeBind
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

