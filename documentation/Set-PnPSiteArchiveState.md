---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteArchiveState.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSiteArchiveState
---

# Set-PnPSiteArchiveState

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the archived state of the site. Can be used to archive and reactivate sites.

## SYNTAX

### Default (Default)

```
Set-PnPSiteArchiveState -Identity <SPOSitePipeBind> -ArchiveState <SPOArchiveState> [-NoWait]
 [-Force]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Use this cmdlet to change the archive status of the site. You must be a SharePoint Online administrator or Global administrator and be a site collection administrator to run the cmdlet.
Microsoft 365 Archive needs to be enabled for the organization to be able to use this feature.

## EXAMPLES

### Example 1

```powershell
Set-PnPSiteArchiveState https://contoso.sharepoint.com/sites/Marketing -ArchiveState Archived
```

This example marks the site as Archived. For seven days after the operation, the site will remain in a "RecentlyArchived" state, where any reactivations will be free and instantaneous. If a site is reactivated after seven days, any reactivations will be charged and will take time.

### Example 2

```powershell
Set-PnPSiteArchiveState https://contoso.sharepoint.com/sites/Marketing -ArchiveState Active
```

This example triggers the reactivation of a site. If the site is reactivated from the "RecentlyArchived" state, it will become available instantaneously. If the site is reactivated from the "FullyArchived" state, it may take time for it to be reactivated.

## PARAMETERS

### -ArchiveState

Sets the archived state of the site. Valid values are Archived, Active.

```yaml
Type: SPOArchiveState
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

### -Force

If provided, no confirmation will be asked for changing the archive state.

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

### -Identity

Specifies the full URL of the SharePoint Online site collection that needs to be renamed.

```yaml
Type: SPOSitePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -NoWait

If specified the task will return immediately after creating the archive state site job.

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
