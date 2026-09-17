---
Module Name: PnP.PowerShell
schema: 2.0.0
applicable: SharePoint Online
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureCertificate.html
external help file: PnP.PowerShell.dll-Help.xml
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

```powershell
Get-PnPAzureCertificate -Path <String> [-Password <SecureString>] 
```

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Path to the certificate (*.pfx)

```yaml
Type: String
Parameter Sets: (All)

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)


