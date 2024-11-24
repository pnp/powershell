---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPDocumentSetField.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPDocumentSetField
---

# Set-PnPDocumentSetField

## SYNOPSIS

Sets a site column from the available content types to a document set

## SYNTAX

### Default (Default)

```
Set-PnPDocumentSetField -DocumentSet <DocumentSetPipeBind> -Field <FieldPipeBind> [-SetSharedField]
 [-SetWelcomePageField] [-RemoveSharedField] [-RemoveWelcomePageField] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to set a site column from the available content types to a document set.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPDocumentSetField -Field "Test Field" -DocumentSet "Test Document Set" -SetSharedField -SetWelcomePageField
```

This will set the field, available in one of the available content types, as a Shared Field and as a Welcome Page Field.

### EXAMPLE 2

```powershell
Set-PnPDocumentSetField -Field "Test Field" -DocumentSet "Test Document Set" -RemoveSharedField -RemoveWelcomePageField
```

This will remove the field, available in one of the available content types, as a Shared Field and as a Welcome Page Field.

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

### -DocumentSet

The document set in which to set the field. Either specify a name, a document set template object, an id, or a content type object

```yaml
Type: DocumentSetPipeBind
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

### -Field

The field to set. The field needs to be available in one of the available content types. Either specify a name, an id or a field object

```yaml
Type: FieldPipeBind
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

### -RemoveSharedField

Removes the field as a Shared Field

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

### -RemoveWelcomePageField

Removes the field as a Welcome Page Field

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

### -SetSharedField

Set the field as a Shared Field

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

### -SetWelcomePageField

Set the field as a Welcome Page field

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
