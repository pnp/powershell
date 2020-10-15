---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnptenantserviceprincipalpermissiongrants
schema: 2.0.0
title: Get-PnPTenantServicePrincipalPermissionGrants
---

# Get-PnPTenantServicePrincipalPermissionGrants

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets the collection of permission grants for the "SharePoint Online Client" service principal

## SYNTAX

```powershell
Get-PnPTenantServicePrincipalPermissionGrants [-Connection <PnPConnection>] [<CommonParameters>]
```

## DESCRIPTION
Gets the collection of permission grants for the "SharePoint Online Client" service principal.

A permission grant contains the following properties:

* ClientId: The objectId of the service principal granted consent to impersonate the user when accessing the resource(represented by the resourceId).
* ConsentType: Whether consent was provided by the administrator on behalf of the organization or whether consent was provided by an individual.The possible values are "AllPrincipals" or "Principal".
* ObjectId: The unique identifier for the permission grant.
* Resource: The resource to which access has been granted (Coming soon)
* ResourceId: The objectId of the resource service principal to which access has been granted.
* Scope: The value of the scope claim that the resource application should expect in the OAuth 2.0 access token.

## EXAMPLES

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)