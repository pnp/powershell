# Environment variables

PnP PowerShell supports a few environment variables you can set to control some of its behaviour. Besides environment variables you can set, PnP PowerShell will also set a few environments for you to use.

## Environment variables you can set

| Environment variable | Description|
| ---------------------------|--------------------------|
| MicrosoftGraphEndPoint | The Microsoft Graph endpoint to use with `Connect-PnPOnline -AzureEnvironment Custom`, as a host name such as `custom.graph.microsoft.com`. It is ignored for the other values of `-AzureEnvironment`. See [National and sovereign clouds](nationalclouds.md#custom-endpoints) |
| AzureADLoginEndPoint | The Microsoft Entra ID sign in endpoint to use with `Connect-PnPOnline -AzureEnvironment Custom`, as a URL such as `https://custom.login.microsoftonline.com`. It is ignored for the other values of `-AzureEnvironment` |
| PNPPOWERSHELL_FEDERATEDIDENTITY_AUDIENCE | The audience `Connect-PnPOnline -FederatedIdentity` requests its GitHub Actions token for, instead of the one that follows from `-AzureEnvironment`. See [National and sovereign clouds](nationalclouds.md#sign-in-methods) |
| SharePointPnPHttpTimeout | The timeout in seconds for requests to SharePoint Online and Microsoft Graph, retries included, or `-1` for no timeout. Defaults to 100 seconds. Set it before connecting. It does not apply to cmdlets built on the PnP Core SDK. See [Throttling and retries](throttling.md#the-request-timeout) |
| SharePointPnPUserAgent | The `User-Agent` sent with the SharePoint REST API and Microsoft Graph requests PnP PowerShell makes through PnP Framework. Set it before the first connection in the PowerShell session. See [Throttling and retries](throttling.md#identifying-your-traffic) for the format |
| ENTRAID_APP_ID | When set [`Connect-PnPOnline`](../cmdlets/connect-pnponline.md) will use this value for authentication. See more info at [Set a default Client ID](defaultclientid.md) |
| ENTRAID_CLIENT_ID | See ENTRAID_APP_ID |
| AZURE_USERNAME | A way to set the username to use when authenticating with `Connect-PnPOnline -EnvironmentVariable` |
| AZURE_PASSWORD | A way to set the password to use when authenticating with `Connect-PnPOnline -EnvironmentVariable` |
| AZURE_CLIENT_ID | A way to set the application registration id/client id to use when authenticating with `Connect-PnPOnline -EnvironmentVariable` |
| AZURE_CLIENT_CERTIFICATE_PATH | Allows you to set the path to the certificate to use to authenticate with `Connect-PnPOnline -EnvironmentVariable` |
| AZURE_CLIENT_CERTIFICATE_PASSWORD | Allows you to set the password to access the certificate to use to authenticate with `Connect-PnPOnline -EnvironmentVariable` |
| PNPPOWERSHELL_DISABLETELEMETRY| Set to 'false' (lowercase) to disable telemetry |
| PNPPSCOMPLETERTIMEOUT | Defines the timeout to use when using << tab >> completion with PnP PowerShell (available in version 2.99.45 and higher). Tab completion defaults to 2 seconds timeout. The environment variable expects a value in milliseconds. E.g. 1000 equals 1 second. Set the value to 0 to disable tab completion.

## Environment variables set for you

| Environment variable | Description|
| ---------------------------|--------------------------|
| PNPPSHOST | The fully qualified hostname of the tenant you are connected to, e.g. `yourtenant.sharepoint.com` |
| PNPPSSITE | The server relative path to the site you are connected to, e.g. `/sites/yoursite` |
