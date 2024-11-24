---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPAzureADAppSitePermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPAzureADAppSitePermission
---

# Set-PnPAzureADAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All

Updates permissions for a given Azure Active Directory application registration.

## SYNTAX

### Default (Default)

```
Set-PnPAzureADAppSitePermission -PermissionId <String> -Permissions <Read|Write|Manage|FullControl>
 [-Site <SitePipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet updates permissions for a given Azure Active Directory application registration in a site collection. It is used in conjunction with the Azure Active Directory SharePoint application permission Sites.Selected.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPAzureADAppSitePermission -PermissionId ABSDFefsdfef33fsdFSvsadf3e3fsdaffsa -Permissions Read
```

Updates the Azure Active Directory application registration with the specific permission id and sets the rights to 'Read' access for the currently connected site collection.

### EXAMPLE 2

```powershell
Set-PnPAzureADAppSitePermission -PermissionId ABSDFefsdfef33fsdFSvsadf3e3fsdaffsa -Permissions FullControl -Site https://contoso.microsoft.com/sites/projects
```

Updates the Azure Active Directory application registration with the specific permission id and sets the rights to 'FullControl' access for the site collection at the provided URL.

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

### -PermissionId

The permission with the specified id will be updated. Use [Get-PnPAzureADAppSitePermission](Get-PnPAzureADAppSitePermission.md) to discover currently set permissions which can be updated.

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
