---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPExternalUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPExternalUser
---

# Remove-PnPExternalUser

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes one or more external users from the tenant.

## SYNTAX

### Default (Default)

```
Remove-PnPExternalUser -UniqueIDs <String[]> [-Confirm]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Remove-PnPExternalUser cmdlet permanently removes a collection of external users from the tenant.

Users who are removed lose access to all tenant resources.

## EXAMPLES

### EXAMPLE 1

```powershell
$user = Get-PnPExternalUser -Filter someone@example.com
Remove-PnPExternalUser -UniqueIDs @($user.UniqueId)
```

This example removes a specific external user who has the address "someone@example.com". Organization members may still see the external user name displayed in the Shared With dialog, but the external user will not be able to sign in and will not be able to access any tenant resources.

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
  Position: 0
  IsRequired: false
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -UniqueIDs

Specifies an ID that can be used to identify an external user based on their Windows Live ID.

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
