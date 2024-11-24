---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPSiteDesignTask.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPSiteDesignTask
---

# Add-PnPSiteDesignTask

## SYNOPSIS

This command is used to apply a published site design to a specified site collection target. It schedules the operation, allowing for the application of larger site scripts (Invoke-PnPSiteDesign is limited to 30 actions and subactions).
This command is intended to replace Invoke-PnPSiteDesign and is useful when you need to apply a large number of actions or multiple site scripts.

## SYNTAX

### Default (Default)

```
Add-PnPSiteDesignTask -SiteDesignId <Guid> [-WebUrl <String>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to apply a site design to a specified site collection. This command is intended to be used when you need to apply a large number of actions or multiple site scripts.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPSiteDesignTask -SiteDesignId 501z8c32-4147-44d4-8607-26c2f67cae82
```

This example applies a site design to the currently connected site. Executing the commands will schedule the site design to be queued and run against the designated site collection.

### EXAMPLE 2

```powershell
Add-PnPSiteDesignTask -SiteDesignId 501z8c32-4147-44d4-8607-26c2f67cae82 -WebUrl "https://contoso.sharepoint.com/sites/project"
```

This example applies a site design to the designated site. Executing the commands will schedule the site design to be queued and run against the designated site collection.

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

### -SiteDesignId

The ID of the site design to apply.

```yaml
Type: Guid
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

### -WebUrl

The URL of the site collection where the site design will be applied. If not specified the design will be applied to the site you connected to with Connect-PnPOnline.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
