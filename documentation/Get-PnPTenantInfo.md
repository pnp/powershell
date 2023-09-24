---
Module Name: PnP.PowerShell
title: Get-PnPTenantInfo
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantInfo.html
---
 
# Get-PnPTenantInfo

## SYNOPSIS
Gets information about any tenant

## SYNTAX

### By TenantId
```powershell
Get-PnPTenantInfo -TenantId <String>
```

### By DomainName
```powershell
Get-PnPTenantInfo -DomainName <String>
```

### Current Tenant
```powershell
Get-PnPTenantInfo
```

## DESCRIPTION

Gets information about any tenant. If no Domain name or Tenant id is specified, it returns the Tenant Info of the current tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantInfo -TenantId "e65b162c-6f87-4eb1-a24e-1b37d3504663"

TenantId                             FederationBrandName DisplayName DefaultDomainName
--------                             ------------------- ----------- -----------------
e65b162c-6f87-4eb1-a24e-1b37d3504663                     contoso      contoso.onmicrosoft.com
```

Returns the tenant information of the specified TenantId as shown. A valid connection with Connect-PnPOnline is required either 


### EXAMPLE 2
```powershell
Get-PnPTenantInfo -DomainName "contoso.com"

TenantId                             FederationBrandName DisplayName DefaultDomainName
--------                             ------------------- ----------- -----------------
e65b162c-6f87-4eb1-a24e-1b37d3504663                     contoso      contoso.onmicrosoft.com
```

Returns the Tenant Information for the tenant contoso.sharepoint.com as shown. A valid connection with Connect-PnPOnline is required either 



### EXAMPLE 3
```powershell
Get-PnPTenantInfo

TenantId                             FederationBrandName DisplayName DefaultDomainName
--------                             ------------------- ----------- -----------------
e65b162c-6f87-4eb1-a24e-1b37d3504663                     contoso      contoso.onmicrosoft.com
```

Returns Tenant Information of the current tenant.

### EXAMPLE 4
```powershell
Get-PnPTenantInfo -CurrentTenant

TenantId                             FederationBrandName DisplayName DefaultDomainName
--------                             ------------------- ----------- -----------------
e65b162c-6f87-4eb1-a24e-1b37d3504663                     contoso      contoso.onmicrosoft.com
```

Returns Tenant Information of the current tenant.

## PARAMETERS

### -TenantId
The id of the tenant to retrieve the information. You can use either TenantId or DomainName to fetch the Tenant information of any tenant. 

```yaml
Type: String
Parameter Sets: GETINFOBYTENANTID

Required: true
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainName
The Domain name of the tenant to lookup. You can use either TenantId or DomainName to fetch the Tenant information of any tenant. Use the full domain name as "contoso.onmicrosoft.com"

```yaml
Type: String
Parameter Sets: GETINFOBYTDOMAINNAME

Required: False
Position: Named
Default value: Production
Accept pipeline input: False
Accept wildcard characters: False
```


### -CurrentTenant
Gets the Tenant Information of the current tenant.

```yaml
Type: SwitchParameter
Parameter Sets: GETINFOOFCURRENTTENANT

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)