---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADApp.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPAzureADApp
---

# Remove-PnPAzureADApp

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.ReadWrite.All

Removes an Azure AD App registration.

## SYNTAX

### Default (Default)

```
Remove-PnPAzureADApp [-Identity] <AzureADAppPipeBind> [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet removes an Azure AD App registration.

## EXAMPLES

### Example 1

```powershell
Remove-PnPAzureADApp -Identity MyApp
```

Removes the specified app.

### Example 2

```powershell
Remove-PnPAzureADApp -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

Removes the specified app.

## PARAMETERS

### -Force

If specified the confirmation question will be skipped.

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

### -Identity

Specify the name, id or app id for the app to remove.

```yaml
Type: AzureADAppPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
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
