---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPKnowledgeHubSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPKnowledgeHubSite
---

# Set-PnPKnowledgeHubSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the Knowledge Hub Site for your tenant.

## SYNTAX

### Default (Default)

```
Set-PnPKnowledgeHubSite -KnowledgeHubSiteUrl <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to set Knowledge Hub Site of the current tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPKnowledgeHubSite -KnowledgeHubSiteUrl "https://yoursite.sharepoint.com/sites/knowledge"
```

Sets the Knowledge Hub Site for your tenant.

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

### -KnowledgeHubSiteUrl

Specifies the URL of the site to be set as the Knowledge Hub Site.

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
