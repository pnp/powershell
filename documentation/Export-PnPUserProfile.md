---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Export-PnPUserProfile.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Export-PnPUserProfile
---

# Export-PnPUserProfile

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Export user profile data.

## SYNTAX

### Default (Default)

```
Export-PnPUserProfile -LoginName <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Export user profile data.

## EXAMPLES

### EXAMPLE 1

```powershell
Export-PnPUserProfile -LoginName user@domain.com
```

This exports user profile data with the email address user@domain.com.

### EXAMPLE 2

```powershell
Export-PnPUserProfile -LoginName user@domain.com | ConvertTo-Csv | Out-File MyFile.csv
```

This exports user profile data with the email address user@domain.com, converts it to a CSV format and writes the result to the file MyFile.csv.

## PARAMETERS

### -LoginName

Specifies the login name of the user to export.

```yaml
Type: String
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
