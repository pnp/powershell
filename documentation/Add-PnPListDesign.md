---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPListDesign.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPListDesign
---

# Add-PnPListDesign

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Creates a new List Design on the current tenant

## SYNTAX

### By SiteScript Instance (Default)

```
Add-PnPListDesign -Title <String> -SiteScript <TenantSiteScriptPipeBind> [-Description <String>]
 [-ListColor<TenantListDesignIcon>] [-ListIcon <TenantListDesignColor>] [-ThumbnailUrl <String>]
 [-Connection <PnPConnection>]
```

### By SiteScript Ids

```
Add-PnPListDesign -Title <String> -SiteScriptIds <Guid[]> [-Description <String>]
 [-ListColor<TenantListDesignIcon>] [-ListIcon <TenantListDesignColor>] [-ThumbnailUrl <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add new List Design to tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPListDesign -Title "My Custom List" -SiteScriptIds "e84dcb46-3ab9-4456-a136-66fc6ae3d3c5"
```

Adds a new List Design, with the specified title and description. When applied it will run the scripts as referenced by the IDs. Use Get-PnPSiteScript to receive site Scripts.

### EXAMPLE 2

```powershell
Add-PnPListDesign -Title "My Company Design" -SiteScriptIds "6def687f-0e08-4f1e-999c-791f3af9a600" -Description "My description" -ListColor Orange -ListIcon BullseyeTarget -ThumbnailUrl "https://contoso.sharepoint.com/SiteAssets/site-thumbnail.png"
```

Adds a new List Design, with the specified title, description and list color, list icon and thumbnail to be shown in the template picker. When applied it will run the scripts as referenced by the IDs. Use Get-PnPSiteScript to receive Site Scripts.

### EXAMPLE 3

```powershell
Get-PnPSiteScript -Identity "My List Script" | Add-PnPListDesign -Title "My Custom List"
```

Adds a new List Design, with the specified title based on a site script with the title "My List Script"

### EXAMPLE 4

```powershell
Get-PnPList -Identity "My List" | Get-PnPSiteScriptFromList | Add-PnPSiteScript -Title "My List Script" | Add-PnPListDesign -Title "My List"
```

Adds a new List Design and site script based on a list with the title "My List"

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

### -Description

The description of the list design

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

### -ListColor

The list color from the create list experience.

```yaml
Type: TenantListDesignColor
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

### -ListIcon

The list icon from the create list experience.

```yaml
Type: TenantListDesignIcon
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

### -SiteScript

An instance, id or title of a site script to use for the list design

```yaml
Type: TenantSiteScriptPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By SiteScript Instance
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -SiteScriptIds

An array of guids of site scripts to use for the list design

```yaml
Type: Guid[]
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By SiteScript Ids
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ThumbnailUrl

The full URL of a thumbnail image, i.e. https://contoso.sharepoint/siteassets/image.png. If none is specified, SharePoint uses a generic image. This is the image that will be shown in the "From your organization" section of the "Create" List screen.

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

### -Title

The title of the list design

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
