---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Export-PnPUserInfo.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Export-PnPUserInfo
---

# Export-PnPUserInfo

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Export user information from site user information list.

## SYNTAX

### Default (Default)

```
Export-PnPUserInfo -LoginName <String> [-Site <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Export user information from the site user information list. If the Site parameter has not been specified, the current connect to site will be used.

## EXAMPLES

### EXAMPLE 1

```powershell
Export-PnPUserInfo -LoginName user@domain.com -Site "https://yoursite.sharepoint.com/sites/team"
```

This exports user data with the email address user@domain.com from the site collection specified.

### EXAMPLE 2

```powershell
Export-PnPUserInfo -LoginName user@domain.com -Site "https://yoursite.sharepoint.com/sites/team" | ConvertTo-Csv | Out-File MyFile.csv
```

This exports user data with the email address user@domain.com from the site collection specified, converts it to a CSV format and writes the result to the file MyFile.csv.

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
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Site

Specifies the URL of the site collection to which you want to export the user.

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
