---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Remove-PnPMicrosoft365GroupSettings.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Remove-PnPMicrosoft365GroupSettings
---

# Remove-PnPMicrosoft365GroupSettings

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API: One of Directory.ReadWrite.All , Directory.AccessAsUser.All

Removes Microsoft 365 Group settings from the tenant or the specified Microsoft 365 Group.

## SYNTAX

### Default (Default)

```
Remove-PnPMicrosoft365GroupSettings -Identity <string> -Group <Microsoft365GroupPipeBind>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to remove Microsoft 365 Group settings from the tenant or the specified group.

## EXAMPLES

### EXAMPLE 1

```powershell
Remove-PnPMicrosoft365GroupSettings -Identity "10f686b9-9deb-4ad8-ba8c-1f9b7a00a22b"
```

Removes a tenant wide Microsoft 365 Group setting based on its ID. You can get the ID of the setting using `Get-PnPMicrosoft365GroupSettings` cmdlet.

### EXAMPLE 2

```powershell
Remove-PnPMicrosoft365GroupSettings -Identity "10f686b9-9deb-4ad8-ba8c-1f9b7a00a22b" -Group $groupId
```

Removes the Microsoft 365 Group setting with Id from the specified group. You can get the ID of the setting using `Get-PnPMicrosoft365GroupSettings -Group` cmdlet.

## PARAMETERS

### -Group

The Identity of the Microsoft 365 Group

```yaml
Type: Microsoft365GroupPipeBind
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: (All)
  Position: Named
  IsRequired: false
  ValueFromPipeline: true
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -Identity

The Identity of the Microsoft 365 Group setting

```yaml
Type: string
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
- [Microsoft Graph documentation](https://learn.microsoft.com/graph/api/groupsetting-delete)
