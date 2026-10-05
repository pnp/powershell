# Throttling and retries

SharePoint Online and Microsoft Graph throttle clients that send too many requests: they answer with `429 Too Many Requests` or `503 Service Unavailable`, usually with a `Retry-After` header saying how many seconds to wait. PnP PowerShell retries throttled requests for you, so a script does not need its own retry loop for a short throttle. A long one can still make a cmdlet fail, see [The request timeout](#the-request-timeout).

## How PnP PowerShell retries

Requests to SharePoint Online, through CSOM as well as through the REST API, and requests to Microsoft Graph all pass through the same retry handler in PnP Framework. When a response has status `429`, `503` or `504`, or the connection drops, the handler:

- waits the number of seconds given in the `Retry-After` header, or 1, 2, 4, 8 and so on seconds when there is none, with no single wait longer than 300 seconds
- retries the request up to 10 times, as long as the request timeout allows, after which the cmdlet fails with `Too many http request retries: 10`

Requests that change data, such as `POST` requests, are retried as well.

Cmdlets built on the PnP Core SDK retry in the same way, starting with a 3 second wait. In a [batch](batching.md), throttled Microsoft Graph requests are retried with that same 3 second starting wait, but without looking at `Retry-After`. A SharePoint batch is retried as a whole.

## The request timeout

A request and all of its retries have to finish within the HTTP timeout of 100 seconds. When throttling lasts longer, the cmdlet fails with `The request was canceled due to the configured HttpClient.Timeout of 100 seconds elapsing.` instead of retrying further. Without `Retry-After`, the waits of 1, 2, 4, 8, 16 and 32 seconds already add up to 63 seconds, so about six retries fit in that time, and a `Retry-After` of 100 seconds or more leaves no room for a retry at all.

For long running jobs, set the `SharePointPnPHttpTimeout` environment variable to the timeout in seconds, or to `-1` for no timeout, before connecting. It does not apply to cmdlets built on the PnP Core SDK, which keep a timeout of 100 seconds.

```powershell
$env:SharePointPnPHttpTimeout = 600
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId "<client id>" -Interactive
```

## Identifying your traffic

Microsoft gives precedence to traffic that identifies the application sending it through its `User-Agent` header. PnP PowerShell identifies most of its CSOM requests as `NONISV|SharePointPnP|PnPPS/<version>`, its REST API and Microsoft Graph requests as `NONISV|SharePointPnP|PnPCore/<version>`, and the requests of cmdlets built on the PnP Core SDK as `NONISV|SharePointPnP|PnPCoreSDK/<version>`.

To identify a script as your own, set the `SharePointPnPUserAgent` environment variable in the format `NONISV|CompanyName|AppName/Version` before the first connection in the PowerShell session. It replaces the `PnPCore` value only.

```powershell
$env:SharePointPnPUserAgent = "NONISV|Contoso|SiteInventory/1.0"
```

## Seeing retries happen

Retries do not show with `-Verbose`. The retries described above are written to the [trace log](logging.md) at the default `Information` level, apart from those of cmdlets built on the PnP Core SDK:

```powershell
Start-PnPTraceLog -WriteToLogStream
# Run the cmdlets of your script
Get-PnPTraceLog | Where-Object Message -like "*retry*"
```

Each retry adds an entry like `Retrying request https://contoso.sharepoint.com/... due to status code TooManyRequests`, followed by `Waiting 4 seconds before retrying`. For a wait of a minute or more, that number shows only the seconds beyond the whole minutes.

## Avoiding throttling

Fewer requests mean less throttling. Use [batching](batching.md) where a cmdlet supports it, ask only for the properties and items you need, and spread large jobs over time. Microsoft's guidance covers the limits and patterns in detail:

- [Avoid getting throttled or blocked in SharePoint Online](https://learn.microsoft.com/sharepoint/dev/general-development/how-to-avoid-getting-throttled-or-blocked-in-sharepoint-online)
- [Microsoft Graph throttling guidance](https://learn.microsoft.com/graph/throttling)
