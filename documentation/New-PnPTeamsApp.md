---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnpteamsapp
schema: 2.0.0
title: New-PnPTeamsApp
---

# New-PnPTeamsApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of AppCatalog.ReadWrite.All, Directory.ReadWrite.All

Adds an app to the Teams App Catalog.

## SYNTAX

```powershell
New-PnPTeamsApp -Path <String> [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPTeamsApp -Path c:\myapp.zip
```

Adds the app as defined in the zip file to the Teams App Catalog

## PARAMETERS

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)