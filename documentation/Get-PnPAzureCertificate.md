---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureCertificate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureCertificate
---

# Get-PnPAzureCertificate

## SYNOPSIS

Get PEM values and manifest settings for an existing certificate (.pfx) for use when using CSOM via an app-only ADAL application.

See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.

KeyCredentials contains the ADAL app manifest sections.

Certificate contains the PEM encoded certificate.

PrivateKey contains the PEM encoded private key of the certificate.

## SYNTAX

### Default (Default)

```
Get-PnPAzureCertificate -Path <String> [-Password <SecureString>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to PEM values and manifest settings for an existing certificate (.pfx) for use when using CSOM via an app-only ADAL application.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureCertificate -Path "mycert.pfx"
```

This will output PEM values and ADAL app manifest settings for the certificate mycert.pfx.

### EXAMPLE 2

```powershell
Get-PnPAzureCertificate -Path "mycert.pfx" -Password (ConvertTo-SecureString -String "YourPassword" -AsPlainText -Force)
```

This will output PEM values and ADAL app manifest settings for the certificate mycert.pfx which has the password YourPassword.

### EXAMPLE 3

```powershell
Get-PnPAzureCertificate -Path "mycert.cer" | clip
```

Output the JSON snippet which needs to be replaced in the application manifest file and copies it to the clipboard

## PARAMETERS

### -Password

Password to the certificate (*.pfx)

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

### -Path

Path to the certificate (*.pfx)

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
