---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Add-PnPContentTypesFromContentTypeHub.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPCompatibleHubContentTypes
---
  
# Get-PnPCompatibleHubContentTypes

## SYNOPSIS

**Required Permissions**

  * ViewPages permission on the current web.

Returns the list of content types present in content type hub site that can be added to the root web or a list on a target site.

## SYNTAX

```powershell
 Get-PnPCompatibleHubContentTypes -WebUrl <String> [-ListUrl <String>] [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
 Get-PnPCompatibleHubContentTypes -WebUrl 'https://contoso.sharepoint.com/web1'
```

This will return the list of content types present in content type hub site that can be added to the root web of the site to which the provided web belongs.

### EXAMPLE 2
```powershell
 Get-PnPCompatibleHubContentTypes -WebUrl 'https://contoso.sharepoint.com/web1' -ListUrl 'https://contoso.sharepoint.com/web1/Shared Documents'
```

This will return the list of content types present in content type hub site that can be added to the provided list.

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

### -WebUrl
The web url for which compatible content types need to be fetched. In case of a list this should be the url of the web which contains the given list.

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ListUrl
The list url for which compatible content types need to be fetched

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)