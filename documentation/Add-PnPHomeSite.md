---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPHomeSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPHomeSite
---

# Add-PnPHomeSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Adds the home site to your tenant. The home site needs to be a communication site.

## SYNTAX

### Default (Default)

```
Add-PnPHomeSite -HomeSiteUrl <String> [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Adds a home site to the current tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome"
```

Adds a home site with the provided site collection url

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

### -HomeSiteUrl

The url of the site to set as the home site

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
- [Set up a home site for your organization](https://learn.microsoft.com/sharepoint/home-site)
- [Customize and edit the Viva Connections home experience](https://learn.microsoft.com/en-us/viva/connections/edit-viva-home)
