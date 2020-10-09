---
applicable: SharePoint Online
external help file: PnP.PowerShell.dll-Help.xml
Module Name: PnP.PowerShell
online version: https://docs.microsoft.com/powershell/module/sharepoint-pnp/get-pnpunifiedauditlog
schema: 2.0.0
title: Get-PnPUnifiedAuditLog
---

# Get-PnPUnifiedAuditLog

## SYNOPSIS

**Required Permissions**

  * Microsoft Office 365 Management API: ActivityFeed.Read

Gets unified audit logs from the Office 365 Management API. Requires the Azure Active Directory application permission 'ActivityFeed.Read'.

## SYNTAX

```powershell
Get-PnPUnifiedAuditLog [-ContentType <AuditContentType>] [-StartTime <DateTime>] [-EndTime <DateTime>]
 [-ByPassPermissionCheck] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PnPUnifiedAuditLog -ContentType SharePoint -StartTime (Get-Date).AddDays(-1) -EndTime (Get-Date).AddDays(-2)
```

Retrieves the audit logs of SharePoint happening between the current time yesterday and the current time the day before yesterday

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

### -ContentType
Content type of logs to be retrieved, should be one of the following: AzureActiveDirectory, Exchange, SharePoint, General, DLP.

```yaml
Type: AuditContentType
Parameter Sets: (All)
Aliases:
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
Aliases:

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
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## RELATED LINKS

[SharePoint Developer Patterns and Practices](https://aka.ms/sppnp)