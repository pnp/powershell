---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Revoke-PnPSiteDesignRights.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Revoke-PnPSiteDesignRights
---

# Revoke-PnPSiteDesignRights

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Revokes the specified principals rights to use the site design.

## SYNTAX

### Default (Default)

```
Revoke-PnPSiteDesignRights [-Identity] <TenantSiteDesignPipeBind> -Principals <String[]>
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Revokes the rights to use the site design for the specified users.

## EXAMPLES

### EXAMPLE 1

```powershell
Revoke-PnPSiteDesignRights -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd -Principals "myuser@mydomain.com","myotheruser@mydomain.com"
```

Revokes rights of the specified principals on the specified site design.

### EXAMPLE 2

```powershell
Get-PnPSiteDesign -Identity 5c73382d-9643-4aa0-9160-d0cba35e40fd | Revoke-PnPSiteDesignRights -Principals "myuser@mydomain.com","myotheruser@mydomain.com"
```

Revokes rights of the specified principals on the specified site design.

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

The site design to use.

```yaml
Type: TenantSiteDesignPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: 0
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Principals

One or more principals to revoke.

```yaml
Type: String[]
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
