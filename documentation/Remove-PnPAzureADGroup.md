---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADGroup.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPAzureADGroup
---

# Remove-PnPAzureADGroup

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: Group.ReadWrite.All

Removes one Azure Active Directory group. This can be a security or Microsoft 365 group. Distribution lists are not currently supported by Graph API.

## SYNTAX

### Default (Default)

```
Remove-PnPAzureADGroup -Identity <AzureADGroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove Azure Active Directory group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPAzureADGroup -Identity $groupId
```

Removes an Azure Active Directory group based on its ID

### EXAMPLE 2

```powershell
Remove-PnPAzureADGroup -Identity $group
```

Removes the provided Azure Active Directory group

## PARAMETERS

### -Identity

The Identity of the Azure Active Directory group

```yaml
Type: AzureADGroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
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
