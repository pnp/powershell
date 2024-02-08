---
Module Name: PnP.PowerShell
title: Set-PnPSiteVersionPolicy
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteVersionPolicy.html
---
 
# Set-PnPSiteVersionPolicy

## SYNOPSIS
Set file version policy related properties on the site.

## SYNTAX

```powershell
Set-PnPSiteVersionPolicy 
 [-EnableAutoExpirationVersionTrim <Boolean>]
 [-ExpireVersionsAfterDays <UInt32>]
 [-MajorVersions <UInt32>]
 [-MajorWithMinorVersions <UInt32>]
 [-InheritFromTenant]
 [-ApplyToNewDocumentLibraries]
 [-ApplyToExistingDocumentLibraries]
 [-CancelForExistingDocumentLibraries]
 [-Connection <PnPConnection>]
```

## EXAMPLES

### EXAMPLE 1
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $true
```

Set AutoExpiration file version trim mode for a site. The new document libraries will use this version setting. Also create a request to set the file version trim mode as AutoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 2
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -MajorWithMinorVersions 10 -ExpireVersionsAfterDays 200
```

Set ExpireAfter file version trim mode for a site. The new document libraries will use this version setting. Also create a request to set the file version trim mode as ExpireAfter for existing document libraries that enabled versioning.

### EXAMPLE 3
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 300 -MajorWithMinorVersions 20 -ExpireVersionsAfterDays 0
```

Set NoExpiration file version trim mode for a site. The new document libraries will use this version setting. Also create a request to set the file version trim mode as NoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 4
```powershell
Set-PnPSiteVersionPolicy -InheritFromTenant
```

Clear the file version setting on a site. The new document libraries will use the tenant level setting.

### EXAMPLE 5
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $true -ApplyToNewDocumentLibraries
```

Set AutoExpiration file version trim mode for a site. The new document libraries will use this version setting.

### EXAMPLE 6
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -ExpireVersionsAfterDays 200 -ApplyToNewDocumentLibraries
```

Set ExpireAfter file version trim mode for a site. The new document libraries will use this version setting.

### EXAMPLE 7
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 300 -ExpireVersionsAfterDays 0 -ApplyToNewDocumentLibraries
```

Set NoExpiration file version trim mode for a site. The new document libraries will use this version setting.

### EXAMPLE 8
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $true -ApplyToExistingDocumentLibraries
```

Create a request to set the file version trim mode as AutoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 9
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -MajorWithMinorVersions 5 -ExpireVersionsAfterDays 200 -ApplyToExistingDocumentLibraries
```

Create a request to set the file version trim mode as ExpireAfter for existing document libraries that enabled versioning.

### EXAMPLE 10
```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -MajorWithMinorVersions 5 -ExpireVersionsAfterDays 0 -ApplyToExistingDocumentLibraries
```

Create a request to set the file version trim mode as NoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 11
```powershell
Set-PnPSiteVersionPolicy -CancelForExistingDocumentLibraries
```

Cancel the existing request which sets the file version trim mode for existing document libraries on a site.

## PARAMETERS

### -EnableAutoExpirationVersionTrim
Enable or disable AutoExpiration version trim for the document libraries on the site. Set to $true to enable, $false to disable.

Parameter ExpireVersionsAfterDays is required when EnableAutoExpirationVersionTrim is false. Set it to 0 for NoExpiration, set it to greater or equal to 30 for ExpireAfter.

Parameter MajorVersions is required when EnableAutoExpirationVersionTrim is false.

Parameter MajorWithMinorVersions is required when EnableAutoExpirationVersionTrim is false and the setting is for document libraries that including existing ones. It is used when minor version is enabled on the document libraries.

```yaml
Type: Boolean
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExpireVersionsAfterDays
Expire the version after the days. Work with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorVersions
Maximum major versions to keep. Work with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MajorWithMinorVersions
Maximum major versions for which to keep minor versions. Work with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InheritFromTenant
Clear the file version setting on a site. The new document libraries will use the tenant level setting.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplyToNewDocumentLibraries
Set site version policy for new document libraries. Work with parameters EnableAutoExpirationVersionTrim, ExpireVersionsAfterDays, MajorVersions and MajorWithMinorVersions.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ApplyToExistingDocumentLibraries
Create a request to set the file version trim mode for existing document libraries that enabled versioning. Work with parameters EnableAutoExpirationVersionTrim, ExpireVersionsAfterDays, MajorVersions and MajorWithMinorVersions.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CancelForExistingDocumentLibraries
Cancel the existing request which sets the file version trim mode for existing document libraries on a site.

```yaml
Type: SwitchParameter
Parameter Sets: Set Properties

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

