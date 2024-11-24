---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPTenantSyncClientRestriction.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPTenantSyncClientRestriction
---

# Set-PnPTenantSyncClientRestriction

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets organization-level sync client restriction properties

## SYNTAX

### Default (Default)

```
Set-PnPTenantSyncClientRestriction [-BlockMacSync] [-DisableReportProblemDialog]
 [-DomainGuids <System.Collections.Generic.List`1[System.Guid]>] [-Enable]
 [-ExcludedFileExtensions <System.Collections.Generic.List`1[System.String]>]
 [-GrooveBlockOption <GrooveBlockOption>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Sets organization-level sync client restriction properties such as BlockMacSync, OptOutOfGroveBlock, and DisableReportProblemDialog.

You must have the SharePoint Online admin or Global admin role to run the cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPTenantSyncClientRestriction -BlockMacSync:$false
```

This example blocks access to Mac sync clients for OneDrive file synchronization

### EXAMPLE 2

```powershell
Set-PnPTenantSyncClientRestriction  -ExcludedFileExtensions "pptx;docx;xlsx"
```

This example blocks syncing of PowerPoint, Word, and Excel file types using the new sync client (OneDrive.exe).

## PARAMETERS

### -BlockMacSync

Block Mac sync clients-- the Beta version and the new sync client (OneDrive.exe). The values for this parameter are $true and $false. The default value is $false.

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

### -DisableReportProblemDialog

Specifies if the Report Problem Dialog is disabled or not.

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

### -DomainGuids

Sets the domain GUID to add to the safe recipient list. Requires a minimum of 1 domain GUID. The maximum number of domain GUIDs allowed are 125. I.e. 634c71f6-fa83-429c-b77b-0dba3cb70b93,4fbc735f-0ac2-48ba-b035-b1ae3a480887.

```yaml
Type: System.Collections.Generic.List`1[System.Guid]
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

### -Enable

Enables the feature to block sync originating from domains that are not present in the safe recipients list.

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

### -ExcludedFileExtensions

Blocks certain file types from syncing with the new sync client (OneDrive.exe). Provide as one string separating the extensions using a semicolon (;). I.e. "docx;pptx"

```yaml
Type: System.Collections.Generic.List`1[System.String]
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

### -GrooveBlockOption

Controls whether or not a tenant's users can sync OneDrive for Business libraries with the old OneDrive for Business sync client. The valid values are OptOut, HardOptin, and SoftOptin. GrooveBlockOption is planned to be deprecated. Please refrain from using the parameter.

```yaml
Type: GrooveBlockOption
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
- OptOut
- HardOptin
- SoftOptin
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
