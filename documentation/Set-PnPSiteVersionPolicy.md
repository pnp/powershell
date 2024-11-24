---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteVersionPolicy.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSiteVersionPolicy
---

# Set-PnPSiteVersionPolicy

## SYNOPSIS

Sets file version policy related properties on the site.

**Required Permissions**

|        Type      |                    API/ Permission Name                    |                    Admin consent required                    |
| --------------- | --------------------------------------- | -------- |
| Delegated       | AllSites.FullControl | yes                               |

## SYNTAX

### Default (Default)

```
Set-PnPSiteVersionPolicy [-EnableAutoExpirationVersionTrim <Boolean>]
 [-ExpireVersionsAfterDays <UInt32>] [-MajorVersions <UInt32>] [-MajorWithMinorVersions <UInt32>]
 [-InheritFromTenant] [-ApplyToNewDocumentLibraries] [-ApplyToExistingDocumentLibraries]
 [-CancelForExistingDocumentLibraries] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Configures the versioning policy for a SharePoint Online site collection.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $true
```

This example sets AutoExpiration file version trim mode for a site. The new document libraries will use this version setting. Also creates a request to set the file version trim mode as AutoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 2

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -MajorWithMinorVersions 10 -ExpireVersionsAfterDays 200
```

This example sets ExpireAfter file version trim mode for a site. The new document libraries will use this version setting. Also creates a request to set the file version trim mode as ExpireAfter for existing document libraries that enabled versioning.

### EXAMPLE 3

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 300 -MajorWithMinorVersions 20 -ExpireVersionsAfterDays 0
```

Example 3 sets NoExpiration file version trim mode for a site. The new document libraries will use this version setting. Also creates a request to set the file version trim mode as NoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 4

```powershell
Set-PnPSiteVersionPolicy -InheritFromTenant
```

Example 4 clears the file version setting on a site. The new document libraries will use the tenant level setting.

### EXAMPLE 5

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $true -ApplyToNewDocumentLibraries
```

This example sets AutoExpiration file version trim mode for a site. The new document libraries will use this version setting.

### EXAMPLE 6

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -ExpireVersionsAfterDays 200 -ApplyToNewDocumentLibraries
```

This example sets ExpireAfter file version trim mode for a site. The new document libraries will use this version setting.

### EXAMPLE 7

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 300 -ExpireVersionsAfterDays 0 -ApplyToNewDocumentLibraries
```

Example 7 sets NoExpiration file version trim mode for a site. The new document libraries will use this version setting.

### EXAMPLE 8

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $true -ApplyToExistingDocumentLibraries
```

Example 8 creates a request to set the file version trim mode as AutoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 9

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -MajorWithMinorVersions 5 -ExpireVersionsAfterDays 200 -ApplyToExistingDocumentLibraries
```

This example creates a request to set the file version trim mode as ExpireAfter for existing document libraries that enabled versioning.

### EXAMPLE 10

```powershell
Set-PnPSiteVersionPolicy -EnableAutoExpirationVersionTrim $false -MajorVersions 100 -MajorWithMinorVersions 5 -ExpireVersionsAfterDays 0 -ApplyToExistingDocumentLibraries
```

Example 10 creates a request to set the file version trim mode as NoExpiration for existing document libraries that enabled versioning.

### EXAMPLE 11

```powershell
Set-PnPSiteVersionPolicy -CancelForExistingDocumentLibraries
```

This example cancels the existing request which sets the file version trim mode for existing document libraries on a site.

## PARAMETERS

### -ApplyToExistingDocumentLibraries

Creates a request to set the file version trim mode for existing document libraries that enabled versioning. Works with parameters EnableAutoExpirationVersionTrim, ExpireVersionsAfterDays, MajorVersions and MajorWithMinorVersions.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ApplyToNewDocumentLibraries

Sets site version policy for new document libraries. Works with parameters EnableAutoExpirationVersionTrim, ExpireVersionsAfterDays, MajorVersions and MajorWithMinorVersions.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -CancelForExistingDocumentLibraries

Cancels the existing request which sets the file version trim mode for existing document libraries on a site.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

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

### -EnableAutoExpirationVersionTrim

Enables or disables AutoExpiration version trim for the document libraries on the site. Set to $true to enable, $false to disable.

Parameter ExpireVersionsAfterDays is required when EnableAutoExpirationVersionTrim is false. Set it to 0 for NoExpiration, set it to greater or equal to 30 for ExpireAfter.

Parameter MajorVersions is required when EnableAutoExpirationVersionTrim is false.

Parameter MajorWithMinorVersions is required when EnableAutoExpirationVersionTrim is false and the setting is for document libraries that including existing ones. It is used when minor version is enabled on the document libraries.

```yaml
Type: Boolean
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ExpireVersionsAfterDays

Expires the version after the days. Works with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -InheritFromTenant

Clears the file version setting on a site. The new document libraries will use the tenant level setting.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MajorVersions

Maximum major versions to keep. Works with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -MajorWithMinorVersions

Maximum major versions for which to keep minor versions. Works with parameter EnableAutoExpirationVersionTrim. Please see description in EnableAutoExpirationVersionTrim.

```yaml
Type: UInt32
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: Set Properties
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
