---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPWebTheme.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPWebTheme
---

# Set-PnPWebTheme

## SYNOPSIS

Sets the theme of the current web.

## SYNTAX

### Default (Default)

```
Set-PnPWebTheme [[-Theme] <ThemePipeBind>] [-WebUrl <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets the theme of the current web. * Requires SharePoint Online Administrator Rights *

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPWebTheme -Theme MyTheme
```

Sets the theme named "MyTheme" to the current web.

### EXAMPLE 2

```powershell
Get-PnPTenantTheme -Name "MyTheme" | Set-PnPWebTheme
```

Sets the theme named "MyTheme" to the current web.

### EXAMPLE 3

```powershell
Set-PnPWebTheme -Theme "MyCompanyTheme" -WebUrl https://contoso.sharepoint.com/sites/MyWeb
```

Sets the theme named "MyCompanyTheme" to MyWeb.

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

### -Theme

The name or ID of the theme that should be applied to the SharePoint site.

```yaml
Type: ThemePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -WebUrl

The URL of the web to apply the theme to. If not specified it will default to the current web based upon the URL specified with Connect-PnPOnline.

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
