---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Reset-PnPMicrosoft365GroupExpiration.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Reset-PnPMicrosoft365GroupExpiration
---

# Reset-PnPMicrosoft365GroupExpiration

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : One of Directory.ReadWrite.All, Group.ReadWrite.All

Renews the Microsoft 365 Group by extending its expiration with the number of days defined in the group expiration policy set on the Azure Active Directory.

## SYNTAX

### Default (Default)

```
Reset-PnPMicrosoft365GroupExpiration -Identity <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to extend the Microsoft 365 Group expiration date by the number of days defined in the group expiration policy.

## EXAMPLES

### EXAMPLE 1

```powershell
Reset-PnPMicrosoft365GroupExpiration
```

Renews the Microsoft 365 Group by extending its expiration with the number of days defined in the group expiration policy set on the Azure Active Directory.

## PARAMETERS

### -Identity

The Identity of the Microsoft 365 Group.

```yaml
Type: Microsoft365GroupPipeBind
DefaultValue: ''
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

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/group-renew)
