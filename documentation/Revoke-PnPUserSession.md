---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Revoke-PnPUserSession.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Revoke-PnPUserSession
---

# Revoke-PnPUserSession

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Provides IT administrators the ability to logout a user's O365 sessions across all their devices.

## SYNTAX

### Default (Default)

```
Revoke-PnPUserSession -User <String> [-Confirm]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

User will be signed out of browser, desktop and mobile applications accessing Office 365 resources across all devices.

It is not applicable to guest users.

## EXAMPLES

### EXAMPLE 1

```powershell
Revoke-PnPUserSession -User user1@contoso.com
```

This example signs out user1 in the contoso tenancy from all devices.

## PARAMETERS

### -Confirm

Prompts you for confirmation before running the cmdlet.

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

### -User

Specifies a user name. For example, user1@contoso.com

```yaml
Type: String
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
