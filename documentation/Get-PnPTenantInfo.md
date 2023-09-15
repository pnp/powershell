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

## DESCRIPTION

Gets information about any tenant. If no Domain name or Tenant id is specified, it returns an error message to specify atleast one of TenantId or DomainName

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPTenantInfo -TenantId "e65b162c-6f87-4eb1-a24e-1b37d3504663"

tenantId                             federationBrandName displayName defaultDomainName
--------                             ------------------- ----------- -----------------
e65b162c-6f87-4eb1-a24e-1b37d3504663                     contoso      contoso.onmicrosoft.com
```

Returns the tenant information of the specified TenantId as shown. A valid connection with Connect-PnPOnline is required either 


### EXAMPLE 2
```powershell
Get-PnPTenantInfo -DomainName "contoso.com"

tenantId                             federationBrandName displayName defaultDomainName
--------                             ------------------- ----------- -----------------
e65b162c-6f87-4eb1-a24e-1b37d3504663                     contoso      contoso.onmicrosoft.com
```

Returns the Tenant Information for the tenant contoso.sharepoint.com as shown. A valid connection with Connect-PnPOnline is required either 


### EXAMPLE 3
```powershell
Get-PnPTenantInfo -DomainName "contoso.com" -TenantId "e65b162c-6f87-4eb1-a24e-1b37d3504663"

Error:
Get-PnPTenantInfo: Specify atleast one, either DomainName or TenantId, but not both
```

Returns error message as shown

### EXAMPLE 4
```powershell
Get-PnPTenantInfo

Error:
Get-PnPTenantInfo: Please specify either DomainName or TenantId, but not both
```

Returns error message as shown

## PARAMETERS

### -TenantId
The id of the tenant to retrieve the information. You can use either TenantId or DomainName to fetch the Tenant information of any tenant. 

```yaml
Type: String
Parameter Sets: (All)

Required: false
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DomainName
The Domain name of the tenant to lookup. You can use either TenantId or DomainName to fetch the Tenant information of any tenant. Use the full domain name as "contoso.onmicrosoft.com"

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: Production
Accept pipeline input: False
Accept wildcard characters: False
```


## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)