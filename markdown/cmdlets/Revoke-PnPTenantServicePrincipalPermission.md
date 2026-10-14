---
Module Name: PnP.PowerShell
title: Revoke-PnPTenantServicePrincipalPermission
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Revoke-PnPTenantServicePrincipalPermission.html
---
 
# Revoke-PnPTenantServicePrincipalPermission

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site
* Microsoft Graph API : Directory.ReadWrite.All

Revokes a permission that was previously granted to the "SharePoint Online Client Extensibility Web Application Service Principal" service principal.

## SYNTAX

```powershell
Revoke-PnPTenantServicePrincipalPermission -Scope <String> [-Resource <String>] [-Force] [-Connection <PnPConnection>]
 
```

## DESCRIPTION
Revokes a permission that was previously granted to the "SharePoint Online Client Extensibility Web Application Service Principal" service principal.

## EXAMPLES

### EXAMPLE 1
```powershell
Revoke-PnPTenantServicePrincipalPermission -Scope "Group.Read.All"
```

Removes the Group.Read.All permission scope from the service principal.

## PARAMETERS

### -Scope
The scope to grant the permission for

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Resource
The resource to grant the permission for. Defaults to "Microsoft Graph"

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: Microsoft Graph
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Specifying the Force parameter will skip the confirmation question.

```yaml
Type: SwitchParameter
Parameter Sets: (All)

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

