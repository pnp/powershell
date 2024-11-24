---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Invoke-PnPSiteDesign.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Invoke-PnPSiteDesign
---

# Invoke-PnPSiteDesign

## SYNOPSIS

Apply a Site Design to an existing site. * Requires Tenant Administration Rights *

## SYNTAX

### Default (Default)

```
Invoke-PnPSiteDesign [-Identity] <TenantSiteDesignPipeBind> [-WebUrl <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Applies the Site Design provided through Identity to an existing site. When providing a site design name and multiple site designs exist with the same name, all of them will be invoked.

## EXAMPLES

### EXAMPLE 1

```powershell
Invoke-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd
```

Applies the specified site design to the current site.

### EXAMPLE 2

```powershell
Invoke-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd -WebUrl "https://contoso.sharepoint.com/sites/mydemosite"
```

Applies the specified site design to the specified site.

### EXAMPLE 3

```powershell
Get-PnPSiteDesign | ?{$_.Title -eq "Demo"} | Invoke-PnPSiteDesign
```

Applies the specified site design to the specified site.

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

### -Identity

The Site Design Id or an actual Site Design object to apply

```yaml
Type: TenantSiteDesignPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
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

### -WebUrl

The URL of the web to apply the site design to. If not specified it will default to the current web based upon the URL specified with Connect-PnPOnline.

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
