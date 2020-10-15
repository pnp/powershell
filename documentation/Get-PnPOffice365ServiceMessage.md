---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpoffice365servicemessage
schema: 2.0.0
title: Get-PnPOffice365ServiceMessage
---

# Get-PnPOffice365ServiceMessage

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ServiceHealth.Read

Gets the service messages regarding services in Office 365 from the Office 365 Management API

## SYNTAX

```powershell
Get-PnPOffice365ServiceMessage [-Workload <Office365Workload>] [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPOffice365ServiceMessage
```

Retrieves the service messages regarding services in Office 365

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