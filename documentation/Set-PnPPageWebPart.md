---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPPageWebPart.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPPageWebPart
---

# Set-PnPPageWebPart

## SYNOPSIS

Sets web part properties.

## SYNTAX

### Default (Default)

```
Set-PnPPageWebPart -Page <PagePipeBind> -Identity <WebPartPipeBind> [-Title <String>]
 [-PropertiesJson <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets specific client side web part properties. Notice that the title parameter will only set the -internal- title of web part. The title which is shown in the UI will, if possible, have to be set using the PropertiesJson parameter. Use Get-PnPPageComponent to retrieve the instance id and properties of a web part.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPPageWebPart -Page Home -Identity a2875399-d6ff-43a0-96da-be6ae5875f82 -PropertiesJson "`"Property1`"=`"Value1`""
```

Sets the properties of the client side web part.

### EXAMPLE 2

```powershell
Set-PnPPageWebPart -Page Home -Identity a2875399-d6ff-43a0-96da-be6ae5875f82 -PropertiesJson $myproperties
```

Sets the properties of the client side web part given in the $myproperties variable.

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

The identity of the web part. This can be the web part instance id or the title of a web part.

```yaml
Type: WebPartPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Page

The name of the page.

```yaml
Type: PagePipeBind
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

### -PropertiesJson

Sets the properties as a JSON string.

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Title

Sets the internal title of the web part. Notice that this will NOT set a visible title.

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
  ValueFromPipeline: true
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
