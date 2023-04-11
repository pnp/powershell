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

  * Microsoft Office 365 Management API: ActivityFeed.Read

Gets unified audit logs from the Office 365 Management API. Requires the Azure Active Directory application permission 'ActivityFeed.Read'.

## SYNTAX

```powershell
Get-PnPUnifiedAuditLog [-ContentType <AuditContentType>] [-StartTime <DateTime>] [-EndTime <DateTime>]
  
```

## DESCRIPTION

Allows to retrieve unified audit logs from the Office 365 Management API.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUnifiedAuditLog -ContentType SharePoint -StartTime (Get-Date).AddDays(-2) -EndTime (Get-Date).AddDays(-1)
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
End time of logs to be retrieved. Start time and end time must both be specified (or both omitted) and must be less than or equal to 24 hours apart.

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
Start time of logs to be retrieved. Start time and end time must both be specified (or both omitted) and must be less than or equal to 24 hours apart, with the start time prior to end time and start time no more than 7 days in the past.

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

