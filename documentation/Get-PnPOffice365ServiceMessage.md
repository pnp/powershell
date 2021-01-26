---
Module Name: PnP.PowerShell
title: Get-PnPOffice365ServiceMessage
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPOffice365ServiceMessage.html
---
 
# Get-PnPOffice365ServiceMessage

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ServiceHealth.Read

Gets the service messages regarding services in Office 365 from the Office 365 Management API

## SYNTAX

```powershell
Get-PnPOffice365ServiceMessage [-Workload <Office365Workload>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPOffice365ServiceMessage
```

Retrieves the service messages regarding services in Office 365

## PARAMETERS

### -Workload
Allows retrieval of the service messages for only one particular service. If not provided, the service messages of all services will be returned.

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

