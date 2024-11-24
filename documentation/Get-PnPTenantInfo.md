---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantInfo.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantInfo
---

# Get-PnPTenantInfo

## SYNOPSIS

Gets information about any tenant

** Required Permissions **
Graph: CrossTenantInformation.ReadBasic.All

## SYNTAX

### Current Tenant (default) (Default)

```
Get-PnPTenantInfo [-Verbose] [<CommonParameters>]
```

### By TenantId

```
Get-PnPTenantInfo -TenantId <String> [-Verbose] [<CommonParameters>]
```

### By Domain Name

```
Get-PnPTenantInfo -DomainName <String> [-Verbose] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

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
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: GETINFOOFCURRENTTENANT
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -DomainName

The Domain name of the tenant to lookup. You can use the onmicrosoft.com domain name such as "contoso.onmicrosoft.com" or use any domain that is connected to the tenant, i.e. "contoso.com".

```yaml
Type: String
DefaultValue: Production
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: GETINFOBYTDOMAINNAME
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TenantId

The id of the tenant to retrieve the information about

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: GETINFOBYTENANTID
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
