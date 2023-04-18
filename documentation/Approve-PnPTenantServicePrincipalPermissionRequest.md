---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Approve-PnPTenantServicePrincipalPermissionRequest.html
external help file: PnP.PowerShell.dll-Help.xml
title: Approve-PnPTenantServicePrincipalPermissionRequest
---
  
# Approve-PnPTenantServicePrincipalPermissionRequest

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Approves a permission request for the current tenant's "SharePoint Online Client" service principal

## SYNTAX

```powershell
Approve-PnPTenantServicePrincipalPermissionRequest -RequestId <Guid> [-Force]
 [-Connection <PnPConnection>] 
```

## DESCRIPTION
Approves a permission request for the current tenant's "SharePoint Online Client" service principal

The return value of a successful call is a permission grant object.

To get the collection of permission grants for the "SharePoint Online Client" service principal, use the Get-PnPTenantServicePrincipalPermissionGrants command.

Approving a permission request also removes that request from the list of permission requests.

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

### -RequestId

```yaml
Type: Guid
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


