---
tags: Available in the current Nightly Release only.
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
title: Add-PnPTenantSearchCrawledProperty
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPTenantSearchCrawledProperty.html
external help file: PnP.PowerShell.dll-Help.xml
---
 
# Add-PnPTenantSearchCrawledProperty

## SYNOPSIS

Adds a tenant-level search crawled property.

## SYNTAX

### KnownPropertySet

```powershell
Add-PnPTenantSearchCrawledProperty -Name <String> -PropertySet <SearchCrawledPropertySet>
 [-Force] [-Connection <PnPConnection>]
```

### PropertySetGuid

```powershell
Add-PnPTenantSearchCrawledProperty -Name <String> -PropertySetGuid <Guid>
 [-Force] [-Connection <PnPConnection>]
```

## DESCRIPTION

Creates a tenant-level crawled property by importing an additive search schema configuration package. This cmdlet must be run against the tenant admin site.

Most SharePoint crawled properties should use one of these property sets: SharePointDefault, SharePointTaxonomy, SharePointStructured, or SharePointRich. Other supported property sets can be specified by GUID for advanced scenarios, but the cmdlet will ask for confirmation unless -Force is specified.

This cmdlet is additive only. SharePoint Online does not expose a supported PnP PowerShell command to delete crawled properties created in error or to move an existing crawled property to a different property set. Verify the crawled property name and property set before running this cmdlet in a production tenant.

If this cmdlet is used to make an implicit crawled property explicit, SharePoint Online will stop automatically creating an implicit managed property for that crawled property going forward.

This cmdlet supports PowerShell's standard -WhatIf and -Confirm parameters. Because creating a tenant crawled property is difficult to undo, the cmdlet asks for confirmation by default. Use -WhatIf to preview the operation or -Confirm:$false to suppress the standard confirmation prompt.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPTenantSearchCrawledProperty -Name "ows_ProjectCode" -PropertySet SharePointDefault
```

Creates a common SharePoint crawled property.

### EXAMPLE 2

```powershell
Add-PnPTenantSearchCrawledProperty -Name "ows_q_TEXT_ProjectCode" -PropertySet SharePointStructured
```

Creates a structured SharePoint crawled property.

### EXAMPLE 3

```powershell
Add-PnPTenantSearchCrawledProperty -Name "ows_taxId_ProjectCategory" -PropertySet SharePointTaxonomy
```

Creates a taxonomy crawled property.

### EXAMPLE 4

```powershell
Add-PnPTenantSearchCrawledProperty -Name "ows_r_HTML_Description" -PropertySet SharePointRich
```

Creates a crawled property for a rich or complex field.

### EXAMPLE 5

```powershell
Add-PnPTenantSearchCrawledProperty -Name "ows_ProjectCode" -PropertySetGuid "00130329-0000-0130-C000-000000131346" -Force
```

Creates a crawled property by specifying the supported property set GUID directly.

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

### -Force

Suppresses confirmation prompts for less common property sets, direct GUID usage, or crawled property names that do not match the selected property set.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name

Name of the crawled property to create.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PropertySet

Known property set to use for the crawled property. Recommended values are SharePointDefault, SharePointTaxonomy, SharePointStructured, and SharePointRich.

```yaml
Type: SearchCrawledPropertySet
Parameter Sets: KnownPropertySet
Accepted values: SharePointDefault, SharePointTaxonomy, SharePointStructured, SharePointRich

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PropertySetGuid

Supported property set GUID to use directly. Prefer -PropertySet for normal usage.

```yaml
Type: Guid
Parameter Sets: PropertySetGuid

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

Microsoft 365 Patterns and Practices https://aka.ms/m365pnp

