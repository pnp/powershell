---
Module Name: PnP.PowerShell
title: Get-PnPSiteSearchQueryResults
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteSearchQueryResults.html
---
 
# Get-PnPSiteSearchQueryResults

## SYNOPSIS
Executes a search query to retrieve indexed site collections

## SYNTAX

### Limit (Default)
```powershell
Get-PnPSiteSearchQueryResults [[-Query] <String>] [-StartRow <Int32>] [-MaxResults <Int32>]
 [-Connection <PnPConnection>] 
```

### All
```powershell
Get-PnPSiteSearchQueryResults [[-Query] <String>] [-All] [-Connection <PnPConnection>]
 
```

## DESCRIPTION

Allows to run a search query to retrieve indexed site collections.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPSiteSearchQueryResults
```

Returns the top 500 site collections indexed by SharePoint Search

### EXAMPLE 2
```powershell
Get-PnPSiteSearchQueryResults -Query "WebTemplate:STS"
```

Returns the top 500 site collections indexed by SharePoint Search which have are based on the STS (Team Site) template

### EXAMPLE 3
```powershell
Get-PnPSiteSearchQueryResults -Query "WebTemplate:SPSPERS"
```

Returns the top 500 site collections indexed by SharePoint Search which have are based on the SPSPERS (MySite) template, up to the MaxResult limit

### EXAMPLE 4
```powershell
Get-PnPSiteSearchQueryResults -Query "Title:Intranet*"
```

Returns the top 500 site collections indexed by SharePoint Search of which the title starts with the word Intranet

### EXAMPLE 5
```powershell
Get-PnPSiteSearchQueryResults -MaxResults 10
```

Returns the top 10 site collections indexed by SharePoint Search

### EXAMPLE 6
```powershell
Get-PnPSiteSearchQueryResults -All
```

Returns absolutely all site collections indexed by SharePoint Search

## PARAMETERS

### -All
Automatically page results until the end to get more than 500 sites. Use with caution!

```yaml
Type: SwitchParameter
Parameter Sets: All

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

### -MaxResults
Maximum amount of search results to return. Default and max is 500 search results.

```yaml
Type: Int32
Parameter Sets: Limit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Query
Search query in Keyword Query Language (KQL) to execute to refine the returned sites. If omitted, all indexed sites will be returned.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -StartRow
Search result item to start returning the results from. Useful for paging. Leave at 0 to return all results.

```yaml
Type: Int32
Parameter Sets: Limit

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```



## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

