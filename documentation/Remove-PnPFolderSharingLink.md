---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPFolderSharingLink.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPFolderSharingLink
---

# Remove-PnPFolderSharingLink

## SYNOPSIS

Removes sharing links associated with a folder.

## SYNTAX

### Default (Default)

```
Remove-PnPFolderSharingLink -Folder <FolderPipeBind> -Identity <String> -Force <SwitchParamter>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes sharing links associated with a folder.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPFolderSharingLink -Folder "/sites/demo/Shared Documents/Test"
```

This will delete all the sharing links associated with the `Test` folder in the `Shared Documents` document library.

### EXAMPLE 2

```powershell
Remove-PnPFolderSharingLink -Folder "/sites/demo/Shared Documents/Test" -Force
```

This will delete all the sharing links associated with the `Test` folder in the `Shared Documents` document library. User will not be prompted for confirmation.

### EXAMPLE 3

```powershell
$sharingLinks = Get-PnPFolderSharingLink -Folder "/sites/demo/Shared Documents/Test"
Remove-PnPFolderSharingLink -Folder "/sites/demo/Shared Documents/Test" -Identity $sharingLinks[0].Id -Force
```

This will delete the first sharing link associated with the `Test` folder in the `Shared Documents` document library. User will not be prompted for confirmation.

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

### -Force

If provided, no confirmation will be requested and the action will be performed

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

The Id of the sharing link associated with the folder.
You can retrieve the identity using `Get-PnPFolderSharingLink` cmdlet.

```yaml
Type: Identity
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
