---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Export-PnPPage.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Export-PnPPage
---

# Export-PnPPage

## SYNOPSIS

Exports a Client Side Page to a PnP Provisioning Template

## SYNTAX

### Default (Default)

```
Export-PnPPage [-Identity] <PagePipeBind> [-PersistBrandingFiles] [-Out <String>] [-Force]
 [-Configuration <ExtractConfigurationPipeBind>] [-OutputInstance] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to export a Client Side Page to a PnP Provisioning Template.

## EXAMPLES

### EXAMPLE 1

```powershell
Export-PnPPage -Identity Home.aspx
```

Exports the page 'Home.aspx' to a new PnP Provisioning Template

### EXAMPLE 2

```powershell
Export-PnPPage -Identity HR/Home.aspx -Out template.pnp
```

Exports the page 'Home.aspx' to a new PnP Provisioning Template

## PARAMETERS

### -Configuration

Specify a JSON configuration file to configure the extraction progress.

```yaml
Type: ExtractConfigurationPipeBind
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

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

### -Force

Specify to override the question to overwrite a file if it already exists.

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

### -Identity

The name/identity of the page. This can be a page instance or the filename of the page. I.e. if the page is called MyPage.aspx and is located in the root of the Site Pages library, provide "MyPage" or "MyPage.aspx". If the page is called MyOtherPage.aspx and is located inside a subfolder called HR located in the root of the Site Pages library, provide "HR/MyOtherPage" or "HR/MyOtherPage.aspx

```yaml
Type: ClientSidePagePipeBind
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

### -Out

If specified the template will be saved to the file specified with this parameter.

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

### -OutputInstance

Returns the template as an in-memory object, which is an instance of the SiteTemplate type of the PnP Core Component. It cannot be used together with the -Out parameter.

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

### -PersistBrandingFiles

If specified referenced files will be exported to the current folder.

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
