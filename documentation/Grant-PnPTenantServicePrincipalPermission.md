---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Grant-PnPTenantServicePrincipalPermission.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Grant-PnPTenantServicePrincipalPermission
---

# Grant-PnPTenantServicePrincipalPermission

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site
* Microsoft Graph API : Directory.ReadWrite.All

Explicitly grants a specified permission to the "SharePoint Online Client Extensibility Web Application Principal" service principal for SPFx solutions.

## SYNTAX

### Default (Default)

```
Grant-PnPTenantServicePrincipalPermission -Scope <String> [-Resource <String>]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to grant a specified permission o the "SharePoint Online Client Extensibility Web Application Principal" service principal for SPFx solutions.

## EXAMPLES

### EXAMPLE 1

```powershell
Grant-PnPTenantServicePrincipalPermission -Scope "Group.Read.All"
```

This will explicitly grant the Group.Read.All permission on the Microsoft Graph resource

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

### -Resource

The resource to grant the permission for. Defaults to "Microsoft Graph"

```yaml
Type: String
DefaultValue: Microsoft Graph
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

### -Scope

The scope to grant the permission for

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
