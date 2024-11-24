---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAzureADAppPermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAzureADAppPermission
---

# Get-PnPAzureADAppPermission

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Application.Read.All

Returns app permissions for Microsoft SharePoint and Microsoft Graph.

## SYNTAX

### Default (Default)

```
Get-PnPAzureADAppPermission [-Identity <AzureADAppPipeBind>] [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet returns the appid, displayname and permissions set for Microsoft SharePoint and the Microsoft Graph APIs.

## EXAMPLES

### Example 1

```powershell
Get-PnPAzureADAppPermission
```

Returns all apps with all permissions.

### Example 2

```powershell
Get-PnPAzureADAppPermission -Identity MyApp
```

Returns permissions for the specified app.

### Example 2

```powershell
Get-PnPAzureADAppPermission -Identity 93a9772d-d0af-4ed8-9821-17282b64690e
```

Returns permissions for the specified app.

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

Specify the display name, id or app id.

```yaml
Type: AzureADAppPipeBind
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
