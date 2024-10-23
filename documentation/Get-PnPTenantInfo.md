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

## Required Permissions
Graph: CrossTenantInformation.ReadBasic.All

## SYNTAX

### Current Tenant (default)
```powershell
Get-PnPTenantInfo [-Verbose]
```

### By TenantId
```powershell
Get-PnPTenantInfo -TenantId <String> [-Verbose]
```

### By Domain Name
```powershell
Get-PnPTenantInfo -DomainName <String> [-Verbose]
```

## DESCRIPTION

Gets the tenantId, federation brand name, company name and default domain name regarding a specific tenant. If no Domain name or Tenant id is specified, it returns the Tenant Info of the currently connected to tenant.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantInfo -TenantId "e65b162c-6f87-4eb1-a24e-1b37d3504663"
```

Returns the tenant information of the specified TenantId.

### EXAMPLE 2
```powershell
Get-PnPTenantInfo -DomainName "contoso.com"
```

Returns the Tenant Information for the tenant connected to the domain contoso.com.

### EXAMPLE 3
```powershell
Get-PnPTenantInfo
```

Returns Tenant Information of the currently connected to tenant.

### EXAMPLE 4
```powershell
Get-PnPTenantInfo -CurrentTenant
```

Returns Tenant Information of the currently connected to tenant.

## PARAMETERS

### -CurrentTenant
Gets the Tenant Information of the currently connected to tenant.

```yaml
Type: SwitchParameter
Parameter Sets: GETINFOOFCURRENTTENANT

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainName
The Domain name of the tenant to lookup. You can use the onmicrosoft.com domain name such as "contoso.onmicrosoft.com" or use any domain that is connected to the tenant, i.e. "contoso.com".

```yaml
Type: String
Parameter Sets: GETINFOBYTDOMAINNAME

Required: False
Position: Named
Default value: Production
Accept pipeline input: False
Accept wildcard characters: False
```

### -TenantId
The id of the tenant to retrieve the information about

```yaml
Type: String
Parameter Sets: GETINFOBYTENANTID

Required: true
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Verbose
When provided, additional debug statements will be shown while executing the cmdlet.

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