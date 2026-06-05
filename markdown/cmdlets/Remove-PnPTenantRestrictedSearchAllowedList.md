---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Remove-PnPTenantRestrictedSearchAllowedList.html
external help file: PnP.PowerShell.dll-Help.xml
title: Remove-PnPTenantRestrictedSearchAllowedList
---
  
# Remove-PnPTenantRestrictedSearchAllowedList

## SYNOPSIS
Removes site URLs from the allowed list when Restricted SharePoint Search is enabled. The URLs can be provided as a string array or read from a CSV file.

## SYNTAX

```powershell
Remove-PnPTenantRestrictedSearchAllowedList [-SitesListFileUrl <String>] [-SitesList <String[]>] [-ContainsHeaders <SwitchParameter>] [-Connection <PnPConnection>] 
```

## DESCRIPTION

Removes site URLs from the allowed list when Restricted SharePoint Search is enabled. The URLs can be provided directly as a string array or read from a CSV file. At present, a maximum of 100 sites can be added to the allowed list.

## EXAMPLES

### EXAMPLE 1
```powershell
Remove-PnPTenantRestrictedSearchAllowedList -SitesListFileUrl "C:\temp\sitelist.csv" -ContainsHeader
```

Removes site URLs from the allowed list from a CSV file. The first line, which is assumed to be a header, is skipped.

Sample CSV file content with Header

SiteUrl
https://contoso.sharepoint.com/sites/Company311
https://contoso.sharepoint.com/sites/contosoportal

### EXAMPLE 2
```powershell
Remove-PnPTenantRestrictedSearchAllowedList -SitesListFileUrl "C:\temp\sitelist.csv" 
```

Removes site URLs from the allowed list from a CSV file.

Sample CSV file content without Header

https://contoso.sharepoint.com/sites/Company311
https://contoso.sharepoint.com/sites/contosoportal


### EXAMPLE 3
```powershell
Remove-PnPTenantRestrictedSearchAllowedList -SitesList @("https://contoso.sharepoint.com/sites/Company311","https://contoso.sharepoint.com/sites/contosoportal")
```
Removes the specified sites from the allowed list.

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

### -SitesListFileUrl
Specifies the path of the CSV file that contains a list of site URLs to be removed from the allowed list when the tenant is set to Restricted Tenant Search Mode.

```yaml
Type: String
Parameter Sets: File

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SitesList
Specifies a collection of sites to remove from the allowed list.

```yaml
Type: String[]
Parameter Sets: SiteList

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Containsheader

If specified, this switch skips the first line from the CSV file, which is assumed to be a header.

```yaml
Type: SwitchParamter
Parameter Sets: File

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[How does Restricted SharePoint Search work?](https://learn.microsoft.com/sharepoint/restricted-sharepoint-search)
[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
