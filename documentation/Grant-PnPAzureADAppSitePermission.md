---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Grant-PnPAzureADAppSitePermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Grant-PnPAzureADAppSitePermission
---

# Grant-PnPAzureADAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All

Adds permissions for a given Azure Active Directory application registration.

## SYNTAX

### Default (Default)

```
Grant-PnPAzureADAppSitePermission -AppId <Guid> -DisplayName <String>
 -Permissions <Read|Write|Manage|FullControl> [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet adds permissions for a given Azure Active Directory application registration in a site collection. It is used in conjunction with the Azure Active Directory SharePoint application permission Sites.Selected.

## EXAMPLES

### EXAMPLE 1

```powershell
Grant-PnPAzureADAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions Read
```

Adds permissions for the Azure Active Directory application registration with the specific application id and sets the rights to 'Read' access for the currently connected site collection.

### EXAMPLE 2

```powershell
Grant-PnPAzureADAppSitePermission -AppId "aa37b89e-75a7-47e3-bdb6-b763851c61b6" -DisplayName "TestApp" -Permissions FullControl -Site https://contoso.sharepoint.com/sites/projects
```

Adds permissions for the Azure Active Directory application registration with the specific application id and sets the rights to 'FullControl' access for the site collection at the provided URL.

## PARAMETERS

### -AppId

Specify the AppId of the Azure Active Directory application registration to grant permission for.

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

### -DisplayName

The display name to set for the application permission you're adding. Only for visual reference purposes, does not need to match the name of the application in Azure Active Directory.

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

### -Permissions

Specifies the permissions to set for the Azure Active Directory application registration which can either be Read, Write, Manage or FullControl.

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
AcceptedValues:
- Read
- Write
- Manage
- FullControl
HelpMessage: ''
```

### -Site

Optional url of a site to set the permissions for. Defaults to the current site if not provided.

```yaml
Type: SitePipeBind
DefaultValue: Currently connected site
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
