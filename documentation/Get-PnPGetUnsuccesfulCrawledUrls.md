---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPFooter.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPFooter
---
  
# Get-PnPGetUnsuccesfulCrawledUrls


## SYNOPSIS

Retrieve a list of URLs that failed to be indexed during a search crawl, which is useful for diagnosing search issues. 
> Make sure you are granted access to the crawl log via the SharePoint search admin center at https://<tenant>-admin.sharepoint.com/_layouts/15/searchadmin/crawllogreadpermission.aspx in order to run this cmdlet.

## SYNTAX

```powershell
Get-PnPGetUnsuccesfulCrawledUrls [-Filter <String>]  [-StartDate <DateTime>] [-EndDate <DateTime>] [-RawFormat]
 [-IncreaseRequestTimeout]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION

Enables retrieval of items that failed to be indexed during a search crawl. This is particularly useful when processing large lists or libraries and encountering request timeouts. By focusing exclusively on errors, you can reliably identify issues without additional effort to narrow the query scope.

> This command relies on `DocumentCrawlLog.GetUnsuccesfulCrawledUrls` undocumented method. 

### EXAMPLE 1
```powershell
Get-PnPGetUnsuccesfulCrawledUrls
```

Returns all (?) crawl log errors for site content. During tests, more than 3000 items were returned.

### EXAMPLE 2
```powershell
Get-PnPGetUnsuccesfulCrawledUrls -Filter "https://contoso-my.sharepoint.com/sites/Intranet"
```
Returns all (?) crawl log errors for the specified site.

### EXAMPLE 3
```powershell
Get-PnPGetUnsuccesfulCrawledUrls -StartDate (Get-Date).AddDays(-10)
```

Returns all (?) crawl log errors, starting from 10 days ago. 

> Based on the author's test results and Copilot's input 😉, the `DocumentCrawlLog` methods don't respect the time component in `StartDate` and `EndDate`. They only use the date portion for filtering. Internally, the crawl log is grouped by crawl day, so any hour/minute you provide is ignored. The CSOM API (GetCrawledUrls) accepts DateTime values, but the backend partitions data by date, not timestamp.

### EXAMPLE 4
```powershell
$ClientID= "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
$env:SharePointPnPHttpTimeout = -1 #👈

Connect-PnPOnline -Url https://<tenant>-admin.sharepoint.com/ -Interactive -ClientId $ClientID -ErrorAction Stop # 👈

Get-PnPGetUnsuccesfulCrawledUrls -Filter "https://contoso-my.sharepoint.com/sites/Intranet"
```

Increases the Request Timeout allowing the call to last up to 3 minutes. The `ClientRuntimeContext` enforces a three-minute limit; when increasing the timeout to its maximum of three minutes, this threshold may still be exceeded.


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


### -EndDate
End date to stop getting entries from. Default to current time.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Filter
Filter to limit what is being returned. Has to be a URL prefix for SharePoint content. Wildcard characters are not supported.

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -RawFormat
Show raw crawl log data

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


### -StartDate
Start date to start getting entries from. Defaults to start of time.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncreaseRequestTimeout

```yaml
Type: Switch
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

Increases timeout to maximum 3 minutes.
The `ClientRuntimeContext` enforces a three-minute limit; when increasing the timeout to its maximum of three minutes, this threshold may still be exceeded.

> Note: Before running Get-PnPUnsuccessfulCrawledUrls with -IncreaseRequestTimeout, you must set $env:SharePointPnPHttpTimeout = -1 to remove the default HttpClient timeout. Then establish a new PnP connection because the environment variable is only applied when the session initializes.

```powershell
$ClientID= "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
$env:SharePointPnPHttpTimeout = -1

Connect-PnPOnline -Url https://<tenant>-admin.sharepoint.com/ -Interactive -ClientId $ClientID -ErrorAction Stop

$scope="https://contoso-my.sharepoint.com/sites/Intranet"
Get-PnPGetUnsuccesfulCrawledUrls -Filter $scope
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

