---
Module Name: PnP.PowerShell
title: Get-PnPOrgAssetsLibrary
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPOrgAssetsLibrary.html
---
 
# Get-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns the list of all the configured organizational asset libraries

## SYNTAX

```powershell
Get-PnPOrgAssetsLibrary [-Connection <PnPConnection>] 
```

## DESCRIPTION

Allows to retrieve list of all the configured organizational asset libraries.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPOrgAssetsLibrary
```

Returns the list of all the configured organizational asset sites

### EXAMPLE 2
```powershell
(Get-PnPOrgAssetsLibrary)[0].OrgAssetsLibraries[0].LibraryUrl.DecodedUrl
```

Returns the server relative url of the first document library which has been flagged as organizational asset library, i.e. "sites/branding/logos"

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

