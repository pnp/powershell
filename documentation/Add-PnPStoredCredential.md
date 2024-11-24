---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPStoredCredential.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPStoredCredential
---

# Add-PnPStoredCredential

## SYNOPSIS

Adds a credential to the Windows Credential Manager or Mac OS Key Chain Entry.

## SYNTAX

### Default (Default)

```
Add-PnPStoredCredential -Name <String> -Username <String> [-Password <SecureString>] [-Overwrite]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Adds an entry to the Windows Credential Manager or Mac OS Key Chain Entry. If you add an entry in the form of the URL of your tenant/server PnP PowerShell will check if that entry is available when you connect using Connect-PnPOnline. If it finds a matching URL it will use the associated credentials.

If you add a Credential with a name of "https://yourtenant.sharepoint.com" it will find a match when you connect to "https://yourtenant.sharepoint.com" but also when you connect to "https://yourtenant.sharepoint.com/sites/demo1". Of course you can specify more granular entries, allow you to automatically provide credentials for different URLs.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPStoredCredential -Name "https://tenant.sharepoint.com" -Username yourname@tenant.onmicrosoft.com
```

You will be prompted to specify the password and a new entry will be added with the specified values

### EXAMPLE 2

```powershell
Add-PnPStoredCredential -Name "https://tenant.sharepoint.com" -Username yourname@tenant.onmicrosoft.com -Password (ConvertTo-SecureString -String "YourPassword" -AsPlainText -Force)
```

A new entry will be added with the specified values

### EXAMPLE 3

```powershell
Add-PnPStoredCredential -Name "https://tenant.sharepoint.com" -Username yourname@tenant.onmicrosoft.com -Password (ConvertTo-SecureString -String "YourPassword" -AsPlainText -Force)
Connect-PnPOnline -Url "https://tenant.sharepoint.com/sites/mydemosite"
```

A new entry will be added with the specified values, and a subsequent connection to a sitecollection starting with the entry name will be made. Notice that no password prompt will occur.

## PARAMETERS

### -Name

The credential to set

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

### -Password

If not specified you will be prompted to enter your password.
If you want to specify this value use ConvertTo-SecureString -String 'YourPassword' -AsPlainText -Force

```yaml
Type: SecureString
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

### -Username



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
