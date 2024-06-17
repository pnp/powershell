---
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
schema: 2.0.0
online version: https://pnp.github.io/powershell/cmdlets/Publish-PnPCompanyApp.html
title: Publish-PnPCompanyApp
---

# Publish-PnPCompanyApp

## SYNOPSIS
This cmdlet publishes a new company app to Microsoft Teams.
** This cmdlet is obsolete and will not be supported **

## SYNTAX

```powershell
Publish-PnPCompanyApp -PortalUrl <String> -AppName <String> [-Description <String>] [-LongDescription <String>]
 [-PrivacyPolicyUrl <String>] [-TermsAndUsagePolicyUrl <String>] -CompanyName <String>
 -CompanyWebSiteUrl <String> -ColoredIconPath <String> -OutlineIconPath <String>
 [-Force] [-NoUpload] [-Connection <PnPConnection>] 
```

## DESCRIPTION
This cmdlet publishes a new company app (Microsoft Viva Connections) to Microsoft Teams. It will create a new package (zip file) in the current folder named after the CompanyApp value specified. E.g. if the name is 'Contoso Portal', the package will be called "Contoso Portal.zip". This package will be uploaded to the Teams App Catalog. If you do not want to upload the package automatically, e.g. prepare a package ahead of time, specify '-NoUpload'.

## EXAMPLES

### Example 1
```powershell
Publish-PnPCompanyApp -PortalUrl https://contoso.sharepoint.com/sites/portal -AppName "Contoso Portal" -CompanyName "Contoso" -CompanyWebSite "https://www.contoso.com" -ColoredIconPath ./coloricon.png -OutlineIconPath ./outlinedicon
```

This will create a new zip file called "Contoso Portal.zip" and it will upload this app to the Teams App Catalog.

## PARAMETERS

### -AppName
The name of the app as you want it to appear in Teams.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColoredIconPath
The path to the color icon (192x192px).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CompanyName
The name of your company/organization.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CompanyWebSiteUrl
The link to the public website of your company / organization.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
A short description for the app (less than 80 characters).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
This will overwrite an existing zip file if present and no confirmation will be asked.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LongDescription
A longer description for the app (less than 4000 characters).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoUpload
If specified the app package will not be uploaded to the Teams App Catalog.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OutlineIconPath
The path to the outline icon (32x32 px).

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PortalUrl
The URL to the site you want to use in the app. This has to be a Communication Site.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PrivacyPolicyUrl
Privacy policy link for the app. If not specified the default privacy policy link from Microsoft will be used.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TermsAndUsagePolicyUrl
The Terms of Use link for the app. If not specified the default Terms of Use from Microsoft link will be used.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
