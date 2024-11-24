---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPAzureCertificate.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPAzureCertificate
---

# New-PnPAzureCertificate

## SYNOPSIS

Generate a new 2048bit self-signed certificate and manifest settings for use when using CSOM via an app-only ADAL application.

See https://github.com/pnp/powershell/tree/master/samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.

KeyCredentials contains the ADAL app manifest sections.

Certificate contains the PEM encoded certificate.

PrivateKey contains the PEM encoded private key of the certificate.

## SYNTAX

### Default (Default)

```
New-PnPAzureCertificate [-CommonName <String>] [-Country <String>] [-State <String>]
 [-Locality <String>] [-Organization <String>] [-OrganizationUnit <String>] [-OutPfx <String>]
 [-OutCert <String>] [-ValidYears <Int32>] [-CertificatePassword <SecureString>]
 [-Store <StoreLocation>] [-SanNames <String[]>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to create a self-signed certificate and manifest settings to be used with PnP PowerShell via an app-only application registration.

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPAzureCertificate -OutPfx pnp.pfx -OutCert pnp.cer
```

This will generate a default self-signed certificate named "pnp.contoso.com" valid for 10 years and output a pfx and cer file to disk. The private key file (pfx) will not be password protected. It will have localhost and the machinename as the Subject Alternative Names.

### EXAMPLE 2

```powershell
New-PnPAzureCertificate -CommonName "My Certificate" -ValidYears 30
```

This will output a certificate named "My Certificate" which expires in 30 years from now to the screen. It will not write the certificate files to disk. It will have localhost and the machinename as the Subject Alternative Names.

### EXAMPLE 3

```powershell
New-PnPAzureCertificate -OutPfx pnp.pfx -OutCert pnp.cer -CertificatePassword (ConvertTo-SecureString -String "pass@word1" -AsPlainText -Force)
```

This will generate a default self-signed certificate named "pnp.contoso.com" valid for 10 years and output a pfx and cer file to disk. The pfx file will have the password pass@word1 set on it. It will have localhost and the machinename as the Subject Alternative Names.

### EXAMPLE 4

```powershell
New-PnPAzureCertificate -OutPfx pnp.pfx -OutCert pnp.cer -SanNames $null
```

This will generate a default self-signed certificate named "pnp.contoso.com" valid for 10 years and output a pfx and cer file to disk. There will not be any Subject Alternative Names in the generated certificate.

## PARAMETERS

### -CertificatePassword

Optional certificate password

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

### -CommonName

Common Name (e.g. server FQDN or YOUR name) [pnp.contoso.com]

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Country

Country Name (2 letter code)

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

### -Locality

Locality Name (eg, city)

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

### -Organization

Organization Name (eg, company)

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

### -OrganizationUnit

Organizational Unit Name (eg, section)

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

### -OutCert

Filename to write to, optionally including full path (.cer)

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

### -OutPfx

Filename to write to, optionally including full path (.pfx)

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

### -SanNames

One or more DNS names to add to the certificate as Subject Alternative Names. Separate multiple names with a comma, i.e. "host1.domain.com","host2.domain.com".

Provide $null to not add any Subject Alternative names to the certificate.

Omit to add localhost and the machine name as Subject Alternative Names.

```yaml
Type: String[]
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

### -State

State or Province Name (full name)

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

### -Store

Local Certificate Store to add the certificate to. Only works on Microsoft Windows.

```yaml
Type: StoreLocation
DefaultValue: ''
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Generate Certificate
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ValidYears

Number of years until expiration (default is 10, max is 30)

```yaml
Type: Int32
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
