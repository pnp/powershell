---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADAppSitePermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureADAppSitePermission
---

# Get-PnPAzureADAppSitePermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Sites.FullControl.All

Returns Azure AD App permissions for a site.

## SYNTAX

### All Permissions

```
Get-PnPAzureADAppSitePermission [-PermissionId <String>] [-Site <SitePipeBind>]
 [-Connection <PnPConnection>]
```

### By Permission Id

```
Get-PnPAzureADAppSitePermission -PermissionId <String> [-Site <SitePipeBind>]
 [-Connection <PnPConnection>]
```

### By App Display Name or App Id

```
Get-PnPAzureADAppSitePermission -AppIdentity <String> [-Site <SitePipeBind>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns app permissions for either the current or a given site.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAzureADAppSitePermission
```

Returns the apps that have permissions for the currently connected site. Note that if PermissionId is not specified then the Roles property is not populated. This is a current API limitation.

### EXAMPLE 2

```powershell
Get-PnPAzureADAppSitePermission -Site https://contoso.sharepoint.com/sites/projects
```

Returns the apps that have permissions for the site specified. Note that you are required to have the SharePoint Administrator role in your tenant to be able to use this command.

### EXAMPLE 3

```powershell
Get-PnPAzureADAppSitePermission -PermissionId TowaS50fG1zLnNwLmV4dHwxYxNmI0OTI1
```

Returns the specific app permission details for the given permission id for the current site.

### EXAMPLE 4

```powershell
Get-PnPAzureADAppSitePermission -AppIdentity "Test App"
```

Returns the specific app permission details for the app with the provided name.

### EXAMPLE 5

```powershell
Get-PnPAzureADAppSitePermission -AppIdentity "14effc36-dc8b-4f68-8919-f6beb7d847b3"
```

Returns the specific app permission details for the app with the provided Id.

## PARAMETERS

### -AppIdentity

You can specify either the Display Name or the AppId to specifically retrieve the permission for.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By App Display Name or App Id
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

### -PermissionId

If specified the permission with that id specified will be retrieved.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Permission Id
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Site

Optional url of a site to retrieve the permissions for. Defaults to the current site.

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
