---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPListFoldersToSiteTemplate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPListFoldersToSiteTemplate
---

# Add-PnPListFoldersToSiteTemplate

## SYNOPSIS

Adds folders to a list in a PnP Provisioning Template

## SYNTAX

### Default (Default)

```
Add-PnPListFoldersToSiteTemplate [-Path] <String> [-List] <ListPipeBind>
 [[-TemplateProviderExtensions] <ITemplateProviderExtension[]>] [-Recursive] [-IncludeSecurity]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add folders to a list in a PnP Provisioning Template.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPListFoldersToSiteTemplate -Path template.pnp -List 'PnPTestList'
```

Adds top level folders from a list to an existing template and returns an in-memory PnP Site Template

### EXAMPLE 2

```powershell
Add-PnPListFoldersToSiteTemplate -Path template.pnp -List 'PnPTestList' -Recursive
```

Adds all folders from a list to an existing template and returns an in-memory PnP Site Template

### EXAMPLE 3

```powershell
Add-PnPListFoldersToSiteTemplate -Path template.pnp -List 'PnPTestList' -Recursive -IncludeSecurity
```

Adds all folders from a list with unique permissions to an in-memory PnP Site Template

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

### -IncludeSecurity

A switch to include ObjectSecurity information.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 5
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -List

The list to query

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 2
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Path

Filename of the .PNP Open XML site template to read from, optionally including full path.

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

### -Recursive

A switch parameter to include all folders in the list, or just top level folders.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- Recurse
ParameterSets:
- Name: (All)
  Position: 4
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TemplateProviderExtensions

Allows you to specify ITemplateProviderExtension to execute while loading the template.

```yaml
Type: ITemplateProviderExtension[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 6
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
