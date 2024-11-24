---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPFolderOrganizationalSharingLink.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPFolderOrganizationalSharingLink
---

# Add-PnPFolderOrganizationalSharingLink

## SYNOPSIS

Creates an organizational sharing link for a folder.

## SYNTAX

### Default (Default)

```
Add-PnPFolderOrganizationalSharingLink -Folder <FolderPipeBind>
 -Type <PnP.Core.Model.Security.ShareType> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Creates a new organization sharing link for a folder.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPFolderOrganizationalSharingLink -Folder "/sites/demo/Shared Documents/Test"
```

This will create an organization sharing link for `Test` folder in the `Shared Documents` library which will be viewable by users in the organization.

### EXAMPLE 2

```powershell
Add-PnPFolderOrganizationalSharingLink -Folder "/sites/demo/Shared Documents/Test" -Type Edit
```

This will create an organization sharing link for `Test` folder in the `Shared Documents` library which will be editable by users in the organization.

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

### -ShareType

The type of sharing that you want to, i.e do you want to enable people in your organization to view the shared content or also edit the content?

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
