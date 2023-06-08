---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Enable-PnPTenantServicePrincipal.html
external help file: PnP.PowerShell.dll-Help.xml
title: Enable-PnPTenantServicePrincipal
---
  
# Enable-PnPTenantServicePrincipal

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Enables the current tenant's "SharePoint Online Client" service principal.

## SYNTAX

```powershell
Enable-PnPTenantServicePrincipal [-Force] [-Connection <PnPConnection>] 
```

## DESCRIPTION
Enables the current tenant's "SharePoint Online Client" service principal.

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

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


