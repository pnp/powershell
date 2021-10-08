---
Module Name: PnP.PowerShell
title: Remove-PnPOrgAssetsLibrary
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPOrgAssetsLibrary.html
---
 
# Remove-PnPOrgAssetsLibrary

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a given document library as a organizational asset source

## SYNTAX

```powershell
Remove-PnPOrgAssetsLibrary -LibraryUrl <String> [-ShouldRemoveFromCdn <Boolean>] [-CdnType <SPOTenantCdnType>]
 [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Removes a given document library as a organizational asset source based on its server relative URL in your SharePoint Online Tenant. It will not remove the document library itself. It may take some time before this change will be reflected in the webinterface.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPOrgAssetsLibrary -LibraryUrl "sites/branding/logos"
```

This example removes the document library with the url "logos" residing in the sitecollection with the url "sites/branding/logos" from the list with organizational assets keeping it as an Office 365 CDN source

### EXAMPLE 2
```powershell
Remove-PnPOrgAssetsLibrary -LibraryUrl "sites/branding/logos" -ShouldRemoveFromCdn $true
```

This example removes the document library with the url "logos" residing in the sitecollection with the url "sites/branding/logos" from the list with organizational assets also removing it as a Public Office 365 CDN source

### EXAMPLE 3
```powershell
Remove-PnPOrgAssetsLibrary -LibraryUrl "sites/branding/logos" -ShouldRemoveFromCdn $true -CdnType Private
```

This example removes the document library with the url "logos" residing in the sitecollection with the url "sites/branding/logos" from the list with organizational assets also removing it as a Private Office 365 CDN source

## PARAMETERS

### -CdnType
Indicates what type of Office 365 CDN source the document library that will no longer be flagged as an organizational asset was of

```yaml
Type: SPOTenantCdnType
Parameter Sets: (All)
Accepted values: Public, Private

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -LibraryUrl
The server relative url of the document library flagged as organizational asset which you want to remove, i.e. "sites/branding/logos"

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ShouldRemoveFromCdn
Boolean indicating if the document library that will no longer be flagged as an organizational asset also needs to be removed as an Office 365 CDN source

```yaml
Type: Boolean
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

