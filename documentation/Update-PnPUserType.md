---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Update-PnPUserType.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Update-PnPUserType
---

# Update-PnPUserType

## SYNOPSIS

Updates a user's UserType across all SharePoint Online sites.

## SYNTAX

### Default (Default)

```
Update-PnPUserType -LoginName <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet retrieves the UserType value of the specified user and updates the UserType across all SharePoint Online sites in the SharePoint Online tenant. This can be used, for example, to convert a Guest user to a standard (Member) user if the user's UserType was previously updated in Azure AD.

## EXAMPLES

### EXAMPLE 1

```powershell
Update-PnPUserType -LoginName jdoe@contoso.com
```
Updates the jdoe@contoso.com's UserType on all SharePoint Online sites in the tenant based on the UserType value in Azure AD.

## PARAMETERS

### -LoginName

The login name of the target user to update across SharePoint Online.

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
