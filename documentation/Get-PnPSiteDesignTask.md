---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteDesignTask.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteDesignTask
---

# Get-PnPSiteDesignTask

## SYNOPSIS

Used to retrieve a scheduled site design script. It takes the ID of the scheduled site design task and the URL for the site where the site design is scheduled to be applied.

## SYNTAX

### Default (Default)

```
Get-PnPSiteDesignTask [-Identity <TenantSiteDesignTaskPipeBind>] [-WebUrl <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve a scheduled site design script.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteDesignTask -Identity 501z8c32-4147-44d4-8607-26c2f67cae82
```

This example retrieves a site design task given the provided site design task id

### EXAMPLE 2

```powershell
Get-PnPSiteDesignTask
```

This example retrieves all site design tasks currently scheduled on the current site

### EXAMPLE 3

```powershell
Get-PnPSiteDesignTask -WebUrl "https://contoso.sharepoint.com/sites/project"
```

This example retrieves all site design tasks currently scheduled on the provided site

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

### -Identity

The ID of the site design task to retrieve.

```yaml
Type: TenantSiteDesignTaskPipeBind
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

### -WebUrl

The URL of the site collection where the site design will be applied. If not specified the site design tasks will be returned for the site you connected to with Connect-PnPOnline.

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
