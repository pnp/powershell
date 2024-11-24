---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPHomeSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPHomeSite
---

# Set-PnPHomeSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Sets the home site for your tenant. The home site needs to be a communication site.

## SYNTAX

### Default (Default)

```
Set-PnPHomeSite -HomeSiteUrl <String> [-VivaConnectionsDefaultStart <SwitchParameter>]
 [-Force <SwitchParameter>] [-DraftMode <SwitchParameter>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to set the home site of the current tenant.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome"
```

Sets the home site to the provided site collection url.

### EXAMPLE 2

```powershell
Set-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome" -VivaConnectionsDefaultStart:$true
```

Sets the home site to the provided site collection url and keeps the Viva Connections landing experience to the SharePoint home site.

### EXAMPLE 3

```powershell
Set-PnPHomeSite -HomeSiteUrl "https://yourtenant.sharepoint.com/sites/myhome" -VivaConnectionsDefaultStart:$true -DraftMode:$true
```

Sets the home site to the provided site collection url and keeps the Viva Connections landing experience to the SharePoint home site but it will be in draft mode.

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

### -DraftMode

When set to $true, the DraftMode parameter will keep the Viva Connections landing experience to the SharePoint home site in draft mode.

```yaml
Type: SwitchParameter
DefaultValue: true
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

### -Force

Use the -Force flag to bypass the confirmation question.

```yaml
Type: SwitchParameter
DefaultValue: true
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

The url of the site to set as the home site.

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

### -VivaConnectionsDefaultStart

When set to $true, the VivaConnectionsDefaultStart parameter will keep the Viva Connections landing experience to the SharePoint home site. If set to $false the Viva Connections home experience will be used.

```yaml
Type: SwitchParameter
DefaultValue: true
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
- [Set up a home site for your organization](https://learn.microsoft.com/sharepoint/home-site)
- [Customize and edit the Viva Connections home experience](https://learn.microsoft.com/en-us/viva/connections/edit-viva-home)
