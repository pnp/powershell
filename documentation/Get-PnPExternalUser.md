---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPExternalUser.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPExternalUser
---

# Get-PnPExternalUser

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Returns external users in the tenant.

## SYNTAX

### Default (Default)

```
Get-PnPExternalUser
 [-Position <Integer>] [-PageSize <Integer>] [-Filter <String>]
 [-SortOrder <SortOrder>] [-SiteUrl <String>]
 [-ShowOnlyUsersWithAcceptingAccountNotMatchInvitedAccount <Boolean>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Get-PnPExternalUser cmdlet returns external users that are located in the tenant based on specified criteria.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPExternalUser -Position 0 -PageSize 2
```

Returns the first two external users in the collection.

### EXAMPLE 2

```powershell
Get-PnPExternalUser -Position 2 -PageSize 2
```

Returns two external users from the third page of the collection.

## PARAMETERS

### -Filter

Prompts you for confirmation before running the cmdlet.

```yaml
Type: String
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

### -PageSize

Specifies the maximum number of users to be returned in the collection.

```yaml
Type: Integer
DefaultValue: 1
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

### -Position

Use to specify the zero-based index of the position in the sorted collection of the first result to be returned.

```yaml
Type: Integer
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

### -ShowOnlyUsersWithAcceptingAccountNotMatchInvitedAccount

Shows users who have accepted an invite but not using the account the invite was sent to.

```yaml
Type: Boolean
DefaultValue: False
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

### -SiteUrl

Specifies the site to retrieve external users for.

If no site is specified, the external users for all sites are returned.

```yaml
Type: String
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

### -SortOrder

Specifies the sort results in Ascending or Descending order on the Email property should occur.

```yaml
Type: SortOrder
DefaultValue: Ascending
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
