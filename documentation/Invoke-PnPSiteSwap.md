---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Invoke-PnPSiteSwap.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Invoke-PnPSiteSwap
---

# Invoke-PnPSiteSwap

## SYNOPSIS

Invokes a job to swap the location of a site with another site while archiving the original site.

## SYNTAX

### Default (Default)

```
Invoke-PnPSiteSwap -SourceUrl <string> -TargetUrl <string> -ArchiveUrl <string>
 [-DisableRedirection] [-NoWait] [-Verbose] [-Connection <PnPConnection>] [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Swaps the location of a source site with a target site while archiving the original target site.

Please note, the target site must be either:

* The root site, for example https://tenant-name.sharepoint.com; or
* The search center site, for example https://tenant-name.sharepoint.com/search.

When the swap is initiated, the target site is moved to the archive location and the source site is moved to the target location. By default, a site redirect is created at the source location that will redirect traffic to the target location.

If the target is the root site at https://tenant-name.sharepoint.com, then the following preparation activities should be performed prior to performing the swap:

1. Any Featured links defined in SharePoint Start Page at https://tenant-name.sharepoint.com/_layouts/15/sharepoint.aspx will not be displayed after performing the swap. If required, the Featured links should be documented so they can be manually recreated after the swap.
1. Functionality such as external sharing and application interfaces are dependent on the policies and permissions defined at the root site. Review the source site to ensure that it has the required policies and permissions as per the existing root site. This includes external sharing settings as well as site permissions.
1. Larger tenants that have more than ~10,000 licenses may also need to run the Page Diagnostic Tool against the source site. Any analysis results that have the category Attention required (Red) or Improvement opportunities (Orange) will need to be remediated before performing the swap.

The source and target sites can't be connected to an Office 365 group. They also can't be hub sites or associated with a hub. If a site is a hub site, unregister it as a hub site, swap the root site, and then register the site as a hub site. If a site is associated with a hub, disassociate the site, swap the root site, and then re-associate the site.

## EXAMPLES

### EXAMPLE 1

```powershell
Invoke-PnPSiteSwap -SourceUrl https://contoso.sharepoint.com/sites/CommunicationSite -TargetUrl https://contoso.sharepoint.com -ArchiveUrl https://contoso.sharepoint.com/sites/Archive
```

Archives the existing site at https://contoso.sharepoint.com to https://contoso.sharepoint.com/sites/Archive and moves https://contoso.sharepoint.com/sites/CommunicationSite to https://contoso.sharepoint.com. A site redirect will be created at https://contoso.sharepoint.com/sites/CommunicationSite that will redirect any requests to https://contoso.sharepoint.com.

### EXAMPLE 2

```powershell
Invoke-PnPSiteSwap -SourceUrl https://contoso.sharepoint.com/sites/SearchSite -TargetUrl https://contoso.sharepoint.com/search -ArchiveUrl https://contoso.sharepoint.com/sites/Archive
```

Archives the existing Search Center site at https://contoso.sharepoint.com/search to https://contoso.sharepoint.com/sites/Archive and moves the https://contoso.sharepoint.com/sites/SearchSite to https://contoso.sharepoint.com/search. A site redirect be created at https://contoso.sharepoint.com/sites/SearchSite that will redirect any requests to https://contoso.sharepoint.com/search.

### EXAMPLE 3

```powershell
Invoke-PnPSiteSwap -SourceUrl https://contoso.sharepoint.com/sites/CommunicationSite -TargetUrl https://contoso.sharepoint.com -ArchiveUrl https://contoso.sharepoint.com/sites/Archive -DisableRedirection
```

Archives the existing site at https://contoso.sharepoint.com to https://contoso.sharepoint.com/sites/Archive and moves https://contoso.sharepoint.com/sites/CommunicationSite to https://contoso.sharepoint.com. A site redirect will not be created at https://contoso.sharepoint.com/sites/CommunicationSite.

## PARAMETERS

### -ArchiveUrl

URL that the target site will be archived to. There should be no existing site, including a deleted site in the Recycle Bin, at this location before performing the swap.

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

### -DisableRedirection

Disables the site redirect from being created at the Source URL location.

```yaml
Type: Switch Parameter
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

### -SourceUrl

URL of the source site. The site at this location must exist before performing the swap.

If the target is the root site at https://tenant-name.sharepoint.com then the source site must be either a Modern Team Site (STS#3) or a Communication Site (SITEPAGEPUBLISHING#0).

If the target is the search center site at https://tenant-name.sharepoint.com/search then the source site must be either a Search Center Site (SRCHCEN#0) or a Basic Search Center Site (SRCHCENTERLITE#0).

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

### -TargetUrl

URL of the target site that the source site will be swapped to. The site at this location must exist before performing the swap.

The target site must be either:

* The root site at https://tenant-name.sharepoint.com; or
* The search center site at https://tenant-name.sharepoint.com/search.

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

### -Verbose

When provided, additional debug statements will be shown while executing the cmdlet.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
