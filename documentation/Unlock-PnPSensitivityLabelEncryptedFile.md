---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Unlock-PnPSensitivityLabelEncryptedFile.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Unlock-PnPSensitivityLabelEncryptedFile
---

# Unlock-PnPSensitivityLabelEncryptedFile

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

## SYNTAX

### Default (Default)

```
Unlock-PnPSensitivityLabelEncryptedFile -Url <String> -JustificationText <string>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

It removes encryption on a Sensitivity label encrypted file in SharePoint Online.

## EXAMPLES

### EXAMPLE 1

```powershell
Unlock-PnPSensitivityLabelEncryptedFile -Url "https://contoso.com/sites/Marketing/Shared Documents/Doc1.docx" -JustificationText "Need to access file"
```

This example will remove a regular label with admin defined encryption from the file Doc1.docx and also make an entry in audit logs.

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

### -JustificationText

Text that explains the reason to run this cmdlet on the given file.

```yaml
Type: string.
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

### -Url

Full URL for the file

```yaml
Type: string.
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
