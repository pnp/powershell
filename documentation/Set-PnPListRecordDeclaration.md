---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPListRecordDeclaration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPListRecordDeclaration
---

# Set-PnPListRecordDeclaration

## SYNOPSIS

Updates record declaration settings of a list.

## SYNTAX

### Default (Default)

```
Set-PnPListRecordDeclaration -List <ListPipeBind>
 [-ManualRecordDeclaration <EcmListManualRecordDeclaration>] [-AutoRecordDeclaration <Boolean>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The RecordDeclaration parameter supports 3 values:

* AlwaysAllowManualDeclaration
* NeverAllowManualDeclaration
* UseSiteCollectionDefaults

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPListRecordDeclaration -List "Documents" -ManualRecordDeclaration NeverAllowManualDeclaration
```

Sets the manual record declaration to never allow.

### EXAMPLE 2

```powershell
Set-PnPListRecordDeclaration -List "Documents" -AutoRecordDeclaration $true
```

Turns on auto record declaration for the list.

## PARAMETERS

### -AutoRecordDeclaration

Turns on or off auto record declaration on the list.

```yaml
Type: Boolean
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

### -List

The list to set the manual record declaration settings for. Specify title, list id, or list object.

```yaml
Type: ListPipeBind
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

### -ManualRecordDeclaration

Defines the manual record declaration setting for the lists.

```yaml
Type: EcmListManualRecordDeclaration
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
AcceptedValues:
- UseSiteCollectionDefaults
- AlwaysAllowManualDeclaration
- NeverAllowManualDeclaration
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
