---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Test-PnPConnectionPermission.html
external help file: PnP.PowerShell.dll-Help.xml
title: Test-PnPConnectionPermission
---

# Test-PnPConnectionPermission

## SYNOPSIS
Tests whether the current connection has the API permissions required by a PnP PowerShell cmdlet.

## SYNTAX

```powershell
Test-PnPConnectionPermission [-CommandName] <String> [-Connection <PnPConnection>] [-Verbose]
```

## DESCRIPTION
Acquires the access tokens needed by the specified cmdlet and compares their `scp` or `roles` claims with the cmdlet's permission metadata as reported by [Get-PnPCommandPermission](Get-PnPCommandPermission.md). Returns `$true` when the connection holds one complete permission set and `$false` when it does not.

A required permission is satisfied by the very same scope, but also by any scope which is more privileged. `Sites.FullControl.All` therefore satisfies a required `Sites.Read.All`, and the delegated notation and the application notation of the same SharePoint level are treated as equivalent, so `AllSites.FullControl` satisfies a required `Sites.ReadWrite.All`. This matters because the permission metadata reports the least privileged scope which suffices rather than the exact scope to hold. `Sites.Selected` is deliberately excluded from this: it grants access only to the sites the application has explicitly been granted access to, so it neither covers nor is covered by the tenant wide scopes.

When permissions are missing the cmdlet returns `$false` and writes a non-terminating `RequiredPermissionMissing` error which names each missing permission and preserves the `AND` and `OR` relationship between permission sets. Because the error is non-terminating, the cmdlet fits all three common usages:

| Usage | Behaviour |
|---|---|
| `if (Test-PnPConnectionPermission ...)` | Branch on the result, with the missing permissions reported on the error stream |
| `-ErrorAction SilentlyContinue` | Return the boolean without writing the error |
| `-ErrorAction Stop` | Terminate the script, for use as a preflight check in unattended runs |

Where the requirement cannot be established, the cmdlet writes a non-terminating `PermissionRequirementsIndeterminate` error and returns nothing at all rather than `$false`. Reporting `$false` would state that the connection does not hold the permissions, while in these cases the check could not be performed. This covers cmdlets whose permissions depend on the resource they are pointed at or on how they are invoked, cmdlets for which no permissions are declared for the token type in use, and resources for which no access token could be acquired.

The check validates API permissions only. SharePoint permission levels, `Sites.Selected` site grants and the additional roles reported by `Get-PnPCommandPermission` cannot be inferred from an access token and are not tested. A `$true` result therefore confirms the token claims, not every authorization requirement of the target resource.

Validation is supported for Microsoft Graph, SharePoint Online, Azure Resource Manager, Power Apps and Graph Connector Service access tokens. The Dynamics CRM audience used by `Get-PnPPowerPlatformSolution` depends on the selected environment and cannot be determined from a cmdlet name, so that permission set is reported as indeterminate. Connections made with an ACS app only token are also reported as indeterminate, as such a token cannot be exchanged for a token carrying permission scopes.

A connection made with `Connect-PnPOnline -AccessToken` holds one fixed token and returns it whatever resource is asked for. The audience of every token is therefore verified before its scopes are used, and a token issued for another API is reported as indeterminate rather than compared against the requirement. In practice this means such a connection can only be tested for cmdlets which use the API the token was issued for.

Testing a SharePoint permission requires a connection created with a SharePoint site URL, because that URL determines the access token audience.

Permissions with a source of `Inferred` or `DeclaredAndInferred` remain estimates. This cmdlet tests the estimate returned by `Get-PnPCommandPermission`; it cannot make inferred metadata authoritative.

## EXAMPLES

### EXAMPLE 1
```powershell
Test-PnPConnectionPermission -CommandName Get-PnPTeamsTeam
```

Returns `$true` if the current connection contains one complete delegated or application permission set required by `Get-PnPTeamsTeam`. Otherwise, it returns `$false` and reports the missing permissions.

### EXAMPLE 2
```powershell
if (-not (Test-PnPConnectionPermission -CommandName Set-PnPList -ErrorAction SilentlyContinue)) {
    Write-Host "Skipping the list configuration step, the connection is not permitted to change lists."
}
```

Branches on the result without writing the missing permissions to the error stream.

### EXAMPLE 3
```powershell
'Get-PnPTeamsTeam', 'Set-PnPList', 'Get-PnPList' | Test-PnPConnectionPermission
```

Tests a sequence of cmdlets and returns one boolean per cmdlet. Access tokens are acquired once and reused across all of them. Processing continues after a cmdlet for which a permission is missing.

### EXAMPLE 4
```powershell
$connection = Connect-PnPOnline -Url https://contoso.sharepoint.com/sites/project -Interactive -ReturnConnection
Test-PnPConnectionPermission -CommandName Set-PnPList -Connection $connection -ErrorAction Stop
```

Tests the supplied connection and terminates the script when a required permission is missing, for use as a preflight check in an unattended run.

## PARAMETERS

### -CommandName
The name or alias of the PnP PowerShell cmdlet whose required permissions should be tested. Tab completion is available for PnP cmdlet names.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Identity, Name

Required: True
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -Connection
Optional connection to be tested. Retrieve a connection by specifying `-ReturnConnection` on `Connect-PnPOnline` or by executing `Get-PnPConnection`.

```yaml
Type: PnPConnection
Parameter Sets: (All)
Aliases: None

Required: False
Position: Named
Default value: Current connection
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### System.Boolean
`$true` when one complete required permission set is present, `$false` when a required permission is missing. Nothing is returned where the requirement could not be established; use `-Verbose` to see the scopes read from each access token.

## RELATED LINKS

[Get-PnPCommandPermission](https://pnp.github.io/powershell/cmdlets/Get-PnPCommandPermission.html)

[How to determine which permissions you need](https://pnp.github.io/powershell/articles/determinepermissions.html)

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
