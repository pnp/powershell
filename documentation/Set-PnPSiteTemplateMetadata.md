---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteTemplateMetadata.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSiteTemplateMetadata
---

# Set-PnPSiteTemplateMetadata

## SYNOPSIS

Sets metadata of a provisioning template.

## SYNTAX

### Default (Default)

```
Set-PnPSiteTemplateMetadata [-Path] <String> [-TemplateDisplayName <String>]
 [-TemplateImagePreviewUrl <String>] [-TemplateProperties <Hashtable>]
 [-TemplateProviderExtensions <ITemplateProviderExtension[]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to modify metadata of a provisioning template.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSiteTemplateMetadata -Path template.xml -TemplateDisplayName "DisplayNameValue"
```

Sets the DisplayName property of a site template in XML format.

### EXAMPLE 2

```powershell
Set-PnPSiteTemplateMetadata -Path template.pnp -TemplateDisplayName "DisplayNameValue"
```

Sets the DisplayName property of a site template in Office Open XML format.

### EXAMPLE 3

```powershell
Set-PnPSiteTemplateMetadata -Path template.xml -TemplateImagePreviewUrl "Full URL of the Image Preview"
```

This example sets the image preview URL for a SharePoint site template stored in the file template.xml in Office Open XML format.

### EXAMPLE 4

```powershell
Set-PnPSiteTemplateMetadata -Path template.pnp -TemplateImagePreviewUrl "Full URL of the Image Preview"
```

This example sets the image preview URL for a SharePoint site template stored in the file template.pnp in Office Open XML format.

### EXAMPLE 5

```powershell
Set-PnPSiteTemplateMetadata -Path template.xml -TemplateProperties @{"Property1" = "Test Value 1"; "Property2"="Test Value 2"}
```

Sets the property 'Property1' to the value 'Test Value 1' of a site template in XML format.

### EXAMPLE 6

```powershell
Set-PnPSiteTemplateMetadata -Path template.pnp -TemplateProperties @{"Property1" = "Test Value 1"; "Property2"="Test Value 2"}
```

Sets the property 'Property1' to the value 'Test Value 1' of a site template in Office Open XML format.

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

### -Path

Path to the xml or pnp file containing the site template.

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
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -TemplateDisplayName

It can be used to specify the DisplayName of the template file that will be updated.

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

### -TemplateImagePreviewUrl

It can be used to specify the ImagePreviewUrl of the template file that will be updated.

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

### -TemplateProperties

It can be used to specify custom Properties for the template file that will be updated.

```yaml
Type: Hashtable
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

### -TemplateProviderExtensions

Allows you to specify ITemplateProviderExtension to execute while extracting a template.

```yaml
Type: ITemplateProviderExtension[]
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
