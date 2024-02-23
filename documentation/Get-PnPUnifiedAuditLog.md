---
Module Name: PnP.PowerShell
title: Get-PnPUnifiedAuditLog
schema: 2.0.0
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
online version: https://pnp.github.io/powershell/cmdlets/Get-PnPUnifiedAuditLog.html
---
 
# Get-PnPUnifiedAuditLog

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ActivityFeed.Read, Microsoft Office 365 Management API: ActivityFeed.ReadDlp, Microsoft Office 365 Management API: ActivityReports.Read, Microsoft Office 365 Management API: ServiceHealth.Read and Microsoft Office 365 Management API:ThreatIntelligence.Read

Gets unified audit logs from the Office 365 Management API. Requires the Azure Entra application permission 'ActivityFeed.Read', 'ActivityFeed.ReadDlp', 'ActivityReports.Read', 'ServiceHealth.Read' and 'ThreatIntelligence.Read'.


## Remarks

:::info

Before you can access audit log data, you must enable unified audit logging for your Microsoft 365 tenant. For instructions, check out the page [Turn auditing on or off](https://learn.microsoft.com/microsoft-365/compliance/audit-log-enable-disable).

:::

:::info

When running this command for the first time for a certain content type, a subscription for this content type is created. It can take up to 12 hours for the first content blobs to become available for that subscription.

:::

:::tip

Retrieving audit logs is an intensive process, especially for large or active tenants. In this case it may take some time to retrieve all audit logs. 

:::


## SYNTAX

```powershell
Get-PnPUnifiedAuditLog [-ContentType <AuditContentType>] [-StartTime <DateTime>] [-EndTime <DateTime>]
  
```

## DESCRIPTION

Allows to retrieve unified audit logs from the Office 365 Management API.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUnifiedAuditLog -ContentType SharePoint -StartTime (Get-Date -asUtc).AddDays(-2) -EndTime (Get-Date -asUtc).AddDays(-1)
```

Retrieves the audit logs of SharePoint happening between the current time yesterday and the current time the day before yesterday

## PARAMETERS

### -ContentType

Content type of logs to be retrieved, should be one of the following: AzureActiveDirectory, Exchange, SharePoint, General, DLP.

```yaml
Type: AuditContentType
Parameter Sets: (All)
Accepted values: AzureActiveDirectory, Exchange, SharePoint, General, DLP

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EndTime
UTC end time of logs to be retrieved. Start time and end time must both be specified (or both omitted) and must be less than or equal to 24 hours apart. If passed as a string this should be defined as a valid ISO 8601 string (2024-01-16T18:28:48.6964197Z). If you don't include a timestamp in the value, the default timestamp is 12:00 AM (midnight) on the specified date.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartTime
UTC start time of logs to be retrieved. Start time and end time must both be specified (or both omitted) and must be less than or equal to 24 hours apart, with the start time prior to end time and start time no more than 7 days in the past. If passed as a string this should be defined as a valid ISO 8601 string (2024-01-16T18:28:48.6964197Z). If you don't include a timestamp in the value, the default timestamp is 12:00 AM (midnight) on the specified date.

```yaml
Type: DateTime
Parameter Sets: (All)

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[Microsoft 365 Patterns and Practices](https://aka.ms/m365pnp)

