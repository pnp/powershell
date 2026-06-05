---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteScriptPackage.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPSiteScriptPackage
---
  
# Add-PnPSiteScriptPackage

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new Site Script Package on the current tenant. Site script packages can contain files in addition to the site scripts which can be used to upload files to sites on which a site template gets applied.

## SYNTAX

```powershell
Add-PnPSiteScriptPackage -Title <String> [-Description <String>] -ContentPath <String> [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add a new Site Script Package on the current tenant

## EXAMPLES

### EXAMPLE 1
```powershell
Add-PnPSiteScriptPackage -Title "My Site Script Package" -Description "A more detailed description" -ContentPath "c:\package.zip"
```

Adds a new Site Script Package

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

### -ContentPath
The full path to the locally stored Site Script Package to upload

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description of the site script package

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Title
The title of the site script package

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


