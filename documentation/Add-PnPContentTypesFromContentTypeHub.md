---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPContentTypesFromContentTypeHub.html
external help file: PnP.PowerShell.dll-Help.xml
title: Add-PnPContentTypesFromContentTypeHub
---
  
# Add-PnPContentTypesFromContentTypeHub

## SYNOPSIS

**Required Permissions**

  * ManageLists permission on the current site or the content type hub site.

Adds published content types from content type hub site to current site. If the content type already exists on the current site then the latest published version of the content type will be synced to the site.

## SYNTAX

```powershell
Add-PnPContentTypesFromContentTypeHub -ContentTypes List<String> [-Site <SitePipeBind>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to add content types from content type hub site to current site. In case the same content type is already present on the current site then the latest published version will be used.

## EXAMPLES

### EXAMPLE 1
```powershell
 Add-PnPContentTypesFromContentTypeHub -ContentTypes "0x0101", "0x01"
```

This will add the content types with the ids '0x0101' and '0x01' to the current site. Latest published version of these content types will be synced if they were already present in the current site.

- There's an issue with this cmdlet if you use it on private channel sites. The workaround for that is to execute the below command:
  - `Enable-PnPFeature -Identity 73ef14b1-13a9-416b-a9b5-ececa2b0604c -Scope Site -Force`

### EXAMPLE 2
```powershell
 Add-PnPContentTypesFromContentTypeHub -ContentTypes "0x010057C83E557396744783531D80144BD08D" -Site https://tenant.sharepoint.com/sites/HR
```

This will add the content type with the id '0x010057C83E557396744783531D80144BD08D' to the site with the provided URL. Latest published version of these content types will be synced if they were already present in the current site.

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

### -ContentTypes
The list of content type ids present in content type hub site that are required to be added/synced to the current site.

```yaml
Type: List<String>
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Site
The site to which to add the content types coming from the hub. If omitted, it will be applied to the currently connected to site.

```yaml
Type: SitePipeBind
Parameter Sets: (All)

Required: False
Position: Named
Default value: Currently connected to site
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
