---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/new-pnpazurecertificate
schema: 2.0.0
title: New-PnPAzureCertificate
---

# New-PnPAzureCertificate

## SYNOPSIS
Generate a new 2048bit self-signed certificate and manifest settings for use when using CSOM via an app-only ADAL application.

See https://github.com/SharePoint/PnP-PowerShell/tree/master/Samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.

KeyCredentials contains the ADAL app manifest sections.

Certificate contains the PEM encoded certificate.

PrivateKey contains the PEM encoded private key of the certificate.

## SYNTAX

```
New-PnPAzureCertificate [[-CommonName] <String>] [[-Country] <String>] [[-State] <String>]
 [[-Locality] <String>] [[-Organization] <String>] [[-OrganizationUnit] <String>] [[-OutPfx] <String>]
 [[-OutCert] <String>] [[-ValidYears] <Int32>] [[-CertificatePassword] <SecureString>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPAzureCertificate -OutPfx pnp.pfx -OutCert pnp.cer
```

This will generate a default self-signed certificate named "pnp.contoso.com" valid for 10 years and output a pfx and cer file.

### EXAMPLE 2
```powershell
New-PnPAzureCertificate -CommonName "My Certificate" -ValidYears 30
```

This will output a certificate named "My Certificate" which expires in 30 years from now.

## PARAMETERS

### -CertificatePassword
Optional certificate password

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: False
Position: 8
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommonName
Common Name (e.g. server FQDN or YOUR name) [pnp.contoso.com]

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Country
Country Name (2 letter code)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Locality
Locality Name (eg, city)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Organization
Organization Name (eg, company)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrganizationUnit
Organizational Unit Name (eg, section)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutCert
Filename to write to, optionally including full path (.cer)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutPfx
Filename to write to, optionally including full path (.pfx)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -State
State or Province Name (full name)

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidYears
Number of years until expiration (default is 10, max is 30)

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: 7
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)