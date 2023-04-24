---
Module Name: PnP.PowerShell
title: Register-PnPManagementShellAccess
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Register-PnPManagementShellAccess.html
---
 
# Register-PnPManagementShellAccess

## SYNOPSIS
Registers access to the tenant for the PnP Management Shell Multi-Tenant Azure AD Application

## SYNTAX

### Main
```powershell
Register-PnPManagementShellAccess [-AzureEnvironment <AzureEnvironment>] 
```

### Show Consent Url
```powershell
Register-PnPManagementShellAccess -ShowConsentUrl [-AzureEnvironment]
```

## DESCRIPTION
This cmdlet grants access to the tenant for the PnP Management Shell Multi-Tenant Azure AD Application which simplifies the use of OAuth based access for PnP PowerShell when using credentials to authenticate. If you are not an administrator that can consent Azure AD Applications, use the -ShowConsentUrl option. It will ask you to log in and provides you with an URL you can share with a person with appropriate access rights to provide consent for the organization.

## EXAMPLES

### EXAMPLE 1
```powershell
Register-PnPManagementShellAccess
```

### EXAMPLE 2
```powershell
Register-PnPManagementShellAccess -ShowConsentUrl
```

Launches the consent flow to grant the PnP Management Shell Azure AD Application delegate access to the tenant and also displays the consent URL which can be shared with Azure AD administrators or Global administrators.

### EXAMPLE 3
```powershell
Register-PnPManagementShellAccess -ShowConsentUrl -TenantName yourtenant.onmicrosoft.com
```

Displays the consent URL which can be shared with Azure AD administrators or Global administrators.

## PARAMETERS

### -ShowConsentUrl
If specified you will be asked to authenticate to acquire the tenant id. An url that can be used to provide consent will be returned.

```yaml
Type: SwitchParameter
Parameter Sets: Show Consent Url

Required: False
Position: Named
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -TenantName
The name of the tenant. Example - ( yourtenant.onmicrosoft.com)

```yaml
Type: SwitchParameter
Parameter Sets: Show Consent Url

Required: False
Position: Named
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

