---
Module Name: PnP.PowerShell
title: Get-PnPTenantServicePrincipalPermissionRequests
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantServicePrincipalPermissionRequests.html
---
 
# Get-PnPTenantServicePrincipalPermissionRequests

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets the collection of permission requests for the "SharePoint Online Client" service principal

## SYNTAX

```powershell
Get-PnPTenantServicePrincipalPermissionRequests [-Connection <PnPConnection>] 
```

## DESCRIPTION
Gets the collection of permission requests for the "SharePoint Online Client" service principal.

Permission request object

A permission request contains the following properties:

* Id: The identifier of the request.
* Resource: The resource that the application requires access to.
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

