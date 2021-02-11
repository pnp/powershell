---
Module Name: PnP.PowerShell
title: Get-PnPOffice365CurrentServiceStatus
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPOffice365CurrentServiceStatus.html
---
 
# Get-PnPOffice365CurrentServiceStatus

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ServiceHealth.Read

Gets current service status of the Office 365 Services from the Office 365 Management API

## SYNTAX

```powershell
Get-PnPOffice365CurrentServiceStatus [-Workload <Office365Workload>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPOffice365CurrentServiceStatus
```

Retrieves the current service status of all Office 365 services

### EXAMPLE 2
```powershell
Get-PnPOffice365CurrentServiceStatus -Workload SharePoint
```

Retrieves the current service status of SharePoint Online

## PARAMETERS

### -Workload
Allows retrieval of the current service status of only one particular service. If not provided, the current service status of all services will be returned.

```yaml
Type: Office365Workload
Parameter Sets: (All)
Accepted values: Bookings, Exchange, Forms, kaizalamessagingservices, Lync, MicrosoftFlow, MicrosoftFlowM365, microsoftteams, MobileDeviceManagement, O365Client, officeonline, OneDriveForBusiness, OrgLiveID, OSDPPlatform, OSub, Planner, PowerAppsM365, PowerBIcom, SharePoint, SwayEnterprise

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

