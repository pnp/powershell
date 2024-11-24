---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPHubToHubAssociation.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPHubToHubAssociation
---

# Remove-PnPHubToHubAssociation

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes the selected hub site from its parent hub.

## SYNTAX

### By Id

```
Remove-PnPHubToHubAssociation -HubSiteId <Guid>
```

### By Url

```
Remove-PnPHubToHubAssociation -HubSiteUrl <string>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Use this cmdlet to remove the selected hub site from its parent hub.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPHubToHubAssociation -HubSiteId 6638bd4c-d88d-447c-9eb2-c84f28ba8b15
```

This example removes the hubsite with id 6638bd4c-d88d-447c-9eb2-c84f28ba8b15 from its parent hub.

### EXAMPLE 2

```powershell
Remove-PnPHubToHubAssociation -HubSiteUrl "https://yourtenant.sharepoint.com/sites/sourcehub"
```
This example removes the hubsite with id https://yourtenant.sharepoint.com/sites/sourcehub from its parent hub.

## PARAMETERS

### -HubSiteId

Id of the hubsite to remove from its parent.

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -HubSiteUrl

Url of the hubsite to remove from its parent.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Url
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
