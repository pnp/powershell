---
external help file: PnP.PowerShell.dll-Help.xml
applicable: SharePoint Online
title: Get-PnPCommandPermission
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPCommandPermission.html
tags: Available in the current Nightly Release only.
schema: 2.0.0
Module Name: PnP.PowerShell
---
   
# Get-PnPCommandPermission

## SYNOPSIS
Returns the API permissions and roles required to run a PnP PowerShell cmdlet.

## SYNTAX

```powershell
Get-PnPCommandPermission [[-CommandName] <String>] [-ResourceTypeName <ResourceTypeName>]
 [-Source <CommandPermissionSource>] [-Verbose]
```

## DESCRIPTION
Returns the delegated and application API permission sets required to run a PnP PowerShell cmdlet, together with the minimum SharePoint rights the calling user needs to hold. Permissions within one set are all required, while multiple sets are alternatives to each other. The alternatives are listed in the order they are declared on the cmdlet, which by convention starts with the least privileged one.

Use this cmdlet to determine up front which permissions to grant to an Entra ID app registration, rather than discovering it from a failed call.

The `PermissionSource` property states how reliable the returned information is:

| PermissionSource | Meaning |
|------------------|---------|
| `Declared` | The permissions are declared on the cmdlet through its permission attributes and are authoritative. Check `Guidance`, as a cmdlet can additionally require permissions that depend on how it is invoked. |
| `DeclaredAndInferred` | Part declared, part derived, in one of two ways. Either the cmdlet uses SharePoint CSOM **next to** the API it declares permissions for, i.e. `Set-PnPList`, in which case the derived SharePoint permission is added to each declared alternative and is required on top of them. Or the cmdlet reaches its goal **either** through the declared API **or** through SharePoint, i.e. `Send-PnPMail`, in which case the derived SharePoint permission is listed as a separate alternative. Read `Guidance` to see which of the two applies before granting anything. |
| `Inferred` | The permissions have been derived from the type of cmdlet and the operation it performs. They are a least privilege estimate and may need to be raised for specific operations. |
| `ResourceDependent` | The permissions follow from the resource the cmdlet is pointed at at runtime and cannot be stated up front, i.e. `New-PnPGraphSubscription` or `Invoke-PnPGraphMethod`. The `Guidance` property names the parameter involved and links to the relevant documentation. |
| `NotApplicable` | The cmdlet requires no permissions on the Entra ID application registration used to connect with PnP PowerShell. That covers cmdlets which perform no request at all, i.e. `Get-PnPContext`, cmdlets which call an API that needs no permissions, i.e. `Get-PnPChangeLog` which reads the release notes from GitHub, and cmdlets which authenticate separately and can still require rights of their own, i.e. `Register-PnPEntraIDApp`. `Guidance` states which applies. |
| `Unknown` | No permissions are declared on the cmdlet and they could not be derived. Consult the documentation of that cmdlet. |

Omitting `-CommandName` returns the permission information of every cmdlet in the module, which allows the full permission surface to be audited.

This cmdlet does not require a connection.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPCommandPermission -CommandName Get-PnPTeamsTeam
```

Returns the permissions declared for `Get-PnPTeamsTeam`.

### EXAMPLE 2
```powershell
Get-PnPCommandPermission -CommandName *TeamsChannel*
```

Returns the permissions of every cmdlet whose name matches the wildcard pattern.

### EXAMPLE 3
```powershell
Get-PnPCommandPermission | Group-Object PermissionSource
```

Returns how many cmdlets declare their permissions and how many have them inferred.

### EXAMPLE 4
```powershell
Get-PnPCommandPermission -ResourceTypeName Graph -Source Declared | Select-Object CommandName, ApplicationPermissions
```

Returns every cmdlet with authoritative Microsoft Graph permission metadata, which can be used to compose the permission set of an app registration.

### EXAMPLE 5
```powershell
Get-PnPCommandPermission -CommandName Set-PnPWeb | Select-Object -ExpandProperty MinimumSharePointRole
```

Returns the minimum SharePoint role required to run `Set-PnPWeb`.

## PARAMETERS

### -CommandName
The name of the PnP PowerShell cmdlet for which the permission information should be returned. Accepts an alias of a cmdlet as well as wildcards. If omitted, the permission information of all cmdlets is returned. Tab completion is available for PnP cmdlet names.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Identity, Name

Required: False
Position: 0
Default value: None
Accept pipeline input: True
Accept wildcard characters: True
```

### -ResourceTypeName
Only return cmdlets which require a permission on the specified resource, i.e. `Graph` or `SharePoint`.

```yaml
Type: ResourceTypeName
Parameter Sets: (All)
Accepted values: Unknown, Graph, SharePoint, AzureManagementApi, ExchangeOnline, PowerAutomate, PowerApps, DynamicsCRM, Gcs

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Source
Only return cmdlets whose permission information originates from the specified source.

```yaml
Type: CommandPermissionSource
Parameter Sets: (All)
Accepted values: Unknown, Declared, Inferred, NotApplicable, ResourceDependent, DeclaredAndInferred

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## OUTPUTS

### PnP.PowerShell.Commands.Model.CommandPermission

| Property | Description |
|----------|-------------|
| `CommandName` | Name of the cmdlet. |
| `Aliases` | Aliases under which the cmdlet can also be called. |
| `DelegatedPermissions` | Delegated permission sets. Permissions within a set are all required, sets are alternatives. |
| `ApplicationPermissions` | Application permission sets. Permissions within a set are all required, sets are alternatives. |
| `ResourceTypes` | The APIs this cmdlet requires permissions on. Also populated when the exact scopes depend on the resource, which is what `-ResourceTypeName` filters on. |
| `DelegatedAvailable` | Indicates if the cmdlet can run using a delegated access token. |
| `ApplicationAvailable` | Indicates if the cmdlet can run using an application access token. |
| `PermissionSource` | Where the permission information originates from. |
| `MinimumSharePointRole` | Minimum rights needed on the SharePoint site the cmdlet acts on. Applies when connecting delegated, and when connecting app only using `Sites.Selected`. A tenant wide application permission such as `Sites.ReadWrite.All` already covers every site. The values follow the [SharePoint permission levels](https://learn.microsoft.com/sharepoint/sites/user-permissions-and-permission-levels): `SiteVisitor` (Read), `SiteMember` (Contribute), `SiteEditor` (Edit, holds Manage Lists), `SiteDesigner` (Design, holds Add and Customize Pages and Apply Themes), `SiteOwner` (Full Control, holds Manage Permissions, Enumerate Permissions, Manage Web Site and Manage Alerts), `SiteCollectionAdministrator` and `SharePointAdministrator`. `NotApplicable` is also returned for operations not governed by a site permission level at all, such as term store operations. |
| `AdditionalRoles` | Roles which need to be held next to the API permissions. Each entry states in which connection scenario it applies. |
| `Guidance` | Remarks on how to interpret the returned permissions. |

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

[How to determine which permissions you need](https://pnp.github.io/powershell/articles/determinepermissions.html)

[Working with permission attributes](https://pnp.github.io/powershell/articles/permissionattributes.html)

