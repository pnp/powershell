---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Register-PnPManagementShellAccess.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Register-PnPManagementShellAccess
---

# Register-PnPManagementShellAccess

## SYNOPSIS

Registers access to the tenant for the PnP Management Shell Multi-Tenant Azure AD Application.

## SYNTAX

### Main

```
Register-PnPManagementShellAccess [-AzureEnvironment <AzureEnvironment>]
```

### Show Consent Url

```
Register-PnPManagementShellAccess -ShowConsentUrl [-AzureEnvironment]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet grants access to the tenant for the PnP Management Shell Multi-Tenant Azure AD Application which simplifies the use of OAuth based access for PnP PowerShell when using credentials to authenticate. If you are not an administrator that can consent Azure AD Applications, use the -ShowConsentUrl option. It will ask you to log in and provides you with an URL you can share with a person with appropriate access rights to provide consent for the organization.

## EXAMPLES

### EXAMPLE 1

```powershell
Register-PnPManagementShellAccess
```

Grants access to the tenant for the PnP Management Shell Multi-Tenant Azure AD Application.

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

### -AzureEnvironment

The Azure environment to use for authentication. It defaults to 'Production' which is the main Azure environment.

```yaml
Type: AzureEnvironment
DefaultValue: Production
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
AcceptedValues:
- Production
- PPE
- China
- Germany
- USGovernment
HelpMessage: ''
```

### -ShowConsentUrl

If specified you will be asked to authenticate to acquire the tenant id. An url that can be used to provide consent will be returned.

```yaml
Type: SwitchParameter
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Show Consent Url
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TenantName

The name of the tenant. Example - ( yourtenant.onmicrosoft.com)

```yaml
Type: SwitchParameter
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Show Consent Url
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
