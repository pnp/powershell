---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFolderAnonymousSharingLink.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFolderAnonymousSharingLink
---

# Add-PnPFolderAnonymousSharingLink

## SYNOPSIS

Creates an anonymous sharing link to share a folder.

## SYNTAX

### Default (Default)

```
Add-PnPFolderAnonymousSharingLink -Folder <FolderPipeBind> -Type <PnP.Core.Model.Security.ShareType>
 -Password <String> -ExpirationDateTime <DateTime> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a new anonymous sharing link for a folder.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPFolderAnonymousSharingLink -Folder "/sites/demo/Shared Documents/Test"
```

This will create an anonymous sharing link for `Test` folder in the `Shared Documents` library which will be viewable to anonymous users.

### EXAMPLE 2

```powershell
Add-PnPFolderAnonymousSharingLink -Folder "/sites/demo/Shared Documents/Test" -Type Edit -Password "PnPRocks!"
```

This will create an anonymous sharing link for `Test` folder in the `Shared Documents` library which will be editable by anonymous users with the specified password.

### EXAMPLE 3

```powershell
Add-PnPFolderAnonymousSharingLink -Folder "/sites/demo/Shared Documents/Test" -Type Edit -Password "PnPRocks!" -ExpirationDateTime (Get-Date).AddDays(15)
```

This will create an anonymous sharing link for `Test` folder in the `Shared Documents` library which will be editable by anonymous users with the specified password. The link will expire after 15 days.

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

The expiration date for the folder after which the shared link will stop working.

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

### -Folder

The folder in the site

```yaml
Type: FolderPipeBind
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

The password for the folder which will be shared anonymously.

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

The type of sharing that you want to, i.e do you want to enable anonymous users to view the shared content or also edit the content?

`Review` and `BlocksDownload` values are not supported.

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
