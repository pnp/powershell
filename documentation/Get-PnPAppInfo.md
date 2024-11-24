---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPAppInfo.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPAppInfo
---

# Get-PnPAppInfo

## SYNOPSIS

Returns information about installed apps.

## SYNTAX

### By Id

```
Get-PnPAppInfo -ProductId <Guid>
```

### By Name

```
Get-PnPAppInfo -Name <String>
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

The Get-PnPAppInfo cmdlet gets all the installed applications from an external marketplace or from the App Catalog that contain `Name` in their application names or the installed application with mentioned `ProductId`.

The returned collection of installed applications contains Product ID (GUID), Product name and Source.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPAppInfo -Name "Excel Service"
```

This will return all installed apps from the external marketplace or from the App Catalog that contain "Excel Service" in the application name.

### EXAMPLE 2

```powershell
Get-PnPAppInfo -ProductId 2646ccc3-6a2b-46ef-9273-81411cbbb60f
```

This will return the installed application info for the app with the given product id.

### EXAMPLE 3

```powershell
Get-PnPAppInfo -Name " " | Sort -Property Name
```

Returns all installed apps that have a space in the name and sorts them by name in ascending order.

## PARAMETERS

### -Name

Specifies the application's name.

```yaml
Type: String
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Name
  Position: Named
  IsRequired: true
  ValueFromPipeline: false
  ValueFromPipelineByPropertyName: false
  ValueFromRemainingArguments: false
DontShow: false
AcceptedValues: []
HelpMessage: ''
```

### -ProductId

Specifies the id of an application

```yaml
Type: Guid
DefaultValue: None
SupportsWildcards: false
ParameterValue: []
Aliases: []
ParameterSets:
- Name: By Id
  Position: 0
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
