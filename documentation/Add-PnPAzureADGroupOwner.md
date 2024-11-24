---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Add-PnPAzureADGroupOwner.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Add-PnPAzureADGroupOwner
---

# Add-PnPAzureADGroupOwner

## SYNOPSIS

**Required Permissions**

  *  Microsoft Graph API: All of Group.ReadWrite.All, User.ReadWrite.All

Adds users to the owners of an Azure Active Directory group. This can be a security or Microsoft 365 group. Distribution lists are not currently supported by Graph API.

## SYNTAX

### Default (Default)

```
Add-PnPAzureADGroupOwner -Identity <AzureADGroupPipeBind> -Users <String[]> [-RemoveExisting]
 [-Connection <PnPConnection>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to add users to owners of an Azure Active Directory Group. This can be a security, distribution or Microsoft 365 group. By specifying `-RemoveExisting` option it is possible to first clear the group of all existing members.

## EXAMPLES

### EXAMPLE 1

```powershell
Add-PnPAzureADGroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com"
```

Adds the provided two users as additional owners to the Azure Active Directory group named "Project Team".

### EXAMPLE 2

```powershell
Add-PnPAzureADGroupOwner -Identity "Project Team" -Users "john@contoso.onmicrosoft.com","jane@contoso.onmicrosoft.com" -RemoveExisting
```

Sets the provided two users as the only owners of the Azure Active Directory group named "Project Team" by removing any current existing members first.

### EXAMPLE 3

```powershell
Add-PnPAzureADGroupOwner -Identity "Project Team" -Users "125eaa87-7b54-41fd-b30f-2adfa68c4afe"
```

Sets the provided security group as owner of the Azure Active Directory group name "Project Team".

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

The Identity of the Azure Active Directory group to add owners to.

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

### -RemoveExisting

If provided, all existing members will be removed and only those provided through Users will become members.

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

### -Users

The UPN(s) of the user(s) to add to the Azure Active Directory group as a member.

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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-post-members)
