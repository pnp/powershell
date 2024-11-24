---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPAzureADGroupMember.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPAzureADGroupMember
---

# Remove-PnPAzureADGroupMember

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All, GroupMember.ReadWrite.All

Removes members from a particular Azure Active Directory group. This can be a security, distribution or Microsoft 365 group.

## SYNTAX

### Default (Default)

```
Remove-PnPAzureADGroupMember -Identity <AzureADGroupPipeBind> -Users <String[]>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove members from Azure Active Directory group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPAzureADGroupMember -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Removes the provided two users as members from the Azure Active Directory group named "Project Team"

## PARAMETERS

### -Identity

The Identity of the Azure Active Directory group to remove members from

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

### -Users

The UPN(s) of the user(s) to remove as members from the Azure Active Directory group

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-delete-members)
