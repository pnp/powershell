---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Resolve-PnPFolder.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Resolve-PnPFolder
---

# Resolve-PnPFolder

## SYNOPSIS

Returns a folder from a given site relative path, and will create it if it does not exist.

## SYNTAX

### Default (Default)

```
Resolve-PnPFolder [-SiteRelativePath] <String> [-Connection <PnPConnection>] [-Includes <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns a folder from a given site relative path, and will create it if it does not exist. If you do not want the folder to be created, for instance just to test if a folder exists, use Get-PnPFolder

## EXAMPLES

### EXAMPLE 1

```powershell
Resolve-PnPFolder -SiteRelativePath "demofolder/subfolder"
```

Creates a folder called subfolder in a folder called demofolder located in the root folder of the site. If the folder hierarchy does not exist, it will be created.

## PARAMETERS

### -Connection

Optional connection to be used by the cmdlet. Retrieve the value for this parameter by either specifying -ReturnConnection on Connect-PnPOnline or by executing Get-PnPConnection.

```yaml
Type: PnPConnection
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

### -Includes

Optionally allows properties to be retrieved for the folder which are not included in the response by default

```yaml
Type: String[]
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

### -SiteRelativePath

Site Relative Folder Path

```yaml
Type: String
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
- [Get-PnPFolder](https://github.com/OfficeDev/PnP-PowerShell/blob/master/Documentation/GetPnPFolder.md)
