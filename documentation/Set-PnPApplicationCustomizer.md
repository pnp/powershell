---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPApplicationCustomizer.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPApplicationCustomizer
---

# Set-PnPApplicationCustomizer

## SYNOPSIS

Updates a SharePoint Framework client side extension application customizer

## SYNTAX

### Custom Action Id

```
Set-PnPApplicationCustomizer [[-Identity] <UserCustomActionPipeBind>] [-Scope <CustomActionScope>]
 [-Title <String>] [-Description <String>] [-Sequence <Int32>]
 [-ClientSideComponentProperties <String>] [-ClientSideHostProperties> <String>]
 [-Connection <PnPConnection>]
```

### Client Side Component Id

```
Set-PnPApplicationCustomizer [-ClientSideComponentId <Guid>] [-Scope <CustomActionScope>]
 [-Title <String>] [-Description <String>] [-Sequence <Int32>]
 [-ClientSideComponentProperties <String>] [-ClientSideHostProperties> <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Updates a SharePoint Framework client side extension application customizer by updating its custom action. Only the properties that will be provided will be updated. Others will remain as they are.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPApplicationCustomizer -Identity aa66f67e-46c0-4474-8a82-42bf467d07f2
```

Updates the custom action representing the client side extension registration with the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2'.

### EXAMPLE 2

```powershell
Set-PnPApplicationCustomizer -ClientSideComponentId aa66f67e-46c0-4474-8a82-42bf467d07f2 -Scope web -ClientSideComponentProperties "{`"sourceTermSet`":`"PnP-CollabFooter-SharedLinks`",`"personalItemsStorageProperty`":`"PnP-CollabFooter-MyLinks`"}"
```

Updates the custom action(s) properties being registered for a SharePoint Framework solution having the id 'aa66f67e-46c0-4474-8a82-42bf467d07f2' in its manifest from the current web.

## PARAMETERS

### -ClientSideComponentId

The Client Side Component Id of the SharePoint Framework client side extension application customizer found in the manifest for which existing custom action(s) should be updated

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Client Side Component Id
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ClientSideComponentProperties

The Client Side Component Properties of the application customizer to update. Specify values as a json string : "{Property1 : 'Value1', Property2: 'Value2'}". Omit to not update this property.

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

### -ClientSideHostProperties

The Client Side Host Properties of the application customizer to update. Omit to not update this property.

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

### -Description

The description of the application customizer. Omit to not update this property.

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

### -Identity

The id or name of the CustomAction representing the client side extension registration that needs to be updated or a CustomAction instance itself

```yaml
Type: UserCustomActionPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Custom Action Id
  Position: 0
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Scope

Define if the CustomAction representing the client side extension registration is to be found at the web or site collection scope. Specify All to update the component on both web and site collection level.

```yaml
Type: CustomActionScope
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
- Web
- Site
- All
HelpMessage: ''
```

### -Sequence

Sequence of this application customizer being injected. Use when you have a specific sequence with which to have multiple application customizers being added to the page. Omit to not update this property.

```yaml
Type: Int32
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

### -Title

The title of the application customizer. Omit to not update this property.

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
