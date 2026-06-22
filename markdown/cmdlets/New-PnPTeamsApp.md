---
tags: Available in the current Nightly Release only.
applicable: SharePoint Online
Module Name: PnP.PowerShell
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/New-PnPTeamsApp.html
title: New-PnPTeamsApp
external help file: PnP.PowerShell.dll-Help.xml
---
  
# New-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of AppCatalog.ReadWrite.All, Directory.ReadWrite.All

Adds an app to the Teams App Catalog.

## SYNTAX

```powershell
New-PnPTeamsApp -Path <String>  
```

## DESCRIPTION

Allows to add an app to the Teams App Catalog.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTeamsApp -Path c:\myapp.zip
```

Adds the app as defined in the zip file to the Teams App Catalog

## PARAMETERS

### -Path
The path pointing to the packaged/zip file containing the app

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


