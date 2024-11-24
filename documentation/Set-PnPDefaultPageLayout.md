---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPDefaultPageLayout.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPDefaultPageLayout
---

# Set-PnPDefaultPageLayout

## SYNOPSIS

Sets a specific page layout to be the default page layout for a publishing site

## SYNTAX

### TITLE

```
Set-PnPDefaultPageLayout -Title <String> [-Connection <PnPConnection>]
```

### INHERIT

```
Set-PnPDefaultPageLayout [-InheritFromParentSite] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to set the default page layout for a publishing site.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPDefaultPageLayout -Title projectpage.aspx
```

Sets projectpage.aspx to be the default page layout for the current web

### EXAMPLE 2

```powershell
Set-PnPDefaultPageLayout -Title test/testpage.aspx
```

Sets a page layout in a folder in the Master Page & Page Layout gallery, such as _catalog/masterpage/test/testpage.aspx, to be the default page layout for the current web

### EXAMPLE 3

```powershell
Set-PnPDefaultPageLayout -InheritFromParentSite
```

Sets the default page layout to be inherited from the parent site

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

### -InheritFromParentSite

Set the default page layout to be inherited from the parent site.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: INHERIT
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Title

Title of the page layout

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: TITLE
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
