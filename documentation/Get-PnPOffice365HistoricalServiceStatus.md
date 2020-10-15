---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpoffice365historicalservicestatus
schema: 2.0.0
title: Get-PnPOffice365HistoricalServiceStatus
---

# Get-PnPOffice365HistoricalServiceStatus

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ServiceHealth.Read

Gets the historical service status of the Office 365 Services of the last 7 days from the Office 365 Management API

## SYNTAX

```powershell
Get-PnPOffice365HistoricalServiceStatus [-Workload <Office365Workload>] [-ByPassPermissionCheck]
 [<CommonParameters>]
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

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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