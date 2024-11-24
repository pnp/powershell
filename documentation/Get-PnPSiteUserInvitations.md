---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPSiteUserInvitations.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPSiteUserInvitations
---

# Get-PnPSiteUserInvitations

## SYNOPSIS

**Required Permissions**

* SharePoint: Access to the SharePoint Tenant Administration site

Searches against all stored sharing links and retrieves the email invites

## SYNTAX

### Default (Default)

```
Get-PnPSiteUserInvitations [[-Site] <SitePipeBind>] -EmailAddress <string>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION



## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPSiteUserInvitations -Site "https://contoso.sharepoint.com/sites/ContosoWeb1/" -EmailAddress someone@example.com
```

This example retrieves email invites stored in the ContosoWeb1 site to the user with email address someone@example.com.

## PARAMETERS

### -EmailAddress

Email Address of the user.

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
Type: SitePipeBind
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
