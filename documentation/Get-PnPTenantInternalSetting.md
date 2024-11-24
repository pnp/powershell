---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantInternalSetting.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantInternalSetting
---

# Get-PnPTenantInternalSetting

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns additional organizational level site collection properties available from endpoint `/_api/SPOInternalUseOnly.TenantAdminSettings`. This is an undocumented endpoint. Usage of this cmdlet might be subject to change if Microsoft changes the response.

## SYNTAX

### Default (Default)

```
Get-PnPTenantInternalSetting [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Returns organizational level site collection properties such as `SitePagesEnabled`, `DisableSelfServiceSiteCreation`, `EnableAutoNewsDigest`,
`CustomFormUrl`, `AutoQuotaEnabled`, `DisableGroupify`, `IncludeAtAGlanceInShareEmails`, `MailFromAddress`, `MobileNotificationIsEnabledForSharepoint`, `NewSiteManagedPath`, `NewSubsiteInModernOffForAll`, `NewSubsiteInModernOffForModernTemplates`, `NewTeamSiteManagedPath`, `ParentSiteUrl`, `PolicyOption`, `RequireSecondaryContact`, `ShowSelfServiceSiteCreation`, `SiteCreationNewUX`, `SmtpServer`, `SPListModernUXOff`, `TenantDefaultTimeZoneId` and `AvailableManagedPathsForSiteCreation`.

Currently, there are no parameters for this cmdlet.

You must have the SharePoint Online admin or Global admin role to run the cmdlet.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPTenantInternalSetting
```

This example returns internal tenant settings.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
