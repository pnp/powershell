---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/register-pnpmanagementshellaccess
schema: 2.0.0
title: Register-PnPManagementShellAccess
---

# Register-PnPManagementShellAccess

## SYNOPSIS
Registers access to the tenant for the PnP Management Shell Multi-Tenant Azure AD Application

## SYNTAX

```powershell
Register-PnPManagementShellAccess [-AzureEnvironment <AzureEnvironment>] [<CommonParameters>]
```

## DESCRIPTION
This cmdlet grants access to the tenant for the PnP Management Shell Multi-Tenant Azure AD Application which simplifies the use of OAuth based access for PnP PowerShell when using credentials to authenticate.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPManagementShellAccess
```

Launches the consent flow to grant the PnP Management Shell Azure AD Application delegate access to the tenant.

## PARAMETERS

### -AzureEnvironment
The Azure environment to use for authentication, the defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
Parameter Sets: (All)
Accepted values: Production, PPE, China, Germany, USGovernment
Required: False
Position: Named
Default value: Production
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)