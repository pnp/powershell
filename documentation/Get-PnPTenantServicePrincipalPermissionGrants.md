---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPTenantServicePrincipalPermissionGrants.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPTenantServicePrincipalPermissionGrants
---

# Get-PnPTenantServicePrincipalPermissionGrants

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Gets the collection of permission grants for the "SharePoint Online Client" service principal

## SYNTAX

### Default (Default)

```
Get-PnPTenantServicePrincipalPermissionGrants [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Gets the collection of permission grants for the "SharePoint Online Client" service principal.

A permission grant contains the following properties:

* ClientId: The objectId of the service principal granted consent to impersonate the user when accessing the resource(represented by the resourceId).
* ConsentType: Whether consent was provided by the administrator on behalf of the organization or whether consent was provided by an individual.The possible values are "AllPrincipals" or "Principal".
* ObjectId: The unique identifier for the permission grant.
* Resource: The resource to which access has been granted (Coming soon)
* ResourceId: The objectId of the resource service principal to which access has been granted.
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
