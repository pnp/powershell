---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteScriptFromList.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteScriptFromList
---

# Get-PnPSiteScriptFromList

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Generates a Site Script from an existing list

## SYNTAX

### By List (Default)

```
Get-PnPSiteScriptFromList -List <ListPipeBind> [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

### By Url

```
Get-PnPSiteScriptFromList -Url <String> [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows a Site Script to be generated off of an existing list on your tenant. The script will return the JSON syntax with the definition of the list, including fields, views, content types, and some of the list settings. The script can then be used with [Add-PnPSiteScript](Add-PnPSiteScript.md) and [Add-PnPListDesign](Add-PnPListDesign.md) to allow lists with the same configuration as the original list to be created by end users.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteScriptFromList -List "MyList"
```

Returns the generated Site Script JSON from the list "MyList" in the currently connected to site

### EXAMPLE 2

```powershell
Get-PnPList -Identity "MyList" | Get-PnPSiteScriptFromList | Add-PnPSiteScript -Title "MyListScript" | Add-PnPListDesign -Title "MyListDesign"
```

Returns the generated Site Script JSON from the list "MyList" in the currently connected to site and registers it as a new Site Script with the title "MyListScript" and uses that Site Script to register a new List Design with the title "MyListDesign"

### EXAMPLE 3

```powershell
Get-PnPSiteScriptFromList -Url "https://contoso.sharepoint.com/sites/teamsite/lists/MyList"
```

Returns the generated Site Script JSON from the list "MyList" at the provided Url

### EXAMPLE 4

```powershell
Get-PnPSiteScriptFromList -Url "https://contoso.sharepoint.com/sites/teamsite/Shared Documents"
```

Returns the generated Site Script JSON from the default document library at the provided Url

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- cf
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

### -List

Specifies an instance, Id or, title of the list to generate a Site Script from

```yaml
Type: ListPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By List
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Url

Specifies the full URL of the list to generate a Site Script from. I.e. https://contoso.sharepoint.com/sites/teamsite/lists/MyList

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
  ValueFromPipeline: true
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
