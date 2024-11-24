---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFileAnonymousSharingLink.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFileAnonymousSharingLink
---

# Add-PnPFileAnonymousSharingLink

## SYNOPSIS

Creates an anonymous sharing link to share a file.

## SYNTAX

### Default (Default)

```
Add-PnPFileAnonymousSharingLink -FileUrl <String> -Type <PnP.Core.Model.Security.ShareType>
 -Password <String> -ExpirationDateTime <DateTime> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates an anonymous sharing link to share a file.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPFileAnonymousSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx"
```

This will create an anonymous sharing link for `Test.docx` file in the `Shared Documents` library which will be viewable.

### EXAMPLE 2

```powershell
Add-PnPFileAnonymousSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx" -Type Edit -Password "PnPRocks!"
```

This will create an anonymous sharing link for `Test.docx` file in the `Shared Documents` library which will be editable by anonymous users after specifying the password.

### EXAMPLE 3

```powershell
Add-PnPFileAnonymousSharingLink -FileUrl "/sites/demo/Shared Documents/Test.docx" -Type View -ExpirationDateTime (Get-Date).AddDays(15)
```

This will create an anonymous sharing link for `Test.docx` file in the `Shared Documents` library which will be viewable by anonymous users. The link will expire after 15 days.

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

### -ExpirationDateTime

The expiration date to be after which the file link will expire.

```yaml
Type: DateTime
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

### -FileUrl

The file in the site

```yaml
Type: String
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

### -Password

The password to be set for the file to be shared.

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

### -ShareType

The type of sharing that you want to, i.e do you want to enable people in your organization to view the shared content or also edit the content?

`CreateOnly` value is not supported.

```yaml
Type: PnP.Core.Model.Security.ShareType
DefaultValue: View
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
