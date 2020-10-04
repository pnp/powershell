---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpoffice365currentservicestatus
schema: 2.0.0
title: Get-PnPOffice365CurrentServiceStatus
---

# Get-PnPOffice365CurrentServiceStatus

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ServiceHealth.Read

Gets current service status of the Office 365 Services from the Office 365 Management API

## SYNTAX

```
Get-PnPOffice365CurrentServiceStatus [-Workload <Office365Workload>] [-ByPassPermissionCheck]
 [<CommonParameters>]
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

### -ByPassPermissionCheck
Allows the check for required permissions in the access token to be bypassed when set to $true

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Workload
Allows retrieval of the current service status of only one particular service. If not provided, the current service status of all services will be returned.

```yaml
Type: Office365Workload
Parameter Sets: (All)
Aliases:
Accepted values: Bookings, Exchange, Forms, kaizalamessagingservices, Lync, MicrosoftFlow, MicrosoftFlowM365, microsoftteams, MobileDeviceManagement, O365Client, officeonline, OneDriveForBusiness, OrgLiveID, OSDPPlatform, OSub, Planner, PowerAppsM365, PowerBIcom, SharePoint, SwayEnterprise

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)