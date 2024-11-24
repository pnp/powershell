---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPManagedAppId.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPManagedAppId
---

# Set-PnPManagedAppId

## SYNOPSIS

Sets/Adds an App Id for use with Connect-PnPOnline to the Windows Credential Manager or Mac OS Key Chain Entry. If you the PowerShell Module Microsoft.PowerShell.SecretsStore and Microsoft.PowerShell.SecretsManagement installed and you have defined a default vault without a password than that will be used to store the App Id.

## SYNTAX

### Default (Default)

```
Set-PnPManagedAppId -Url <String> -AppId <String> [-Overwrite]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Adds an App Id entry to the Windows Credential Manager or Mac OS Key Chain Entry. PnP PowerShell will check if an App Id is available when you connect using Connect-PnPOnline -Interactive. If it finds a matching URL it will use the associated App Id. You do not need to specify the -ClientId parameter then.

If you add a Credential with a name of "https://yourtenant.sharepoint.com" it will find a match when you connect to "https://yourtenant.sharepoint.com" but also when you connect to "https://yourtenant.sharepoint.com/sites/demo1". Of course you can specify more granular entries, allow you to automatically provide App Ids for different URLs.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPManagedAppId -Name "https://tenant.sharepoint.com" -AppId d96c0a07-770d-46f4-bb38-a54084254bf7
```
This will add an entry for the specified App Id to be use when connecting with Connect-PnPOnline to the URL specified.

## PARAMETERS

### -AppId

The App Id to associate with the Url.

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

### -Overwrite

Use parameter to overwrite existing Mac OS Key Chain Entry. Not required on Windows.

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

### -Url

The URL to associate the App Id with

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
