---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPUserProfile.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPUserProfile
---

# Remove-PnPUserProfile

## SYNOPSIS

Removes a SharePoint User Profile from the tenant.

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

## SYNTAX

### Default (Default)

```
Remove-PnPUserProfile -LoginName <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Removes SharePoint User Profile data from the tenant.

> [!NOTE]
> The User must first be deleted from AAD before the user profile can be deleted. You can use the Azure AD cmdlet Remove-AzureADUser for this action.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPUserProfile -LoginName user@domain.com
```

This removes user profile data with the email address user@domain.com.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
