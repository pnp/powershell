---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTeamsApp.html
tags: Available in the current Nightly Release only.
title: Get-PnPTeamsApp
---
  
# Get-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of AppCatalog.Submit, AppCatalog.Read.All, AppCatalog.ReadWrite.All, Directory.Read.All, or Directory.ReadWrite.All (delegated), or one of AppCatalog.Read.All or AppCatalog.ReadWrite.All (application)

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


