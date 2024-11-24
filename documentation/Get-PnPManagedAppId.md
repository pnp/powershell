---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPManagedAppId.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPManagedAppId
---

# Get-PnPManagedAppId

## SYNOPSIS

Retrieve an App Id associated with a Url from either the Windows Credential Manager, the MacOS Key chain or if you use the Microsoft.PowerShell.SecretManagement module, a default vault.

## SYNTAX

### Default (Default)

```
Get-PnPManagedAppId -Url <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns an associated App Id from the Windows Credential Manager or Mac OS Key Chain Entry.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPManagedAppId -Url https://yourtenant.sharepoint.com
```

Returns the App Id associated with the specified tenant Url.

## PARAMETERS

### -Url

The Url for which to retrieve the associated App Id

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
