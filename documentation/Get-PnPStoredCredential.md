---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPStoredCredential.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPStoredCredential
---

# Get-PnPStoredCredential

## SYNOPSIS

Get a credential

## SYNTAX

### Default (Default)

```
Get-PnPStoredCredential -Name <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns a stored credential from the Windows Credential Manager or Mac OS Key Chain Entry.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPStoredCredential -Name O365
```

Returns the credential associated with the specified identifier

## PARAMETERS

### -Name

The credential to retrieve.

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
