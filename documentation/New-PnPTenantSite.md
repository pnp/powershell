---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/New-PnPTenantSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: New-PnPTenantSite
---

# New-PnPTenantSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new (classic) site collection for the current tenant

## SYNTAX

### Default (Default)

```
New-PnPTenantSite -Title <String> -Url <String> -Owner <String> -TimeZone <Int32> [-Lcid <UInt32>]
 [-Template <String>] [-ResourceQuota <Double>] [-ResourceQuotaWarningLevel <Double>]
 [-StorageQuota <Int64>] [-StorageQuotaWarningLevel <Int64>] [-RemoveDeletedSite]
 [-SharingCapability <SharingCapabilities>] [-Wait] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The New-PnPTenantSite cmdlet creates a new site collection for the current company. However, creating a new SharePoint Online site collection fails if a deleted site with the same URL exists in the Recycle Bin

## EXAMPLES

### EXAMPLE 1

```powershell
New-PnPTenantSite -Title Contoso -Url "https://tenant.sharepoint.com/sites/contoso" -Owner user@example.org -TimeZone 4 -Template STS#0
```

This will add a site collection with the title 'Contoso', the url 'https://tenant.sharepoint.com/sites/contoso', the timezone 'UTC+01:00',the owner 'user@example.org' and the template used will be STS#0, a TeamSite

### EXAMPLE 2

```powershell
New-PnPTenantSite -Title Contoso -Url /sites/contososite -Owner user@example.org -TimeZone 4 -Template STS#0
```

This will add a site collection with the title 'Contoso', the url 'https://tenant.sharepoint.com/sites/contososite' of which the base part will be picked up from your current connection, the timezone 'UTC+01:00', the owner 'user@example.org' and the template used will be STS#0, a TeamSite

## PARAMETERS

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

### -Lcid

Specifies the language of this site collection. For more information, see Locale IDs supported by SharePoint at https://github.com/pnp/powershell/wiki/Supported-LCIDs-by-SharePoint. To get the list of supported languages on a SharePoint environment use: Get-PnPAvailableLanguage.

```yaml
Type: UInt32
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

### -Owner

Specifies the user name of the site collection's primary owner. The owner must be a user instead of a security group or an email-enabled security group.

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

### -RemoveDeletedSite

Specifies if any existing site with the same URL should be removed from the recycle bin

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

### -ResourceQuota

Specifies the quota for this site collection in Sandbox Solutions units. This value must not exceed the company's aggregate available Sandbox Solutions quota. The default value is 0. For more information, see Resource Usage Limits on Sandbox Solutions in SharePoint 2010 : https://learn.microsoft.com/previous-versions/office/developer/sharepoint-2010/gg615462(v=office.14).

```yaml
Type: Double
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

### -ResourceQuotaWarningLevel

Specifies the warning level for the resource quota. This value must not exceed the value set for the ResourceQuota parameter

```yaml
Type: Double
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

### -SharingCapability

Specifies what the sharing capabilities are for the site. Possible values: Disabled, ExternalUserSharingOnly, ExternalUserAndGuestSharing, ExistingExternalUserSharingOnly. It will only work if Wait parameter is specified.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (Wait)
  Position: Named
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues:
- Disabled
- ExternalUserSharingOnly
- ExternalUserAndGuestSharing
- ExistingExternalUserSharingOnly
HelpMessage: ''
```

### -StorageQuota

Specifies the storage quota for this site collection in megabytes. This value must not exceed the company's available quota.

```yaml
Type: Int64
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

### -StorageQuotaWarningLevel

Specifies the warning level for the storage quota in megabytes. This value must not exceed the values set for the StorageQuota parameter

```yaml
Type: Int64
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

### -Template

Specifies the site collection template type. Use the Get-PnPWebTemplates cmdlet to get the list of valid templates. If no template is specified, one can be added later. The Template and LocaleId parameters must be a valid combination as returned from the Get-PnPWebTemplates cmdlet.

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

### -TimeZone

Use Get-PnPTimeZoneId to retrieve possible timezone values

```yaml
Type: Int32
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

### -Title

Specifies the title of the new site collection

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

### -Url

Specifies the full URL of the new site collection. It must be in a valid managed path in the company's site. For example, for company contoso, valid managed paths are https://contoso.sharepoint.com/sites and https://contoso.sharepoint.com/teams.

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

### -Wait

Waits for the site collection to be fully provisioned.

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
- Name: (Wait)
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
