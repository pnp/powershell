---
Module Name: PnP.PowerShell
title: Get-PnPTenantInternalSetting
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantInternalSetting.html
---
 
# Get-PnPTenantInternalSetting

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns additional organization-level site collection properties available from endpoint _api/SPOInternalUseOnly.TenantAdminSettings.

## SYNTAX

```powershell
Get-PnPTenantInternalSetting [-Connection <PnPConnection>] 
```

## DESCRIPTION
Returns organization-level site collection properties such as SitePagesEnabled, DisableSelfServiceSiteCreation, EnableAutoNewsDigest,
CustomFormUrl, AutoQuotaEnabled, DisableGroupify, IncludeAtAGlanceInShareEmails, MailFromAddress, MobileNotificationIsEnabledForSharepoint, NewSiteManagedPath, NewSubsiteInModernOffForAll, NewSubsiteInModernOffForModernTemplates, NewTeamSiteManagedPath, ParentSiteUrl, PolicyOption, RequireSecondaryContact, ShowSelfServiceSiteCreation, SiteCreationNewUX, SmtpServer, SPListModernUXOff, TenantDefaultTimeZoneId and AvailableManagedPathsForSiteCreation.

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
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)