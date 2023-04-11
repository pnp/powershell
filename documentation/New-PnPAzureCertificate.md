---
Module Name: PnP.PowerShell
title: New-PnPAzureCertificate
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/New-PnPAzureCertificate.html
---
 
# New-PnPAzureCertificate

## SYNOPSIS
Generate a new 2048bit self-signed certificate and manifest settings for use when using CSOM via an app-only ADAL application.

See https://github.com/pnp/powershell/tree/master/samples/SharePoint.ConnectUsingAppPermissions for a sample on how to get started.

KeyCredentials contains the ADAL app manifest sections.

Certificate contains the PEM encoded certificate.

PrivateKey contains the PEM encoded private key of the certificate.

## SYNTAX

```powershell
New-PnPAzureCertificate [-CommonName <String>] [-Country <String>] [-State <String>]
 [-Locality <String>] [-Organization <String>] [-OrganizationUnit <String>] [-OutPfx <String>]
 [-OutCert <String>] [-ValidYears <Int32>] [-CertificatePassword <SecureString>] [-Store <StoreLocation>] 
```

## DESCRIPTION

Allows to create a self-signed certificate and manifest settings to be used with CSOM via an app-only ADAL application.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PnPAzureCertificate -OutPfx pnp.pfx -OutCert pnp.cer
```

This will generate a default self-signed certificate named "pnp.contoso.com" valid for 10 years and output a pfx and cer file to disk. The private key file (pfx) will not be password protected.

### EXAMPLE 2
```powershell
New-PnPAzureCertificate -CommonName "My Certificate" -ValidYears 30
```

This will output a certificate named "My Certificate" which expires in 30 years from now to the screen. It will not write the certificate files to disk.

### EXAMPLE 3
```powershell
New-PnPAzureCertificate -OutPfx pnp.pfx -OutCert pnp.cer -CertificatePassword (ConvertTo-SecureString -String "pass@word1" -AsPlainText -Force)
```

This will generate a default self-signed certificate named "pnp.contoso.com" valid for 10 years and output a pfx and cer file to disk. The pfx file will have the password pass@word1 set on it.

## PARAMETERS

### -CertificatePassword
Optional certificate password

```yaml
Type: SecureString
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CommonName
Common Name (e.g. server FQDN or YOUR name) [pnp.contoso.com]

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: 0Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Country
Country Name (2 letter code)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Locality
Locality Name (eg, city)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Organization
Organization Name (eg, company)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OrganizationUnit
Organizational Unit Name (eg, section)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutCert
Filename to write to, optionally including full path (.cer)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutPfx
Filename to write to, optionally including full path (.pfx)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -State
State or Province Name (full name)

```yaml
Type: String
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ValidYears
Number of years until expiration (default is 10, max is 30)

```yaml
Type: Int32
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Store
Local Certificate Store to add the certificate to. Only works on Microsoft Windows.

```yaml
Type: StoreLocation
Parameter Sets: Generate Certificate

Required: False
Position: Named
Accept pipeline input: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

