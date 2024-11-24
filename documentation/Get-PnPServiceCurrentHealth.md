---
applicable: SharePoint Online
document type: cmdlet
external help file: PnP.PowerShell.dll-Help.xml
HelpUri: https://pnp.github.io/powershell/cmdlets/Get-PnPServiceCurrentHealth.html
Module Name: PnP.PowerShell
PlatyPS schema version: 2024-05-01
title: Get-PnPServiceCurrentHealth
---

# Get-PnPServiceCurrentHealth

## SYNOPSIS

**Required Permissions**

  * Microsoft Graph API : ServiceHealth.Read.All

Gets current service status of the Office 365 Services from the Microsoft Graph API

## SYNTAX

### Default (Default)

```
Get-PnPServiceCurrentHealth [-Identity <Office365Workload>]
```

## ALIASES

This cmdlet has no aliases.

## DESCRIPTION

Allows to retrieve current service status of the Office 365 Services from the Microsoft Graph API.

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PnPServiceCurrentHealth
```

Retrieves the current service status of all Office 365 services

### EXAMPLE 2

```powershell
Get-PnPServiceCurrentHealth -Identity "SharePoint Online"
```

Retrieves the current service status of SharePoint Online

## PARAMETERS

### -Identity

Allows retrieval of the current service status of only one particular service. If not provided, the current service status of all services will be returned. Note that you need to use the full name of the service, not the shortened Id.

```yaml
Type: Identity
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
AcceptedValues:
- Exchange Online
- Identity Service
- Microsoft 365 suite
- Skype for Business
- SharePoint Online
- Dynamics 365 Apps
- Azure Information Protection
- Mobile Device Management for Office 365
- Planner
- Sway
- Power BI
- OneDrive for Business
- Microsoft Teams
- Microsoft Kaizala
- Microsoft Bookings
- Office for the web
- Microsoft 365 Apps
- Power Apps
- Power Apps in Microsoft 365
- Microsoft Power Automate
- Microsoft Power Automate in Microsoft 365
- Microsoft Forms
- Microsoft Stream
HelpMessage: ''
```

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

- [Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)
