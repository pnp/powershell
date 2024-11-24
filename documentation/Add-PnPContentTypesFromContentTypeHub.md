---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPContentTypesFromContentTypeHub.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPContentTypesFromContentTypeHub
---

# Add-PnPContentTypesFromContentTypeHub

## SYNOPSIS

**Required Permissions**

  * ManageLists permission on the current site or the content type hub site.

Adds published content types from content type hub site to current site. If the content type already exists on the current site then the latest published version of the content type will be synced to the site.

## SYNTAX

### Default (Default)

```
Add-PnPContentTypesFromContentTypeHub -ContentTypes [-Site <SitePipeBind>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add content types from content type hub site to current site. In case the same content type is already present on the current site then the latest published version will be used.

## EXAMPLES

### EXAMPLE 1

```powershell
 Add-PnPContentTypesFromContentTypeHub -ContentTypes "0x0101", "0x01"
```

This will add the content types with the ids '0x0101' and '0x01' to the current site. Latest published version of these content types will be synced if they were already present in the current site.

- There's an issue with this cmdlet if you use it on private channel sites. The workaround for that is to execute the below command:
  - `Enable-PnPFeature -Identity 73ef14b1-13a9-416b-a9b5-ececa2b0604c -Scope Site -Force`

### EXAMPLE 2

```powershell
 Add-PnPContentTypesFromContentTypeHub -ContentTypes "0x010057C83E557396744783531D80144BD08D" -Site https://tenant.sharepoint.com/sites/HR
```

This will add the content type with the id '0x010057C83E557396744783531D80144BD08D' to the site with the provided URL. Latest published version of these content types will be synced if they were already present in the current site.

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

### -ContentTypes

The list of content type ids present in content type hub site that are required to be added/synced to the current site.

```yaml
Type: List<String>
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

### -Site

The site to which to add the content types coming from the hub. If omitted, it will be applied to the currently connected site.

```yaml
Type: SitePipeBind
DefaultValue: Currently connected site
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
