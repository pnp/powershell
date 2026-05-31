# Environment variables

PnP PowerShell supports a few environment variables you can set to control some of its behaviour. Besides environment variables you can set, PnP PowerShell will also set a few environments for you to use.

## Environment variables you can set

| Environment variable | Description|
| ---------------------------|--------------------------|
| MicrosoftGraphEndPoint | Overrides the default Microsoft Graph endpoint (https://graph.microsoft.com) to use |
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
