---
Module Name: PnP.PowerShell
title: Get-PnPOffice365HistoricalServiceStatus
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPOffice365HistoricalServiceStatus.html
---
 
# Get-PnPOffice365HistoricalServiceStatus

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ServiceHealth.Read

Gets the historical service status of the Office 365 Services of the last 7 days from the Office 365 Management API

## SYNTAX

```powershell
Get-PnPOffice365HistoricalServiceStatus [-Workload <Office365Workload>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPOffice365HistoricalServiceStatus
```

Retrieves the historical service status of all Office 365 services

### EXAMPLE 2
```powershell
Get-PnPOffice365HistoricalServiceStatus -Workload SharePoint
```

Retrieves the historical service status of SharePoint Online

## PARAMETERS

### -Workload
Allows retrieval of the historical service status of only one particular service. If not provided, the historical service status of all services will be returned.

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

