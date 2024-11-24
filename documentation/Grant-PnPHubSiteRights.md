---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Grant-PnPHubSiteRights.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Grant-PnPHubSiteRights
---

# Grant-PnPHubSiteRights

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Grant additional permissions to the permissions already in place to associate sites to Hub Sites for one or more specific users

## SYNTAX

### Default (Default)

```
Grant-PnPHubSiteRights [-Identity] <HubSitePipeBind> -Principals <String[]>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add additional permissions to existing once to associate sites to Hub Sites for specified users.

## EXAMPLES

### EXAMPLE 1

```powershell
Grant-PnPHubSiteRights -Identity "https://contoso.sharepoint.com/sites/hubsite" -Principals "myuser@mydomain.com","myotheruser@mydomain.com"
```

This example shows how to grant rights to myuser and myotheruser to associate their sites with the provided Hub Site

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

Specify hub site url

```yaml
Type: HubSitePipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases:
- HubSite
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Principals

Specify user(s) login name i.e user@company.com

```yaml
Type: String[]
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
