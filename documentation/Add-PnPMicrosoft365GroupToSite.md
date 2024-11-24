---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPMicrosoft365GroupToSite.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPMicrosoft365GroupToSite
---

# Add-PnPMicrosoft365GroupToSite

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Groupifies a classic team site by creating a Microsoft 365 group for it and connecting the site with the newly created group.

## SYNTAX

### Default (Default)

```
Add-PnPMicrosoft365GroupToSite -Url <String> -Alias <String> -DisplayName <String>
 [-Description <String>] [-Classification <String>] [-IsPublic] [-KeepOldHomePage]
 [-HubSiteId <Guid>] [-Owners <String[]>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This command allows you to add a Microsoft 365 Unified group to an existing classic site collection, also known as groupifying.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPMicrosoft365GroupToSite -Url "https://contoso.sharepoint.com/sites/FinanceTeamsite" -Alias "FinanceTeamsite" -DisplayName "My finance team site group"
```

This will groupify the FinanceTeamsite at the provided URL.

### EXAMPLE 2

```powershell
Add-PnPMicrosoft365GroupToSite -Alias "HRTeamsite" -DisplayName "My HR team site group"
```

This will groupify the currently connected site.

### EXAMPLE 3

```powershell
Add-PnPMicrosoft365GroupToSite -Url $SiteURL -Alias $GroupAlias -DisplayName $GroupName -IsPublic -KeepOldHomePage
```
This will groupify the $SiteURL site, make the Group public (default is Private) and keep the old Home page as the default homepage. The new Home.aspx is created but not set as default Homepage.

## PARAMETERS

### -Alias

Specifies the alias of the group. Cannot contain spaces.

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

### -Classification

Specifies the classification of the group.

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

The optional description of the group.

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

### -DisplayName

The display name of the group.

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

### -HubSiteId

If specified the site will be associated to the hubsite as identified by this id.

```yaml
Type: Guid
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

### -IsPublic

Specifies if the group is public. Defaults to false.

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

### -KeepOldHomePage

Specifies if the current site home page is kept. Defaults to false.

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

### -Owners

The array of the UPN values of the group's owners.

```yaml
Type: String[]
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

### -Url

Url of the site to be connected to an Microsoft 365 Group. When not provided, the site currently being connected to will be used.

```yaml
Type: String
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
