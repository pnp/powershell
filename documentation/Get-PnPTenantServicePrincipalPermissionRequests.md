---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantServicePrincipalPermissionRequests.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantServicePrincipalPermissionRequests
---

# Get-PnPTenantServicePrincipalPermissionRequests

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets the collection of permission requests for the "SharePoint Online Client" service principal

## SYNTAX

### Default (Default)

```
Get-PnPTenantServicePrincipalPermissionRequests [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Gets the collection of permission requests for the "SharePoint Online Client" service principal.

Permission request object

A permission request contains the following properties:

* Id: The identifier of the request.
* Resource: The resource that the application requires access to.
* Scope: The value of the scope claim that the resource application should expect in the OAuth 2.0 access token.

## EXAMPLES

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
