---
Module Name: PnP.PowerShell
title: Set-PnPSiteScriptPackage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteScriptPackage.html
---
 
# Set-PnPSiteScriptPackage

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Updates an existing Site Script Package on the current tenant.

## SYNTAX

```powershell
Set-PnPSiteScriptPackage -Identity <TenantSiteScriptPipeBind> [-Title <String>] [-Description <String>]
 [-ContentPath <String>] [-Version <Int32>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to update an existing Site Script Package on the current tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteScriptPackage -Identity f1d55d9b-b116-4f54-bc00-164a51e7e47f -Title "My Site Script"
```

Updates an existing Site Script Package and changes the title.


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
The path to the locally stored Site Script Package to upload to SharePoint Online

```yaml
Type: String
Parameter Sets: (All)

Required: False
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

### -Identity
The guid or an object representing the site script package

```yaml
Type: TenantSiteScriptPipeBind
Parameter Sets: (All)

Required: True
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Version
Specifies the version of the site script package

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

