---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPUserInfo.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPUserInfo
---

# Remove-PnPUserInfo

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Removes a user from the user information list of a specific site collection.

## SYNTAX

### Default (Default)

```
Remove-PnPUserInfo -LoginName <String> [-Site <String>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes user information from the site user information list.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPUserInfo -LoginName user@domain.com -Site "https://yoursite.sharepoint.com/sites/team"
```

This removes a user who has the e-mail address user@domain.com from the user information list of https://contoso.sharepoint.com/sites/team site collection.

## PARAMETERS

### -LoginName

Specifies the login name of the user to remove.

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

### -Site

Specifies the URL of the site collection.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
