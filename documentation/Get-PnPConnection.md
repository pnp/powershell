---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPConnection.html
external help file: PnP.PowerShell.dll-Help.xml
title: Get-PnPConnection
---
  
# Get-PnPConnection

## SYNOPSIS
Returns the current connection

## SYNTAX

```powershell
Get-PnPConnection [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns a PnP PowerShell Connection for use with the -Connection parameter on other cmdlets.

The read-only `Authentication` property describes the connection's actual provider and the identity
reported by its latest successful MSAL token acquisition. Reading it does not acquire a token,
enumerate accounts, or make a network request. Reused providers retain their actual method even
if a different login method was requested.

| Property | Meaning |
| --- | --- |
| `Method` | Provider, such as `Browser`, `WAM`, `DeviceCode`, `UsernamePassword`, or `Certificate`. A cache hit does not change the method. |
| `IdentityType` | `User`, `Application`, or `Unknown`. |
| `AccountId` | MSAL home-account identifier, not the user's object ID in the target tenant. |
| `Username` | Display username returned by MSAL; do not use it as an immutable identity. |
| `HomeTenantId` | Tenant of the MSAL home account. |
| `AuthenticationTenantId` | Resolved tenant returned by MSAL, which can differ from the home tenant for a guest. |

Unavailable values remain null. Supplied access tokens are not decoded to infer identity. Workload
and federated connections report their provider and application identity type, but do not currently
expose resolved MSAL account or tenant details. These properties do not prove that a site exists,
that the identity is a tenant administrator, or that Graph or SharePoint permissions were granted.
Expiry, scopes, and renewal capability belong to individual resource tokens and are not reported here.

The default connection display expands selected authentication properties. It hides `AccountId` and
omits `HomeTenantId` when it equals `AuthenticationTenantId`. The full properties remain available
for scripts. It omits the legacy `ClientId` for managed identity, workload identity, supplied-token,
on-premises, and unknown providers, where that property can be a placeholder rather than a resolved
application ID. Existing connection properties, including `ClientId`, `Tenant`, `ConnectionMethod`,
and `AzureEnvironment`, retain their existing behavior.

## EXAMPLES

### EXAMPLE 1
```powershell
$ctx = Get-PnPConnection
```

This will put the current connection for use with the -Connection parameter on other cmdlets.

### EXAMPLE 2
```powershell
$connection = Connect-PnPOnline -Url https://contoso.sharepoint.com/sites/project -Interactive -ReturnConnection
$connection.Authentication | Format-List *
$connection.Authentication.AccountId
```

Inspects the provider and available identity metadata on an explicitly returned connection without
making it the default connection or acquiring another token for inspection.

## PARAMETERS

### -Connection
Optional connection to be used by the cmdlet. Retrieve the value for this parameter by specifying -ReturnConnection on Connect-PnPOnline. If not provided, the connection will be retrieved from the current context.

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
