---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPHomePage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPHomePage
---

# Set-PnPHomePage

## SYNOPSIS

Sets the home page of the current web.

## SYNTAX

### Default (Default)

```
Set-PnPHomePage [-RootFolderRelativeUrl] <String> [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to set the home page of the current site.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPHomePage -RootFolderRelativeUrl SitePages/Home.aspx
```

Sets the home page to the home.aspx file which resides in the SitePages library.

### EXAMPLE 2

```powershell
Set-PnPHomePage -RootFolderRelativeUrl Lists/Sample/AllItems.aspx
```

Sets the home page to be the Sample list.

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

### -RootFolderRelativeUrl

The root folder relative url of the homepage, e.g. 'sitepages/home.aspx'. Notice that the url is relative to the root folder of the web.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Path
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
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
