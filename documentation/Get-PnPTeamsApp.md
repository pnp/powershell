---
Module Name: PnP.PowerShell
title: Get-PnPTeamsApp
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsApp.html
---
 
# Get-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of AppCatalog.Read.All, Directory.ReadWrite.All

Gets one Microsoft Teams App or a list of all apps.

## SYNTAX

```powershell
Get-PnPTeamsApp [-Identity <TeamsAppPipeBind>]  
```

## DESCRIPTION

Allows to retrieve Microsoft Teams apps. By using `Identity` option it is possible to retrieve a specific app.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTeamsApp
```

Retrieves all the Microsoft Teams Apps

### EXAMPLE 2
```powershell
Get-PnPTeamsApp -Identity a54224d7-608b-4839-bf74-1b68148e65d4
```

Retrieves a specific Microsoft Teams App

### EXAMPLE 3
```powershell
Get-PnPTeamsApp -Identity "MyTeamsApp"
```

Retrieves a specific Microsoft Teams App

## PARAMETERS

### -Identity
Specify the name, id or external id of the app.

```yaml
Type: TeamsAppPipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

