---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Set-PnPSiteSensitivityLabel.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Set-PnPSiteSensitivityLabel
---

# Set-PnPSiteSensitivityLabel

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : Delegate token of Group.ReadWrite.All, Directory.ReadWrite.All (see description below)

Allows placing a Microsoft Purview sensitivity label on the current site

## SYNTAX

### Default (Default)

```
Set-PnPSiteSensitivityLabel -Identity <String> [-Connection <PnPConnection>] [-Verbose]
 [<CommonParameters>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

This cmdlet allows for setting a Microsoft Purview sensitivity label on the currently connected to site. If the site has a Microsoft 365 Group behind it, the label will be placed on the Microsoft 365 Group and will require either Group.ReadWrite.All or Directory.ReadWrite.All delegate permissions on Microsoft Graph. This currently cannot be done using App Only permissions due to a limitation in Microsoft Graph. If it does not have a Microsoft 365 Group behind it, it will set the label on the SharePoint Online site and will not require Microsoft Graph permissions and will work with both delegate as well as app only logins. If you're looking to set a sensitivity label on a Microsoft 365 Group backed site in an App Only context, you can use [Set-PnPTenantSite -SensitivityLabel](Set-PnPTenantSite.md#-sensitivitylabel) instead to do so.

It may take up to a few minutes for a change to the sensitivity label to become visible in SharePoint Online and Entra ID / Azure Active Directory.

Use [Get-PnPAvailableSensitivityLabel](Get-PnPAvailableSensitivityLabel.md) to get an overview of the available Microsoft Purview sensitivity labels on the tenant.

For the classic classification labels, use [Set-PnPSiteClassification](Set-PnPSiteClassification.md) instead.

## EXAMPLES

### EXAMPLE 1

```powershell
Set-PnPSiteSensitivityLabel -Identity "Top Secret"
```

Sets the Microsoft Purview sensitivity label with the name "Top Secret" on the current site

### EXAMPLE 2

```powershell
Set-PnPSiteSensitivityLabel -Identity a1888df2-84c2-4379-8d53-7091dd630ca7
```

Sets the Microsoft Purview sensitivity label with the Id a1888df2-84c2-4379-8d53-7091dd630ca7 on the current site

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

Id or name of the Microsoft Purview sensitivity label to apply

```yaml
Type: String
DefaultValue: True
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: true
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: true
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Verbose

When provided, additional debug statements will be shown while going through the execution of this cmdlet.

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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-update?view=graph-rest-beta&tabs=http#example-2-apply-sensitivity-label-to-a-microsoft-365-group)
