# National and sovereign clouds

By default, PnP PowerShell signs in to and calls the worldwide Microsoft cloud. To work with a tenant in another cloud, connect to a site in that tenant and pass the cloud to [Connect-PnPOnline](../cmdlets/Connect-PnPOnline.md#-azureenvironment) with `-AzureEnvironment`:

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.us" -ClientId "<client id>" -Interactive -AzureEnvironment USGovernmentHigh
```

The application registration you connect with has to be registered in that same cloud. See [Register your application](registerapplication.md#special-instructions-for-gcc-or-national-cloud-environments) for how to create one with `Register-PnPEntraIDApp -AzureEnvironment`.

## The values of -AzureEnvironment

The value decides which Microsoft Entra ID endpoint PnP PowerShell signs in with and which Microsoft Graph endpoint it calls. The SharePoint endpoint follows from the URL you connect to.

| Value | Cloud | Sign in | Microsoft Graph |
|---|---|---|---|
| `Production` | Worldwide, the default | login.microsoftonline.com | graph.microsoft.com |
| `USGovernment` | US Government Community Cloud (GCC) | login.microsoftonline.com | graph.microsoft.com |
| `USGovernmentHigh` | US Government GCC High | login.microsoftonline.us | graph.microsoft.us |
| `USGovernmentDoD` | US Government DoD | login.microsoftonline.us | dod-graph.microsoft.us |
| `China` | Microsoft 365 operated by 21Vianet | login.chinacloudapi.cn | microsoftgraph.chinacloudapi.cn |
| `BleuCloud` | Bleu, France | login.sovcloud-identity.fr | graph.svc.sovcloud.fr |
| `DelosCloud` | Delos, Germany | login.sovcloud-identity.de | graph.svc.sovcloud.de |
| `GovSGCloud` | GovSG | login.sovcloud-identity.sg | graph.svc.sovcloud.sg |
| `Germany` | Microsoft Cloud Deutschland, closed on 29 October 2021 | login.microsoftonline.com | graph.microsoft.com |
| `PPE` | Microsoft's internal preproduction environment | login.windows-ppe.net, except for `-Interactive` and `-OSLogin` | graph.microsoft.com |
| `Custom` | Endpoints you provide | see [Custom endpoints](#custom-endpoints) | see [Custom endpoints](#custom-endpoints) |

A tenant in GCC uses the worldwide endpoints for signing in and for Microsoft Graph, but `USGovernment` also points the cmdlets that call Power Platform, Azure Resource Manager and Office 365 Management APIs to their US Government endpoints. `Get-PnPUnifiedAuditLog` calls the Office 365 Management API for GCC, GCC High and DoD at its endpoint in that cloud, and at the worldwide endpoint for the other values. With `BleuCloud`, `DelosCloud` and `GovSGCloud`, the cmdlets that call Power Platform APIs are not supported.

`Germany` is still accepted, but uses the worldwide endpoints, as the cloud it stood for has closed.

## Custom endpoints

For a cloud not in the list, use `-AzureEnvironment Custom` and provide the endpoints yourself. Provide the Microsoft Graph endpoint as a host name and the sign in endpoint as a URL:

```powershell
Connect-PnPOnline -Url "https://contoso.sharepoint.com" -ClientId "<client id>" -DeviceLogin -AzureEnvironment Custom -MicrosoftGraphEndPoint "custom.graph.microsoft.com" -AzureADLoginEndPoint "https://custom.login.microsoftonline.com"
```

Instead of the parameters, you can set the `MicrosoftGraphEndPoint` and `AzureADLoginEndPoint` environment variables. Like the parameters, they are only used with `-AzureEnvironment Custom`, and an endpoint you do not provide falls back to the worldwide one. The custom sign in endpoint is used when signing in with a certificate, with credentials, with `-DeviceLogin` or with `-FederatedIdentity`; `-Interactive` and `-OSLogin` sign in through the worldwide endpoint, and `-AzureADWorkloadIdentity` through the endpoint in `AZURE_AUTHORITY_HOST`.

## Sign in methods

`-AzureEnvironment` applies to every way of signing in with `Connect-PnPOnline`, with these differences:

- **`-AccessToken`** sends the token as it is given to every API, so the token has to be issued in the cloud given with `-AzureEnvironment`.
- **`-AzureADWorkloadIdentity`** signs in through the endpoint in the `AZURE_AUTHORITY_HOST` environment variable, which the workload identity webhook sets for the cloud of the cluster. `-AzureEnvironment` decides the endpoints of Microsoft Graph and the other APIs.
- **`-FederatedIdentity`** on GitHub Actions requests its token for the audience `api://AzureADTokenExchangeUSGov` with `USGovernmentHigh` and `USGovernmentDoD`, `api://AzureADTokenExchangeChina` with `China`, and `api://AzureADTokenExchange` otherwise. The federated credential on your application registration has to use the same audience. To use another one, set it in the `PNPPOWERSHELL_FEDERATEDIDENTITY_AUDIENCE` environment variable.

## Cmdlets that only work in the worldwide cloud

The following cmdlets call APIs through their worldwide endpoints, whatever the value of `-AzureEnvironment`:

- `Get-PnPPlannerConfiguration`, `Set-PnPPlannerConfiguration`, `Get-PnPPlannerUserPolicy` and `Set-PnPPlannerUserPolicy`
- `Get-PnPSearchVertical`, `New-PnPSearchVertical`, `Set-PnPSearchVertical`, `Remove-PnPSearchVertical`, `Set-PnPSearchVerticalOrder`, `Get-PnPSearchResultType`, `New-PnPSearchResultType`, `Set-PnPSearchResultType`, `Remove-PnPSearchResultType` and `Get-PnPSearchSiteConnection`

## Other cmdlets that take -AzureEnvironment

[Register-PnPEntraIDApp](../cmdlets/Register-PnPEntraIDApp.md), [Register-PnPEntraIDAppForInteractiveLogin](../cmdlets/Register-PnPEntraIDAppForInteractiveLogin.md) and [Get-PnPTenantId](../cmdlets/Get-PnPTenantId.md) take the same values, to work with a tenant in another cloud without connecting first.

For the endpoints of each cloud, see [Microsoft Graph national cloud deployments](https://learn.microsoft.com/graph/deployments) and [National clouds](https://learn.microsoft.com/entra/identity-platform/authentication-national-cloud).
